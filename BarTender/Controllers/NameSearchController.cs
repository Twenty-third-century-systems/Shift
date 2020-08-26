using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BarTender.DataModels;
using BarTender.Dtos;
using BarTender.Models;
using BarTender.Repositories;
using IdentityModel.Client;
using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var userClaims = User.Claims;
            var defaults = _nameSearchRepository.GetDefaults(_poleDb, _shwaDb);
            return Ok(defaults);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> PostNewNameSearch([FromBody] NameSearchRequestDto details)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo").Result.Content
                    .ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(response);
            }


            int status = 1;

            var service = (
                from value in _poleDb.Services
                where value.Description == "name search"
                select value
            ).FirstOrDefault();

            if (service != null)
            {
                var applicationId = _eachDb.Applications
                    .Value(a => a.UserId, user.Sub)
                    .Value(a => a.ServiceId, service.Id)
                    .Value(a => a.DateSubmitted, DateTime.Now)
                    .Value(a => a.Status, status)
                    .Value(a => a.SortingOffice, details.Details.SortingOffice)
                    .InsertWithInt32Identity();

                if (applicationId != null)
                {
                    Guid nameSearchId = Guid.NewGuid();
                    int nameSearchSubmissionResult = _eachDb.NameSearches
                        .Value(b => b.Id, nameSearchId.ToString())
                        .Value(b => b.Service, details.Details.TypeOfEntity)
                        .Value(b => b.Justification, details.Details.Justification)
                        .Value(b => b.DesignationId, details.Details.Designation)                       
                        .Value(b => b.ApplicationId, applicationId)
                        .Value(b => b.ReasonForSearch, details.Details.ReasonForSearch)
                        .Value(b => b.Reference, Guid.NewGuid().ToString())
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
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}