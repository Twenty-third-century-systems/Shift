using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Models;
using Cabinet.Dtos.External.Request;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TurnTable.ExternalServices.PrivateEntity;

namespace BarTender.Controllers {
    [Route("api/entity")]
    public class PvtEntityController : Controller {
        private readonly IPrivateEntityService _privateEntityService;

        public PvtEntityController(IPrivateEntityService privateEntityService)
        {
            _privateEntityService = privateEntityService;
        }

        [HttpGet("{applicationId}/reload")]
        public IActionResult ReloadApplication(int applicationId)
        {
            // var populatedApplicationDetails = new PopulatedApplicationDetailDto();
            // var pvtEntity = (
            //     from p in _eachDb.PvtEntities
            //     where p.ApplicationId == applicationId
            //     select p
            // ).FirstOrDefault();
            //
            // if (pvtEntity != null)
            // {
            //     //Populating office details
            //     var populatedOfficeDetails = (
            //         from o in _eachDb.Offices
            //         where o.Id == pvtEntity.OfficeId
            //         select o
            //     ).FirstOrDefault();
            //     if (populatedOfficeDetails != null)
            //     {
            //         populatedApplicationDetails.Office = new Office
            //         {
            //             PhysicalAddress = populatedOfficeDetails.PhysicalAddress,
            //             OfficeCity = populatedOfficeDetails.City,
            //             PostalAddress = populatedOfficeDetails.PostalAddress,
            //             EmailAddress = populatedOfficeDetails.EmailAddress,
            //             TelNumber = populatedOfficeDetails.TelephoneNumber,
            //             MobileNumber = populatedOfficeDetails.MobileNumber
            //         };
            //     }
            //
            //     //Populating Memo
            //     var memorundum = (
            //         from m in _eachDb.Memorundums
            //         where m.Id == pvtEntity.MemorundumId
            //         select m
            //     ).FirstOrDefault();
            //     if (memorundum != null)
            //     {
            //         populatedApplicationDetails.LiabilityClause = memorundum.LiabilityClause;
            //         populatedApplicationDetails.ShareClause = memorundum.ShareClause;
            //
            //         var objects = (
            //             from m in _eachDb.MemoObjects
            //             where m.MemorundumId == memorundum.Id
            //             select m
            //         ).ToList();
            //         if (objects.Count > 0)
            //         {
            //             foreach (var memoObject in objects)
            //             {
            //                 populatedApplicationDetails.Objectives.Add(new SingleObjective
            //                 {
            //                     Id = memoObject.Id,
            //                     Objective = memoObject.Value
            //                 });
            //             }
            //         }
            //     }
            //
            //     //Populate Articles
            //     var articles = (
            //         from art in _eachDb.ArticleOfAssociations
            //         where art.Id == pvtEntity.ArticlesId
            //         select art
            //     ).FirstOrDefault();
            //     if (articles != null)
            //     {
            //         if (articles.TableA != null)
            //         {
            //             populatedApplicationDetails.TableOfArticles = "Table A";
            //         }
            //         else if (articles.TableB != null)
            //         {
            //             populatedApplicationDetails.TableOfArticles = "Table B";
            //         }
            //         else
            //         {
            //             populatedApplicationDetails.TableOfArticles = "Other";
            //         }
            //
            //         var objects = (
            //             from a in _eachDb.AmmendedArticles
            //             where a.ArticleId == articles.Id
            //             select a
            //         ).ToList();
            //         if (objects.Count > 0)
            //         {
            //             foreach (var o in objects)
            //             {
            //                 populatedApplicationDetails.AmendedArticles.Add(new AmendedArticle
            //                 {
            //                     Id = o.Id,
            //                     Article = o.Value
            //                 });
            //             }
            //         }
            //     }
            //
            //     //Subscribers here
            //     var subscribers = (
            //         from p in _eachDb.PvtEntityHasSubcribers
            //         from s in _eachDb.Subcribers.InnerJoin(s => p.Subcriber == s.Id)
            //         from r in _eachDb.Roles.InnerJoin(r => r.Id == p.RolesId)
            //         from sr in _eachDb.Subscriptions.InnerJoin(sr => sr.Id == p.SubscriptionId)
            //         where p.Entity == pvtEntity.Id
            //         select new
            //         {
            //             s,
            //             r,
            //             sr
            //         }).ToList();
            //     if (subscribers.Count > 0)
            //     {
            //         foreach (var subscriber in subscribers)
            //         {
            //             bool isMember = subscriber.r.Member == null;
            //             bool isDirector = subscriber.r.Director == null;
            //             bool isSecretary = subscriber.r.Secretary == null;
            //             populatedApplicationDetails.Members.Add(new ShareHoldingMember
            //             {
            //                 PeopleCountry = subscriber.s.CountryCode,
            //                 NationalId = subscriber.s.NationalId,
            //                 MemberSurname = subscriber.s.Surname,
            //                 MemberName = subscriber.s.FirstName,
            //                 PhyAddress = subscriber.s.PhysicalAddress,
            //                 IsSecretary = isSecretary,
            //                 IsMember = isMember,
            //                 IsDirector = isDirector,
            //                 OrdShares = subscriber.sr.Ordinary.ToString(),
            //                 PrefShares = subscriber.sr.Preference.ToString()
            //             });
            //         }
            //     }
            //
            //     return Ok(populatedApplicationDetails);
            // }

            return BadRequest("Could not reload application");
        }


