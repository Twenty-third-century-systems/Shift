using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Cooler.DataModels;
using DJ.Dtos;
using DJ.Models;
using IdentityModel.Client;
using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TurnTable.InternalServices;
using Application = DJ.Models.Application;

namespace DJ.Controllers {
    [Route("api/applications")]
    public class ApplicationsController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;
        private IApplicationService _applicationService;

        public ApplicationsController(EachDB eachDb, PoleDB poleDb, IApplicationService applicationService)
        {
            _applicationService = applicationService;
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        // GET
        [AllowAnonymous]
        [HttpGet("all/{sortingOffice}")]
        public async Task<IActionResult> All(int sortingOffice)
        {
            // var unAllocatedApplications =
            // (
            //     from application in _eachDb.Applications
            //     // where application.CreditId != null && ================ impliment after credit system
            //     where application.TaskId == null
            //           && application.SortingOffice == office
            //     select application
            // ).ToList();
            //
            // List<Application> nameSearches = new List<Application>();
            // List<Application> pvtEntity = new List<Application>();
            //
            // if (unAllocatedApplications.Count > 0)
            // {
            //     foreach (var unAllocatedApplication in unAllocatedApplications)
            //     {
            //         var serviceFromDb =
            //         (
            //             from serv in _poleDb.Services
            //             where serv.Id == unAllocatedApplication.ServiceId
            //             select serv
            //         ).FirstOrDefault();
            //`
            //         if (serviceFromDb != null)
            //         {
            //             string service = serviceFromDb.Description;
            //
            //             if (!string.IsNullOrEmpty(service) && service.Equals("name search"))
            //             {
            //                 nameSearches.Add(new Application
            //                 {
            //                     Id = unAllocatedApplication.Id,
            //                     Service = service.ToUpper(),
            //                     User = unAllocatedApplication.UserId,
            //                     SubmissionDate =
            //                         Convert.ToDateTime(unAllocatedApplication.DateSubmitted.ToString("dd MMM yyy"))
            //                 });
            //             }
            //             else // use else if when new service introduced
            //             {
            //                 if (unAllocatedApplication.Status == 7)
            //                 {
            //                     pvtEntity.Add(new Application
            //                     {
            //                         Id = unAllocatedApplication.Id,
            //                         Service = service.ToUpper(),
            //                         User = unAllocatedApplication.UserId,
            //                         SubmissionDate =
            //                             Convert.ToDateTime(unAllocatedApplication.DateSubmitted.ToString("dd MMM yyyy"))
            //                     });
            //                 }
            //             }
            //         }
            //     }
            // }
            //
            // AllApplicationsDto allApplications = new AllApplicationsDto
            // {
            //     NameSearchApplications = nameSearches,
            //     PvtEntityApplications = pvtEntity
            // };
            //
            // return Ok(allApplications);

            return Ok(await _applicationService.GetAllUnAllocatedApplicationsAsync(sortingOffice));
        }

        [HttpPost("allocate")]
        public async Task<IActionResult> AllocateApplicationsToExaminers([FromBody] NewTaskAllocationRequestDto dto)
        {
            // User user;
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = await client.GetAsync("https://localhost:5002/connect/userinfo").Result.Content
            //         .ReadAsStringAsync();
            //     user = JsonConvert.DeserializeObject<User>(response);
            // // }
            //
            // if (task.Id == 0)
            // {
            //     var count = 0;
            //     if (task.Service.Equals("Pvt Entity applications"))
            //     {
            //         task.Service = "private limited company";
            //     }
            //
            //     var service = (
            //         from s in _poleDb.Services
            //         where s.Description.Equals(task.Service.ToLower())
            //         select s
            //     ).FirstOrDefault();
            //
            //     if (service != null)
            //     {
            //         var taskId = _eachDb.Tasks
            //             .Value(t => t.ExaminerId, task.Examiner)
            //             .Value(t => t.DateAssigned, DateTime.Now)
            //             .Value(t => t.AssignedBy, task.Allocator)
            //             .Value(t => t.ExpectedDateOfCompletion, task.DateOfCompletion)
            //             .InsertWithInt32Identity();
            //
            //         if (taskId != null && taskId > 0)
            //         {
            //             var applications = (
            //                 from a in _eachDb.Applications
            //                 where a.ServiceId == service.Id
            //                       && a.TaskId == null
            //                 select a
            //             ).ToList();
            //
            //             if (applications.Count > 0)
            //             {
            //                 for (int i = 0; i < task.NumberOfApplications; i++)
            //                 {
            //                     applications[i].TaskId = taskId;
            //                     count += _eachDb.Update(applications[i]);
            //                 }
            //
            //                 if (count == task.NumberOfApplications)
            //                 {
            //                     task.Id = taskId;
            //                     return Created("", task);
            //                 }
            //             }
            //             else
            //             {
            //                 return NotFound("The are no pending applications");
            //             }
            //         }
            //     }
            // }

            return Created("",await _applicationService.AllocateTasksAsync(dto));
            return BadRequest("Something went wrong");
        }
    }
}