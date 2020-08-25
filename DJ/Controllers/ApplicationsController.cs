using System.Collections.Generic;
using System.Linq;
using DJ.DataModels;
using DJ.Dtos;
using Microsoft.AspNetCore.Mvc;
using Application = DJ.Models.Application;

namespace DJ.Controllers {
    [Route("applications")]
    public class ApplicationsController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;

        public ApplicationsController(EachDB eachDb, PoleDB poleDb)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        // GET
        [HttpGet("all")]
        public IActionResult All()
        {
            var unAllocatedApplications =
            (
                from application in _eachDb.Applications
                // where application.CreditId != null && ================ impliment after credit system
                where application.TaskId == null
                select application
            ).ToList();

            List<Application> nameSearches = new List<Application>();
            List<Application> pvtEntity = new List<Application>();

            if (unAllocatedApplications.Count > 0)
            {
                foreach (var unAllocatedApplication in unAllocatedApplications)
                {
                    var serviceFromDb =
                    (
                        from serv in _poleDb.Services
                        where serv.Id == unAllocatedApplication.ServiceId
                        select serv
                    ).FirstOrDefault();

                    if (serviceFromDb != null)
                    {
                        string service = serviceFromDb.Description;

                        if (!string.IsNullOrEmpty(service) && service.Equals("name search"))
                        {
                            nameSearches.Add(new Application
                            {
                                Id = unAllocatedApplication.Id,
                                Service = service.ToUpper(),
                                User = unAllocatedApplication.UserId,
                                SubmissionDate = unAllocatedApplication.DateSubmitted
                            });
                        }
                        else
                        {
                            pvtEntity.Add(new Application
                            {
                                Id = unAllocatedApplication.Id,
                                Service = service.ToUpper(),
                                User = unAllocatedApplication.UserId,
                                SubmissionDate = unAllocatedApplication.DateSubmitted
                            });
                        }
                    }
                }
            }

            AllApplicationsDto allApplications = new AllApplicationsDto
            {
                NameSearchApplications = nameSearches,
                PvtEntityApplications = pvtEntity
            };

            return Ok(allApplications);
        }
    }
}