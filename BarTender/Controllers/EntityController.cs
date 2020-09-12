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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BarTender.Controllers {
    [Route("api/entity")]
    public class EntityController : Controller {
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;
        private EachDB _eachDb;

        public EntityController(PoleDB poleDb, ShwaDB shwaDb, EachDB eachDb)
        {
            _poleDb = poleDb;
            _shwaDb = shwaDb;
            _eachDb = eachDb;
        }

        // GET
        [HttpGet("names")]
        public async Task<IActionResult> Index()
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo").Result.Content
                    .ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(response);

                var applications = (
                    from a in _eachDb.Applications
                    from ns in _eachDb.NameSearches.InnerJoin(k => k.ApplicationId == a.Id)
                    where a.UserId == user.Sub
                          && a.ServiceId == 12
                          && a.Status == 3
                    select new
                    {
                        ns.Id,
                        ns.Reference,
                        a.DateSubmitted,
                        ns.ExpiryDate
                    }
                ).ToList();

                List<RegisteredNameDto> names = new List<RegisteredNameDto>();
                if (applications.Count > 0)
                {
                    foreach (var application in applications)
                    {
                        var name = (
                            from n in _eachDb.Names
                            where n.NameSearchId == application.Id
                                  && n.Status == 1001
                            select new
                            {
                                n.Id,
                                n.Value
                            }
                        ).FirstOrDefault();

                        if (name == null)
                            continue;

                        names.Add(new RegisteredNameDto
                        {
                            NameId = name.Id,
                            Ref = application.Reference,
                            Name = name.Value,
                            DateSubmitted = application.DateSubmitted,
                            DateExp = application.ExpiryDate
                        });
                    }
                }

                return Ok(names);
            }
        }

        [HttpGet("{nameId}/name")]
        public async Task<IActionResult> NameChosenForApplication(int nameId)
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

            var name = (
                from n in _eachDb.Names
                from ns in _eachDb.NameSearches.InnerJoin(k => k.Id == n.NameSearchId)
                from a in _eachDb.Applications.InnerJoin(k => k.Id == ns.ApplicationId)
                where n.Id == nameId
                      && a.UserId == user.Sub
                select new
                {
                    n.Value,
                    n.NameSearchId,
                    a.SortingOffice
                }
            ).FirstOrDefault();

            if (name != null)
            {
                var service = (
                    from value in _poleDb.Services
                    where value.Description == "private limited company"
                    select value
                ).FirstOrDefault();

                int status = 1002;

                if (service != null)
                {
                    var applicationId = _eachDb.Applications
                        .Value(a => a.UserId, user.Sub)
                        .Value(a => a.ServiceId, service.Id)
                        .Value(a => a.DateSubmitted, DateTime.Now)
                        .Value(a => a.Status, status)
                        .Value(a => a.SortingOffice, name.SortingOffice)
                        .InsertWithInt32Identity();

                    if (applicationId > 0)
                    {
                        var pvtEntityId = Guid.NewGuid().ToString();
                        var entityId = _eachDb.PvtEntities
                            .Value(p => p.Id, pvtEntityId)
                            .Value(p => p.ApplicationId, applicationId)
                            .Insert();

                        if (entityId > 0)
                        {
                            return Ok(new NewNameSearchApplicationDto
                            {
                                Value = name.Value,
                                NameSearchId = name.NameSearchId,
                                ApplicationId = applicationId,
                                PvtEntityId = pvtEntityId
                            });
                        }
                    }
                }
            }

            return BadRequest();
        }

        [HttpPost("o")]
        public IActionResult SubmitOffice([FromBody] OfficeInformationDto officeInformationDto)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == officeInformationDto.ApplicationId
                select a
            ).FirstOrDefault();

            if (application != null)
            {
                var pvtApplication = (
                    from pvt in _eachDb.PvtEntities
                    where pvt.Id == officeInformationDto.PvtEntityId
                    select pvt
                ).FirstOrDefault();

                if (pvtApplication != null)
                {
                    if (pvtApplication.OfficeId == null)
                    {
                        var officeId = _eachDb.Offices
                            .Value(o => o.PhysicalAddress, officeInformationDto.Office.PhysicalAddress)
                            .Value(o => o.PostalAddress, officeInformationDto.Office.PostalAddress)
                            .Value(o => o.City, 1000)
                            .Value(o => o.MobileNumber, officeInformationDto.Office.MobileNumber)
                            .Value(o => o.TelephoneNumber, officeInformationDto.Office.TelNumber)
                            .Value(o => o.EmailAddress, officeInformationDto.Office.EmailAddress)
                            .InsertWithInt32Identity();

                        if (officeId != null)
                        {
                            pvtApplication.OfficeId = officeId;
                            if (_eachDb.Update(pvtApplication) == 1)
                            {
                                return NoContent();
                            }
                        }
                    }
                    else
                    {
                        var savedOffice = (
                            from o in _eachDb.Offices
                            where o.Id == pvtApplication.OfficeId
                            select o
                        ).FirstOrDefault();

                        savedOffice.PhysicalAddress = officeInformationDto.Office.PhysicalAddress;
                        savedOffice.PostalAddress = officeInformationDto.Office.PostalAddress;
                        // ADD City here
                        savedOffice.MobileNumber = officeInformationDto.Office.MobileNumber;
                        savedOffice.TelephoneNumber = officeInformationDto.Office.TelNumber;
                        savedOffice.EmailAddress = officeInformationDto.Office.EmailAddress;

                        if (_eachDb.Update(savedOffice) == 1)
                        {
                            return NoContent();
                        }
                    }
                }
            }

            return BadRequest("Something went wrong");
        }
    }
}