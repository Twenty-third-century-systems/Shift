using System;
using System.Linq;
using BarTender.DataModels;
using BarTender.Dtos;
using BarTender.Repositories;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarTender.Controllers {
    [Route("api/name")]
    public class NameSearchController : Controller {
        private INameSearchRepository _nameSearchRepository;
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;
        private EachDB _eachDb;

        public NameSearchController(INameSearchRepository nameSearchRepository, PoleDB poleDb, ShwaDB shwaDb,
            EachDB eachDb)
        {
            _nameSearchRepository = nameSearchRepository;
            _poleDb = poleDb;
            _shwaDb = shwaDb;
            _eachDb = eachDb;
        }

        [HttpGet("defaults")]
        public IActionResult GetDefaults()
        {
            var defaults = _nameSearchRepository.GetDefaults(_poleDb, _shwaDb);
            return Ok(defaults);
        }

        [HttpPost("submit")]
        public IActionResult PostNewNameSearch([FromBody] NameSearchRequestDto details)
        {
            int status = 1;

            var applicationID = _eachDb.Applications
                .Value(a => a.Status, status)
                .Value(a => a.SortingOffice, details.Details.SortingOffice)
                .InsertWithInt32Identity();

            if (applicationID != null)
            {
                Guid nameSearchId = Guid.NewGuid();
                int nameSearchSubmissionResult = _eachDb.NameSearches
                    .Value(b => b.Id, nameSearchId.ToString())
                    .Value(b => b.Service, details.Details.TypeOfEntity) 
                    .Value(b => b.DateSubmitted, DateTime.Now)
                    .Value(b => b.Justification, details.Details.Justification)
                    .Value(b => b.DesignationId, details.Details.Designation)
                    .Value(b => b.ExpiryDate, DateTime.Now.AddDays(30))
                    .Value(b => b.ApplicationId, applicationID)
                    .Value(b => b.ReasonForSearch, details.Details.ReasonForSearch)
                    .Value(b=> b.Reference, Guid.NewGuid().ToString())
                    .Insert();
                if (nameSearchSubmissionResult == 1)
                {
                    int namesSubmited = 0;
                    foreach (var name in details.Names)
                    {
                        int nameStatus = 7;
                        namesSubmited += _eachDb.Name
                            .Value(c => c.Value, name)
                            .Value(c => c.Status, nameStatus)
                            .Value(c => c.NameSearchId, nameSearchId.ToString())
                            .Insert();
                    }

                    if (namesSubmited >= 2)
                    {
                        return Created("/submited", new NameSearchResponseDto
                        {
                            Id = nameSearchId,
                            Details = details.Details,
                            Names = details.Names
                        });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert Names");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert Name search");
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create application");
            }
        }
    }
}