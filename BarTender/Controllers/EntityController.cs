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
        public IActionResult NameChosenForApplication(int nameId)
        {
            var name = (
                from n in _eachDb.Names
                where n.Id == nameId
                select new
                {
                    n.Value,
                    n.NameSearchId
                }
            ).FirstOrDefault();
            return Ok(new NameOnApplicationDto
            {
                Value = name.Value,
                NameSearchId = name.NameSearchId
            });
        }
    }
}