        [HttpGet("names")]
        public async Task<IActionResult> GetApplicableNames()
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

            return Ok(await _privateEntityService.GetRegisteredNamesAsync(user.Sub));
        }

        [AllowAnonymous]
        [HttpPost("name")]
        public async Task<IActionResult> NameChosenForApplication(int nameId)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.CreateApplicationAsync(user.Sub, nameId));
        }

        [AllowAnonymous]
        [HttpPost("office")]
        public async Task<IActionResult> Office([FromBody] NewPrivateEntityOfficeRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertOfficeAsync(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("directors")]
        public async Task<IActionResult> Directors([FromBody] NewDirectorsRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            if (dto.Directors.Count > 1)
                return Ok(await _privateEntityService.InsertDirectors(user.Sub, dto));
            return BadRequest("A minimum of two directors is required");
        }

        [AllowAnonymous]
        [HttpPost("secretary")]
        public async Task<IActionResult> Secretary([FromBody] NewSecretaryRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(_privateEntityService.InsertSecretary(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("liability/clause")]
        public async Task<IActionResult> Clauses([FromBody] NewLiabilityClauseRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertLiabilityClauseAsync(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("share/clause")]
        public async Task<IActionResult> ShareClause([FromBody] NewShareClausesRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertShareClauseAsync(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("memorandum/objects")]
        public async Task<IActionResult> Objects([FromBody] NewMemorandumOfAssociationObjectsRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertMemorandumObjectsAsync(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("articles")]
        public async Task<IActionResult> TableArticle([FromBody] NewArticleOfAssociationRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertArticlesOfAssociationAsync(user.Sub, dto));
        }

        [HttpPost("amended/articles")]
        public async Task<IActionResult> AmendedArticles([FromBody] NewAmendedArticlesRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(_privateEntityService.InsertAmendedArticles(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpPost("shareholders")]
        public async Task<IActionResult> ShareHoldingPeople([FromBody] NewShareHoldersRequestDto dto)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            return Ok(await _privateEntityService.InsertMembersAsync(user.Sub, dto));
        }

        [AllowAnonymous]
        [HttpHead("finish")]
        public async Task<IActionResult> FinishApplication(int applicationId)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            if (await _privateEntityService.FinishApplicationAsync(user.Sub, applicationId) > 0)
                return NoContent();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("resubmit/{applicationId}")]
        public async Task<IActionResult> ResubmitApplication(int applicationId)
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }
                else
                {
                    return Unauthorized();
                }
            }

            var resubmitApplicationAsync =
                await _privateEntityService.ResubmitApplicationAsync(user.Sub, applicationId);
            if (resubmitApplicationAsync != null)
                return Ok(resubmitApplicationAsync);
            return BadRequest("Application does not qualify for resubmission");
        }
    }
}