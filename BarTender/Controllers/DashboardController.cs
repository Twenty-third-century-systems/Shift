using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Dtos;
using BarTender.Models;
using Cooler.DataModels;
using IdentityModel.Client;
using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TurnTable.ExternalServices;
using TurnTable.ExternalServices.Values;

namespace BarTender.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;
        private IValueService _valueService;

        public DashboardController(EachDB eachDb, PoleDB poleDb,IValueService valueService)
        {
            _valueService = valueService;
            _poleDb = poleDb;
            _eachDb = eachDb;
        }

        // [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetExternalUserDash()
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    var userDetailsFromAuth = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(userDetailsFromAuth);
                }
                else
                {
                    // TODO: to substitute with NOT ALLOWED
                    return BadRequest("Not allowed");
                }
            }

            // ExternalUserDashboardDto externalUserDashboard = new ExternalUserDashboardDto();
            //
            // var applications = (
            //     from a in _eachDb.Applications
            //     orderby a.DateSubmitted
            //     where a.UserId.Equals(user.Sub)
            //           && a.Status != 1002
            //     select a
            // ).ToList();
            //
            // //Number of applications
            // externalUserDashboard.ApplicationsSubmitted = applications.Count;
            //
            // //Registered Entities
            // var examinedEntityApplications =
            //     applications.Where(a => a.DateExamined != null && a.ServiceId == 13).ToList();
            // foreach (var examinedEntityApplication in examinedEntityApplications)
            // {
            //     var entity = (
            //         from e in _eachDb.PvtEntities
            //         where e.ApplicationId == examinedEntityApplication.Id
            //         select e
            //     ).SingleOrDefault();
            //
            //     if (entity != null && entity.Reference != null)
            //         externalUserDashboard.RegisteredEntities++;
            // }
            //
            // //Account Balance
            // using (var client = new HttpClient())
            // {
            //     var paymentDataDto = new PaymentDataDto
            //     {
            //         Email = "brightonkofu@outlook.com",
            //         Service = 1,
            //         UserId = Guid.Parse(user.Sub) //user.Sub 
            //     };
            //
            //     //TODO: get email from authority
            //
            //
            //     var responce = await client
            //         .GetAsync($"https://localhost:44375/Information/{user.Sub}");// user.Sub
            //     if (responce.IsSuccessStatusCode)
            //     {
            //         externalUserDashboard.AccountBalance = double.Parse(await responce.Content.ReadAsStringAsync());
            //     }
            // }
            //
            // //Recent Activity
            // externalUserDashboard.RecentActivities = new List<RecentActivityDto>();
            // var recentActs = applications.Take(10).ToList();
            // foreach (var recentAct in recentActs)
            // {
            //     var service = (
            //         from s in _poleDb.Services
            //         where s.Id == recentAct.ServiceId
            //         select s.Description
            //     ).SingleOrDefault();
            //
            //     var status = (
            //         from s in _poleDb.Status
            //         where s.Id == recentAct.Status
            //         select s.Description
            //     ).SingleOrDefault();
            //
            //     externalUserDashboard.RecentActivities.Add(new RecentActivityDto
            //     {
            //         Id = recentAct.Id,
            //         Service = service.ToUpper(),
            //         Date = recentAct.DateSubmitted.ToString("g"),
            //         Status = status
            //     });
            // }
            //
            // //Approved Applications
            // externalUserDashboard.ApprovedApplications = new List<ApprovedApplicationDto>();
            // var examinedApplications =
            //     applications.Where(a => a.DateExamined != null).ToList();
            // foreach (var examinedApplication in examinedApplications)
            // {
            //     var service = (
            //         from s in _poleDb.Services
            //         where s.Id == examinedApplication.ServiceId
            //         select s.Description
            //     ).SingleOrDefault();
            //
            //     if (examinedApplication.ServiceId == 12)
            //     {
            //         var nameSearchRef = (
            //             from n in _eachDb.NameSearches
            //             where n.ApplicationId == examinedApplication.Id
            //                   && n.ExpiryDate != null
            //                   && n.Reference != null
            //             select n.Reference
            //         ).SingleOrDefault();
            //
            //         if (!string.IsNullOrEmpty(nameSearchRef))
            //             externalUserDashboard.ApprovedApplications.Add(new ApprovedApplicationDto
            //             {
            //                 Id = examinedApplication.Id,
            //                 Date = examinedApplication.DateSubmitted.ToString("g"),
            //                 Reference = nameSearchRef,
            //                 Service = service.ToUpper()
            //             });
            //     }
            //     else if (examinedApplication.ServiceId == 13)
            //     {
            //         var entity = (
            //             from p in _eachDb.PvtEntities
            //             where p.ApplicationId == examinedApplication.Id
            //                   && p.Reference != null
            //             select p
            //         ).SingleOrDefault();
            //
            //         if (entity != null)
            //         {
            //             var nameSearchId = (
            //                 from n in _eachDb.Names
            //                 where n.Id == entity.NameId
            //                 select n.NameSearchId
            //             ).SingleOrDefault();
            //
            //             var applicationId = (
            //                 from n in _eachDb.NameSearches
            //                 where n.Id == nameSearchId
            //                 select n.ApplicationId
            //             ).SingleOrDefault();
            //             
            //             externalUserDashboard.ApprovedApplications.Add(new ApprovedApplicationDto
            //             {
            //                 Id = examinedApplication.Id,
            //                 Date = examinedApplication.DateSubmitted.ToString("g"),
            //                 Reference = entity.Reference,
            //                 Service = service.ToUpper(),
            //                 NameSearchApplication = applicationId
            //             });                        
            //         }                    
            //     }
            // }
            
            return Ok(await _valueService.GetUserDashBoardValuesAsync(user.Sub));
        }
    }
}