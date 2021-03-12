using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BarTender.Dtos;
using Cabinet.Dtos.External.Request;
using Dab.Clients.PrivateEntity;
using Dab.Dtos;
using Dab.Globals;
using Drinkers.ExternalClients.NameSearch;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dab.Controllers {
    [Route("entity")]
    public class PvtEntityController : Controller {
        private readonly INameSearchApiClientService _nameSearchApiClientService;
        private readonly IPrivateEntityApiClientService _privateEntityApiClientService;
        private readonly IMapper _mapper;

        public PvtEntityController(INameSearchApiClientService nameSearchApiClientService,
            IPrivateEntityApiClientService privateEntityApiClientService, IMapper mapper)
        {
            _nameSearchApiClientService = nameSearchApiClientService;
            _privateEntityApiClientService = privateEntityApiClientService;
            _mapper = mapper;
        }

        [HttpGet("{nameId}/new/{name}")]
        public async Task<IActionResult> NewPrivateEntity(int nameId, string name)
        {
            var nameClaim = User.Claims
                .FirstOrDefault(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"));
            ViewBag.User = nameClaim.Value;

            var application = await _privateEntityApiClientService.NewPrivateEntityAsync(nameId);
            if (application != null)
            {
                ViewBag.Application = application;
                ViewBag.EntityName = $"{name} (pvt) ltd";
            }

            else return NotFound();

            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var responce = client
            //         .GetAsync($"{ApiUrls.NameOnApplication}/{nameId}/name")
            //         .Result;

            // var imweResponse = client
            //     .PostAsJsonAsync<NameApplicationAndDefaults>(
            //         $"{ApiUrls.InitialisePvtApplication}", 
            //         nameAndApplication
            //         )
            //     .Result;

            //     if (responce.IsSuccessStatusCode)
            //     {
            //         var nameAndApplication =
            //             JsonConvert.DeserializeObject<NameApplicationAndDefaults>(
            //                 await responce.Content.ReadAsStringAsync());
            //         nameAndApplication.Id = nameId;
            //         ViewBag.NameRacho = nameAndApplication.Value;
            //         ViewBag.ApplicationId = nameAndApplication.ApplicationId;
            //         ViewBag.PvtEntityApplication = nameAndApplication.PvtEntityId;
            //         ViewBag.Cities = nameAndApplication.Cities;
            //         ViewBag.Countries = nameAndApplication.Countries;
            //         ViewBag.Gender = nameAndApplication.Genders;
            //     }
            //     else
            //     {
            //         return BadRequest("Something went wrong in trying to use");
            //     }
            // }

            return View();
        }

        [HttpGet("names/reg")]
        public async Task<IActionResult> RegisteredNames()
        {
            return Ok(await _nameSearchApiClientService.GetApplicableNamesAsync());
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = await client.GetAsync(ApiUrls.RegisteredNames).Result.Content
            //         .ReadAsStringAsync();
            //     return Ok(JsonConvert.DeserializeObject<List<RegisteredNameDto>>(response));
            // }
        }

        [HttpPost("office")]
        public async Task<IActionResult> Office(PrivateOfficeInformationRequestDto dto)
        {
            var officeDto = _mapper.Map<NewPrivateEntityOfficeRequestDto>(dto.AddressInformation);
            officeDto.Address = _mapper.Map<NewPrivateEntityAddressRequestDto>(dto.AddressInformation);
            officeDto.ApplicationId = dto.ApplicationId;
            var application = await _privateEntityApiClientService.NewPrivateEntityOffice(officeDto);
            if (application != null)
                return Ok();
            // dto.Office.PhysicalAddress = dto.Office.PhysicalAddress.ToUpper();
            // dto.Office.PostalAddress = dto.Office.PostalAddress.ToUpper();
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var responce = client
            //         .PostAsJsonAsync<OfficeInformationDto>(ApiUrls.SubmitPvtApplicationOffice, dto)
            //         .Result;
            //     if (responce.IsSuccessStatusCode)
            //     {
            //         return Ok();
            //     }
            // }

            return BadRequest();
        }

        //===========================================================================================================

        [HttpPost("directors")]
        public async Task<IActionResult> Directors(NewDirectorsRequestDto dto)
        {
            if (await _privateEntityApiClientService.NewDirectors(dto) != null)
                return Ok();
            return BadRequest("Something went wrong in saving Directors");
        }

        [HttpPost("secretary")]
        public async Task<IActionResult> Secretary(NewSecretaryRequestDto dto)
        {
            if (await _privateEntityApiClientService.NewSecretary(dto) != null)
                return Ok();
            return BadRequest("Something went wrong in saving Directors");
        }

        //===========================================================================================================

        [HttpPost("liability/clause")]
        public async Task<IActionResult> ShareAndLiabilityClauses(LiabilityClauseDto dto)
        {
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = client
            //         .PostAsJsonAsync<LiabilityShareClausesDto>(ApiUrls.SubmitPvtApplicationsClauses,
            //             dto).Result;
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var resp = await response.Content.ReadAsStringAsync();
            //         return Ok(JsonConvert.DeserializeObject<int>(resp));
            //     }
            // }
            var liabilityClause = _mapper.Map<NewLiabilityClauseRequestDto>(dto);
            if (await _privateEntityApiClientService.LiabilityClause(liabilityClause) != null)
                return Ok();

            return BadRequest();
        }

        [HttpPost("objects")]
        public async Task<IActionResult> Objects(MemorandumObjectsRequestDto dto)
        {
            //
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = client
            //         .PostAsJsonAsync<MemorandumObjectsRequestDto>(ApiUrls.SubmitPvtApplicationsObjects,
            //             memorandumObjectsRequestDto).Result;
            //     if (response.IsSuccessStatusCode)
            //     {
            //         return NoContent();
            //     }
            // }

            var newMemo = new NewMemorandumOfAssociationObjectsRequestDto
            {
                ApplicationId = dto.ApplicationId
            };
            foreach (var dtoObject in dto.Objects)
            {
                newMemo.Objects.Add(new NewMemorandumOfAssociationObjectRequestDto
                {
                    Value = dtoObject.Objective
                });
            }

            if (await _privateEntityApiClientService.Objectives(newMemo) != null)
                return Ok();

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
                    var populatedApplication = JsonConvert
                        .DeserializeObject<PopulatedApplicationDetailDto>(await response.Content.ReadAsStringAsync());
                    return Ok(populatedApplication);
                }
            }

            return BadRequest("Something went wrong in reloading the application");
        }
    }
}