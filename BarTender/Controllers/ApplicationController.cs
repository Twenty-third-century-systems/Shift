using System.Linq;
using BarTender.Dtos;
using Cooler.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarTender.Controllers {
    [Authorize]
    [Route("[controller]")]
    public class ApplicationController : Controller {
        private EachDB _eachDb;

        public ApplicationController(EachDB eachDb)
        {
            _eachDb = eachDb;
        }

        // [AllowAnonymous]
        [HttpGet("ns/sum/{applicationId}")]
        public IActionResult GetRegisteredNameSummary(int applicationId)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == applicationId
                      && a.ServiceId == 12
                select a
            ).SingleOrDefault();            

            if (application != null)
            {
                var search = (
                    from nameSearch in _eachDb.NameSearches
                    where nameSearch.ApplicationId == application.Id
                    select nameSearch
                ).SingleOrDefault();

                if (search != null)
                {
                    var name = (
                        from names in _eachDb.Names
                        where names.NameSearchId == search.Id
                              && names.Status == 1001
                        select names
                    ).SingleOrDefault();

                    if (name != null)
                    {
                        return Ok(new ReservedNameDto
                            {
                                NameSearchRef = search.Reference,
                                Value = name.Value,
                                ExpiryDate = search.ExpiryDate.Value
                            }
                        );
                    }
                }
            }

            return NotFound();
        }
    }
}