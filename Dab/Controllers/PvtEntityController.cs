using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    [Route("entity")]
    public class PvtEntityController : Controller {
        // GET
        [HttpGet("{nameId}/new")]
        public async Task<IActionResult> NewPrivateEntity(int nameId)
        {
            var nameClaim = User.Claims.Where(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"))
                .FirstOrDefault();
            ViewBag.User = nameClaim.Value;

            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var responce = client
                    .GetAsync($"{ApiUrls.NameOnApplication}/{nameId}/name")
                    .Result;
                
                // var imweResponse = client
                //     .PostAsJsonAsync<NameApplicationAndDefaults>(
                //         $"{ApiUrls.InitialisePvtApplication}", 
                //         nameAndApplication
                //         )
                //     .Result;
                
                if(responce.IsSuccessStatusCode){
                    var nameAndApplication = JsonConvert.DeserializeObject<NameApplicationAndDefaults>(await responce.Content.ReadAsStringAsync());
                    nameAndApplication.Id = nameId;
                    ViewBag.NameRacho = nameAndApplication.Value;
                    ViewBag.ApplicationId = nameAndApplication.ApplicationId;
                    ViewBag.PvtEntityApplication = nameAndApplication.PvtEntityId;
                    ViewBag.Cities = nameAndApplication.Cities;
                    ViewBag.Countries = nameAndApplication.Countries;
                    ViewBag.Gender = nameAndApplication.Genders;
                }
                else
                {
                    return BadRequest("Something went wrong in trying to use");
                }
                
            }
            return View();
        }

        [HttpGet("names/reg")]
        public async Task<IActionResult> RegisteredNames()
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync(ApiUrls.RegisteredNames).Result.Content
                    .ReadAsStringAsync();
                return Ok(JsonConvert.DeserializeObject<List<RegisteredNameDto>>(response));
            }
        }

        [HttpPost("office")]
        public async Task<IActionResult> Office(OfficeInformationDto officeInformationDto)
        {
            officeInformationDto.Office.PhysicalAddress = officeInformationDto.Office.PhysicalAddress.ToUpper();
            officeInformationDto.Office.PostalAddress = officeInformationDto.Office.PostalAddress.ToUpper();
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var responce = client.PostAsJsonAsync<OfficeInformationDto>(ApiUrls.SubmitPvtApplicationOffice, officeInformationDto).Result;
                if (responce.IsSuccessStatusCode)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("clause")]
        public async Task<IActionResult> ShareAndLiabilityClauses(LiabilityShareClausesDto liabilityShareClausesDto)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<LiabilityShareClausesDto>(ApiUrls.SubmitPvtApplicationsClauses,
                        liabilityShareClausesDto).Result;
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();                    
                    return Ok(JsonConvert.DeserializeObject<int>(resp));
                }
            }
            return BadRequest();
        }

        [HttpPost("objects")]
        public async Task<IActionResult> Objects(MemorandumObjectsDto memorandumObjectsDto)
        {
            // Handle Updates to objectives 
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<MemorandumObjectsDto>(ApiUrls.SubmitPvtApplicationsObjects,
                        memorandumObjectsDto).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpPost("table")]
        public async Task<IActionResult> Table(ArticleTableDto articleTableDto)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<ArticleTableDto>(ApiUrls.SubmitPvtApplicationsArticleTable,
                        articleTableDto).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpPost("amends")]
        public async Task<IActionResult> AmendedArticles(AmmendedArticleDto amendedArticleDto)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<AmmendedArticleDto>(ApiUrls.SubmitPvtApplicationsAmendedArticle,
                        amendedArticleDto).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpPost("people")]
        public async Task<IActionResult> People(ShareHoldingPersonDto shareHoldingPersonDto)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<ShareHoldingPersonDto>(ApiUrls.SubmitPvtApplicationShareHoldingPeople,
                        shareHoldingPersonDto).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpPost("entity")]
        public async Task<IActionResult> Entity(ShareHoldingEntityDto shareHoldingEntityDto)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<ShareHoldingEntityDto>(ApiUrls.SubmitPvtApplicationShareHoldingEntities,
                        shareHoldingEntityDto).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpPost("{applicationId}/submit")]
        public async Task<IActionResult> SubmitEntity(int applicationId)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .PostAsJsonAsync<int>(ApiUrls.SubmitPvtApplication,
                        applicationId).Result;
                if (response.IsSuccessStatusCode)
                {                                        
                    return NoContent();
                }
            }
            return BadRequest();
        }

        [HttpGet("{applicationId}/reload")]
        public async Task<IActionResult> ReloadApplication(int applicationId)
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = client
                    .GetAsync($"{ApiUrls.ReloadPvtApplication}/{applicationId}/reload").Result;
                if (response.IsSuccessStatusCode)
                {
                    var populatedApplication =  JsonConvert
                        .DeserializeObject<PopulatedApplicationDetailDto>(await response.Content.ReadAsStringAsync());
                    return Ok(populatedApplication);
                }
            }
            return BadRequest("Something went wrong in reloading the application");
        }
    }
}