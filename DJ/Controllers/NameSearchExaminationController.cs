using System;
using System.Linq;
using System.Text.Json.Serialization;
using Cooler.DataModels;
using DanceFlow.Dtos;
using DJ.Dtos;
using DJ.Models;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DJ.Controllers {
    [Route("api/ex/name")]
    public class NameSearchExaminationController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;

        public NameSearchExaminationController(EachDB eachDb, PoleDB poleDb)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        [HttpPatch("{nameId}/{status}")]
        public IActionResult ExamineName(int nameId, string status)
        {
            var name = (
                from n in _eachDb.Names
                where n.Id == nameId
                select n
            ).FirstOrDefault();

            if (!status.Equals("reserved") && name != null)
            {
                name.Status = (
                    from s in _poleDb.Status
                    where s.Description == status
                    select s.Id
                ).FirstOrDefault();
            }
            else
            {
                var names = (
                    from c in _eachDb.Names
                    where c.NameSearchId == name.NameSearchId
                          && c.Id != name.Id
                    select c
                ).ToList();

                int notConsidered = (
                    from t in _poleDb.Status
                    where t.Description == "not considered"
                    select t.Id
                ).FirstOrDefault();

                foreach (var d in names)
                {
                    if (d.Status == 7)
                    {
                        d.Status = notConsidered;
                        _eachDb.Update(d);
                    }
                }
            }

            if (_eachDb.Update(name) == 1)
                return NoContent();
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Something happened while trying to examine name");
            }
        }

        [HttpPatch("f/{applicationId}")]
        public IActionResult FinishNameSearchExamination(int applicationId)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == applicationId
                select a
            ).FirstOrDefault();

            if (application != null)
            {
                var nameSearch = (
                    from ns in _eachDb.NameSearches
                    where ns.ApplicationId == application.Id
                    select ns
                ).FirstOrDefault();

                if (nameSearch != null)
                {
                    application.Status = (
                        from s in _poleDb.Status
                        where s.Description.Equals("examined")
                        select s.Id
                    ).FirstOrDefault();
                    application.DateExamined = DateTime.Now;

                    nameSearch.ExpiryDate = DateTime.Now.AddDays(30);
                    nameSearch.Reference = "NS/" + DateTime.Now.Year.ToString() + "/" + nameSearch.Id.ToString();

                    if (_eachDb.Update(application) + _eachDb.Update(nameSearch) == 2)
                    {
                        return NoContent();
                    }
                }
            }
            return BadRequest("Application has incorrect information");
        }
    }
}