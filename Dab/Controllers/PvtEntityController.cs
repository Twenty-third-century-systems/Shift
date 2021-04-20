using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Cabinet.Dtos.External.Request;
using Drinkers.ExternalApiClients.NameSearch;
using Drinkers.ExternalApiClients.PrivateEntity;
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

        [HttpGet("new")]
        public async Task<IActionResult> NewPrivateEntity(int nameId, string name)
        {
            var nameClaim = User.Claims
                .FirstOrDefault(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"));
            if (nameClaim != null) ViewBag.User = nameClaim.Value;

            // var application = await _privateEntityApiClientService.NewPrivateEntityAsync(nameId);
            // if (application != null)
            // {
            //     ViewBag.Application = application;
            //     ViewBag.EntityName = $"{name} (pvt) ltd";
            // }
            
            ViewBag.Application = 1;
            ViewBag.EntityName = $" (pvt) ltd";

            // else return NotFound();

            return View();
        }

        [HttpGet("names/reg")]
        public async Task<IActionResult> RegisteredNames()
        {
            return Ok(await _nameSearchApiClientService.GetApplicableNamesAsync());
        }

        [HttpPost("office")]
        public async Task<IActionResult> Office(PrivateOfficeInformationRequestDto dto)
        {
            var officeDto = _mapper.Map<NewPrivateEntityOfficeRequestDto>(dto.AddressInformation);
            officeDto.Address = _mapper.Map<NewPrivateEntityAddressRequestDto>(dto.AddressInformation);
            officeDto.ApplicationId = dto.ApplicationId;
            var application = await _privateEntityApiClientService.NewPrivateEntityOfficeAsync(officeDto);
            if (application != null)
                return Ok();

            return BadRequest();
        }

        [HttpPost("directors")]
        public async Task<IActionResult> Directors(NewDirectorsRequestDto dto)
        {
            if (await _privateEntityApiClientService.NewDirectorsAsync(dto) != null)
                return Ok();
            return BadRequest("Something went wrong in saving Directors");
        }

        [HttpPost("secretary")]
        public async Task<IActionResult> Secretary(NewSecretaryRequestDto dto)
        {
            if (await _privateEntityApiClientService.NewSecretaryAsync(dto) != null)
                return Ok();
            return BadRequest("Something went wrong in saving Directors");
        }

        [HttpPost("liability/clause")]
        public async Task<IActionResult> ShareAndLiabilityClauses(LiabilityClauseDto dto)
        {
            if (await _privateEntityApiClientService.LiabilityClauseAsync(_mapper.Map<NewLiabilityClauseRequestDto>(dto)) !=
                null)
                return Ok();
            return BadRequest();
        }

        [HttpPost("share/clauses")]
        public async Task<IActionResult> ShareClause(NewShareClausesRequestDto dto)
        {
            if (await _privateEntityApiClientService.ShareClausesAsync(dto) != null)
                return Ok();
            return BadRequest();
        }

        [HttpPost("objects")]
        public async Task<IActionResult> Objects(MemorandumObjectsRequestDto dto)
        {
            if (await _privateEntityApiClientService.ObjectivesAsync(
                _mapper.Map<NewMemorandumOfAssociationObjectsRequestDto>(dto)) != null)
                return Ok();
            return BadRequest();
        }

        [HttpPost("shareHolders")]
        public async Task<IActionResult> ShareHolders(NomineeBeneficiariesRequestDto dto)
        {
            var shareHoldersRequestDto = new NewShareHoldersRequestDto
            {
                ApplicationId = dto.ApplicationId
            };
            foreach (var shareHolder in dto.ShareHolders)
            {
                var subscription = _mapper.Map<ShareholderSubscriptionDto>(shareHolder);
                var nominee = _mapper.Map<NewShareHolderRequestDto>(shareHolder);
                nominee.Subs.Add(subscription);

                if (!string.IsNullOrEmpty(shareHolder.BeneficiaryCountryCode))
                {
                    var beneficiary = new NewShareHolderRequestDto
                    {
                        CountryCode = shareHolder.BeneficiaryCountryCode,
                        Surname = shareHolder.BeneficiarySurname,
                        Names = shareHolder.BeneficiaryNames,
                        Gender = shareHolder.BeneficiaryGender,
                        DateOfBirth = shareHolder.BeneficiaryDateOfBirth,
                        NationalIdentification = shareHolder.BeneficiaryNationalIdentification,
                        PhysicalAddress = shareHolder.BeneficiaryPhysicalAddress,
                        MobileNumber = shareHolder.BeneficiaryMobileNumber,
                        EmailAddress = shareHolder.BeneficiaryEmailAddress,
                        DateOfTakeUp = shareHolder.BeneficiaryDateOfAppointment,
                    };
                    beneficiary.Subs.Add(subscription);
                    nominee.PeopleRepresented.Add(beneficiary);
                    shareHoldersRequestDto.People.Add(nominee);
                }

                if (string.IsNullOrEmpty(shareHolder.BeneficiaryEntityCountry)) continue;
                var beneficiaryEntity = _mapper.Map<NewShareHoldingEntityRequestDto>(shareHolder);
                beneficiaryEntity.Nominees.Add(nominee);
                beneficiaryEntity.Subs.Add(subscription);
                shareHoldersRequestDto.Entities.Add(beneficiaryEntity);
            }

            if (await _privateEntityApiClientService.ShareHoldersAsync(shareHoldersRequestDto) != null)
                return Ok();
            return BadRequest();
        }

        [HttpPost("table")]
        public async Task<IActionResult> Table(TableOfArticlesDto dto)
        {
            if (await _privateEntityApiClientService.TableOfArticlesAsync(
                _mapper.Map<NewArticleOfAssociationRequestDto>(dto)) != null)
                return Ok();

            return BadRequest();
        }

        [HttpPost("amends")]
        public async Task<IActionResult> AmendedArticles(AmendedArticlesDto dto)
        {
            if (await _privateEntityApiClientService.AmendedArticlesAsync(_mapper.Map<NewAmendedArticlesRequestDto>(dto)) !=
                null)
                return Ok();

            return BadRequest();
        }

        [HttpPost("{applicationId}/submit")]
        public async Task<IActionResult> FinishApplication(int applicationId)
        {
            if (await _privateEntityApiClientService.FinishAsync(applicationId))
                return Ok();
            return BadRequest();
        }

        [HttpGet("{applicationId}/reload")]
        public IActionResult ReloadApplication(int applicationId)
        {
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = client
            //         .GetAsync($"{ApiUrls.ReloadPvtApplication}/{applicationId}/reload").Result;
            //     if (response.IsSuccessStatusCode)
            //     {
            //         var populatedApplication = JsonConvert
            //             .DeserializeObject<PopulatedApplicationDetailDto>(await response.Content.ReadAsStringAsync());
            //         return Ok(populatedApplication);
            //     }
            // }

            return BadRequest("Something went wrong in reloading the application");
        }
    }
}