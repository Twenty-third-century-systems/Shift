using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cabinet.Dtos.Internal.Request;
using Drinkers.InternalApiClients.Applications;
using Drinkers.InternalApiClients.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Route("applications")]
    public class ApplicationsController : Controller {
        private readonly ITaskApiClientService _taskApiClientService;
        private readonly IApplicationsApiClientService _applicationsApiClientService;

        public ApplicationsController(ITaskApiClientService taskApiClientService,
            IApplicationsApiClientService applicationsApiClientService)
        {
            _taskApiClientService = taskApiClientService;
            _applicationsApiClientService = applicationsApiClientService;
        }


        [Authorize(Policy = "IsPrincipal")]
        [HttpGet("")]
        public async Task<IActionResult> AllocateTasks()
        {
            var authorityUrl = "https://localhost:5002/U";
            var office = User
                .Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            using (var client = new HttpClient())
            {
                if (office != null)
                {
                    var response = await client.GetAsync($"{authorityUrl}/{office.Value}")
                        .Result
                        .Content
                        .ReadAsStringAsync();
                    var examiners = JsonConvert.DeserializeObject<List<AvailableExaminerRequestDto>>(response);
                    ViewBag.Examiners = examiners;
                }
            }

            if (office != null)
            {
                var unallocatedApplications =
                    await _taskApiClientService.GetAllUnallocatedApplicationsAsync(Int32.Parse(office.Value));
                if (unallocatedApplications != null)
                {
                    ViewBag.NameSearchCount = unallocatedApplications.Count(a => a.Service.Equals("NameSearch"));
                    ViewBag.PrivateEntitiesCount =
                        unallocatedApplications.Count(a => a.Service.Equals("PrivateLimitedCompany"));
                }
                else
                {
                    ViewBag.NameSearchCount = 0;
                    ViewBag.PrivateEntitiesCount = 0;
                }
            }

            return View();
        }


        [HttpPost("allocate")]
        public async Task<IActionResult> Allocate(NewTaskAllocationRequestDto dto)
        {
            var user = User.Claims
                .FirstOrDefault(c => c.Type.Equals("sub") && c.Issuer.Equals("https://localhost:5002"));
            var office = User.Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            if (user != null) dto.AssignedBy = Guid.Parse(user.Value);
            else return NotFound();
            dto.DateAssigned = DateTime.Now;
            if (office != null) dto.SortingOffice = Convert.ToInt32(office.Value);
            else return NotFound();

            var multipleApplicationTask = await _taskApiClientService.PostMultipleApplicationTaskAsync(dto);
            if (multipleApplicationTask != null) return Ok(multipleApplicationTask);
            
            return BadRequest("Could not allocate");
        }

        [HttpGet("approval/pending")]
        public IActionResult PendingApprovals()
        {
            var office = User.Claims
                .FirstOrDefault(c => c.Type.Equals("office") && c.Issuer.Equals("https://localhost:5002"));
            if (office != null) ViewBag.SortingOffice = Convert.ToInt32(office.Value);
            return View();
        }

        [HttpGet("approval/{sortingOffice}/pending/applications")]
        public async Task<IActionResult> PendingApprovalApplications(int sortingOffice)
        {
            return Ok(await _applicationsApiClientService.ApplicationsPendingApprovalAsync(sortingOffice));
        }

        [HttpPost("approval/{applicationId}/approve")]
        public async Task<IActionResult> ApproveApplication(int applicationId)
        {
            if (await _applicationsApiClientService.ApproveAsync(applicationId))
                return Ok();
            return BadRequest();
        }
    }
}