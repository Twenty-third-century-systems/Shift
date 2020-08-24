using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BarTender.Dtos;
using Dab.Dtos;
using Dab.Globals;
using Dab.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(accessToken);
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

        // Post
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
                    
                    if(!string.IsNullOrEmpty(newNameDetails.Name1))
                    nameSearchRequest.Names.Add(newNameDetails.Name1);
                    if(!string.IsNullOrEmpty(newNameDetails.Name2))
                    nameSearchRequest.Names.Add(newNameDetails.Name2);
                    if(!string.IsNullOrEmpty(newNameDetails.Name3))
                    nameSearchRequest.Names.Add(newNameDetails.Name3);
                    if(!string.IsNullOrEmpty(newNameDetails.Name4))
                    nameSearchRequest.Names.Add(newNameDetails.Name4);
                    if(!string.IsNullOrEmpty(newNameDetails.Name5))
                    nameSearchRequest.Names.Add(newNameDetails.Name5);
                    
                    var accessToken = await HttpContext.GetTokenAsync("access_token");
                    client.SetBearerToken(accessToken);
                    var response = await client
                        .PostAsJsonAsync<NameSearchRequestDto>(ApiUrls.SubmitNameSearchUrl, nameSearchRequest).Result.Content
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
    }
}