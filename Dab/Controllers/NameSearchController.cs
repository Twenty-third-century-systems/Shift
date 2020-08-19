using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BarTender.Dtos;
using Dab.Globals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dab.Controllers {
    [Route("name-search")]
    public class NameSearchController : Controller {
        // GET
        [HttpGet("new")]
        public async Task<IActionResult> NewNameSearch()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(ApiUrls.NameSearchDefaultsUrl).Result.Content
                        .ReadAsStringAsync();
                    var nameSearchDefaults = JsonConvert.DeserializeObject<NameSearchDefaultsDto>(response);
                    ViewBag.Defaults = nameSearchDefaults;
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

            return View();
        }

        //post
        [HttpPost("new-submission")]
        public async Task<IActionResult> NewSubmission([FromBody] NewNameSearchDto newNameDetails)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var response = await client
                        .PostAsJsonAsync<NewNameSearchDto>(ApiUrls.SubmitNameSearchUrl, newNameDetails).Result.Content
                        .ReadAsStringAsync();
                    NewNameSearchDto nameSearch = JsonConvert.DeserializeObject<NewNameSearchDto>(response);
                    return Created("/name-search/new-submission", nameSearch);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
    }
}