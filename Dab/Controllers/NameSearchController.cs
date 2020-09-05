using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BarTender.Dtos;
using BarTender.Models;
using Dab.Dtos;
using Dab.Globals;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NameSearchDetails = Dab.Models.NameSearchDetails;
using User = Dab.Models.User;

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
                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(accessToken);
                    var apiResponse = await client.GetAsync(ApiUrls.NameSearchDefaultsUrl).Result.Content
                        .ReadAsStringAsync();
                    var nameSearchDefaults = JsonConvert.DeserializeObject<NameSearchDefaultsDto>(apiResponse);
                    var nameClaim = User.Claims
                        .Where(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"))
                        .FirstOrDefault();
                    ViewBag.User = nameClaim.Value;
                    ViewBag.Defaults = nameSearchDefaults;
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

            return View();
        }

        // POST
        [HttpPost("submission")]
        public async Task<IActionResult> NewSubmission(NewNameSearchDto newNameDetails)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    NameSearchDetails details = new NameSearchDetails
                    {
                        ReasonForSearch = newNameDetails.Reason,
                        TypeOfEntity = newNameDetails.Type,
                        Justification = newNameDetails.Justification,
                        Designation = newNameDetails.Designation,
                        SortingOffice = newNameDetails.Office
                    };

                    NameSearchRequestDto nameSearchRequest = new NameSearchRequestDto
                    {
                        Details = details,
                        Names = new List<string>()
                    };

                    if (!string.IsNullOrEmpty(newNameDetails.Name1))
                        nameSearchRequest.Names.Add(newNameDetails.Name1.ToUpper());
                    if (!string.IsNullOrEmpty(newNameDetails.Name2))
                        nameSearchRequest.Names.Add(newNameDetails.Name2.ToUpper());
                    if (!string.IsNullOrEmpty(newNameDetails.Name3))
                        nameSearchRequest.Names.Add(newNameDetails.Name3.ToUpper());
                    if (!string.IsNullOrEmpty(newNameDetails.Name4))
                        nameSearchRequest.Names.Add(newNameDetails.Name4.ToUpper());
                    if (!string.IsNullOrEmpty(newNameDetails.Name5))
                        nameSearchRequest.Names.Add(newNameDetails.Name5.ToUpper());

                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(accessToken);
                    var response = await client
                        .PostAsJsonAsync<NameSearchRequestDto>(ApiUrls.SubmitNameSearchUrl, nameSearchRequest).Result
                        .Content
                        .ReadAsStringAsync();
                    NameSearchResponseDto nameSearch = JsonConvert.DeserializeObject<NameSearchResponseDto>(response);
                    return Created("/name-search/new-submission", nameSearch);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        // GET
        [HttpGet("availability/name")]
        public async Task<IActionResult> NameAvailability(Names name)
        {
            var nameToSend = "";

            if (!string.IsNullOrEmpty(name.name1))
                nameToSend = name.name1;
            else if (!string.IsNullOrEmpty(name.name2))
                nameToSend = name.name2;
            else if (!string.IsNullOrEmpty(name.name3))
                nameToSend = name.name3;
            else if (!string.IsNullOrEmpty(name.name4))
                nameToSend = name.name4;
            else
                nameToSend = name.name5;

            using (var client = new HttpClient())
            {
                var responce = await client.GetAsync($"{ApiUrls.CheckNameAvailability}/{nameToSend}/availability");
                if (responce.IsSuccessStatusCode)
                {
                    return Ok(true);
                }

                return Ok("\"Name not available\"");
            }
        }
    }
}