using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using BarTender.Dtos;
using BarTender.Models;
using Cabinet.Dtos.External.Request;
using Cooler.DataModels;
using IdentityModel.Client;
using LinqToDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TurnTable.ExternalServices;
using Gender = BarTender.Models.Gender;
using Office = BarTender.Models.Office;

namespace BarTender.Controllers {
    [Route("api/entity")]
    public class PvtEntityController : Controller {
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;
        private EachDB _eachDb;
        private Guid _user = Guid.Parse("375cad3c-ed53-4757-a186-02d15cfcc110");
        private readonly IPrivateEntityService _privateEntityService;


        public PvtEntityController(PoleDB poleDb, ShwaDB shwaDb, EachDB eachDb,
            IPrivateEntityService privateEntityService)
        {
            _privateEntityService = privateEntityService;
            _poleDb = poleDb;
            _shwaDb = shwaDb;
            _eachDb = eachDb;
        }

        [HttpGet("{applicationId}/reload")]
        public async Task<IActionResult> ReloadApplication(int applicationId)
        {
            var PopulatedApplicationDetails = new PopulatedApplicationDetailDto();
            var pvtEntity = (
                from p in _eachDb.PvtEntities
                where p.ApplicationId == applicationId
                select p
            ).FirstOrDefault();

            if (pvtEntity != null)
            {
                //Populating office details
                var populatedOfficeDetails = (
                    from o in _eachDb.Offices
                    where o.Id == pvtEntity.OfficeId
                    select o
                ).FirstOrDefault();
                if (populatedOfficeDetails != null)
                {
                    PopulatedApplicationDetails.Office = new Office
                    {
                        PhysicalAddress = populatedOfficeDetails.PhysicalAddress,
                        OfficeCity = populatedOfficeDetails.City,
                        PostalAddress = populatedOfficeDetails.PostalAddress,
                        EmailAddress = populatedOfficeDetails.EmailAddress,
                        TelNumber = populatedOfficeDetails.TelephoneNumber,
                        MobileNumber = populatedOfficeDetails.MobileNumber
                    };
                }

                //Populating Memo
                var memorundum = (
                    from m in _eachDb.Memorundums
                    where m.Id == pvtEntity.MemorundumId
                    select m
                ).FirstOrDefault();
                if (memorundum != null)
                {
                    PopulatedApplicationDetails.LiabilityClause = memorundum.LiabilityClause;
                    PopulatedApplicationDetails.ShareClause = memorundum.ShareClause;

                    var objects = (
                        from m in _eachDb.MemoObjects
                        where m.MemorundumId == memorundum.Id
                        select m
                    ).ToList();
                    if (objects.Count > 0)
                    {
                        foreach (var memoObject in objects)
                        {
                            PopulatedApplicationDetails.Objectives.Add(new SingleObjective
                            {
                                Id = memoObject.Id,
                                Objective = memoObject.Value
                            });
                        }
                    }
                }

                //Populate Articles
                var articles = (
                    from art in _eachDb.ArticleOfAssociations
                    where art.Id == pvtEntity.ArticlesId
                    select art
                ).FirstOrDefault();
                if (articles != null)
                {
                    if (articles.TableA != null)
                    {
                        PopulatedApplicationDetails.TableOfArticles = "Table A";
                    }
                    else if (articles.TableB != null)
                    {
                        PopulatedApplicationDetails.TableOfArticles = "Table B";
                    }
                    else
                    {
                        PopulatedApplicationDetails.TableOfArticles = "Other";
                    }

                    var objects = (
                        from a in _eachDb.AmmendedArticles
                        where a.ArticleId == articles.Id
                        select a
                    ).ToList();
                    if (objects.Count > 0)
                    {
                        foreach (var o in objects)
                        {
                            PopulatedApplicationDetails.AmendedArticles.Add(new AmendedArticle
                            {
                                Id = o.Id,
                                Article = o.Value
                            });
                        }
                    }
                }

                //Subscribers here
                var subscribers = (
                    from p in _eachDb.PvtEntityHasSubcribers
                    from s in _eachDb.Subcribers.InnerJoin(s => p.Subcriber == s.Id)
                    from r in _eachDb.Roles.InnerJoin(r => r.Id == p.RolesId)
                    from sr in _eachDb.Subscriptions.InnerJoin(sr => sr.Id == p.SubscriptionId)
                    where p.Entity == pvtEntity.Id
                    select new
                    {
                        s,
                        r,
                        sr
                    }).ToList();
                if (subscribers.Count > 0)
                {
                    foreach (var subscriber in subscribers)
                    {
                        bool isMember = subscriber.r.Member == null;
                        bool isDirector = subscriber.r.Director == null;
                        bool isSecretary = subscriber.r.Secretary == null;
                        PopulatedApplicationDetails.Members.Add(new ShareHoldingMember
                        {
                            PeopleCountry = subscriber.s.CountryCode,
                            NationalId = subscriber.s.NationalId,
                            MemberSurname = subscriber.s.Surname,
                            MemberName = subscriber.s.FirstName,
                            PhyAddress = subscriber.s.PhysicalAddress,
                            IsSecretary = isSecretary,
                            IsMember = isMember,
                            IsDirector = isDirector,
                            OrdShares = subscriber.sr.Ordinary.ToString(),
                            PrefShares = subscriber.sr.Preference.ToString()
                        });
                    }
                }

                return Ok(PopulatedApplicationDetails);
            }

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

            //
            //     var applications = (
            //         from a in _eachDb.Applications
            //         from ns in _eachDb.NameSearches.InnerJoin(k => k.ApplicationId == a.Id)
            //         where a.UserId == user.Sub
            //               && a.ServiceId == 12
            //               && a.Status == 3
            //               && ns.ReasonForSearch == 1
            //               && ns.Service == 13
            //         select new
            //         {
            //             ns.Id,
            //             ns.Reference,
            //             a.DateSubmitted,
            //             ns.ExpiryDate
            //         }
            //     ).ToList();
            //
            //     List<RegisteredNameDto> names = new List<RegisteredNameDto>();
            //     if (applications.Count > 0)
            //     {
            //         foreach (var application in applications)
            //         {
            //             var name = (
            //                 from n in _eachDb.Names
            //                 where n.NameSearchId == application.Id
            //                       && n.Status == 1001
            //                 select new
            //                 {
            //                     n.Id,
            //                     n.Value
            //                 }
            //             ).FirstOrDefault();
            //
            //             if (name == null)
            //                 continue;
            //
            //             var companyApplication = (
            //                 from a in _eachDb.Applications
            //                 from p in _eachDb.PvtEntities.InnerJoin(k => k.ApplicationId == a.Id)
            //                 where a.ServiceId == 13 //Pvt company
            //                       && a.Status != 1002 //Incomplete
            //                       && p.NameId == name.Id
            //                 select a
            //             ).FirstOrDefault();
            //
            //             if (companyApplication == null && DateTime.Now < application.ExpiryDate)
            //             {
            //                 names.Add(new RegisteredNameDto
            //                 {
            //                     NameId = name.Id,
            //                     Ref = application.Reference,
            //                     Name = name.Value,
            //                     DateSubmitted = application.DateSubmitted,
            //                     DateExp = application.ExpiryDate
            //                 });
            //             }
            //         }
            //     }
            //
            //     return Ok(names);
            // }
            return Ok(await _privateEntityService.GetRegisteredNamesAsync(user.Sub));
        }

        [AllowAnonymous]
        [HttpGet("name")]
        public async Task<IActionResult> NameChosenForApplication(int nameId, string industrySector)
        {
            // User user;
            // using (var client = new HttpClient())
            // {
            //     var accessToken = await HttpContext.GetTokenAsync("access_token");
            //     client.SetBearerToken(accessToken);
            //     var response = await client.GetAsync("https://localhost:5001/connect/userinfo").Result.Content
            //         .ReadAsStringAsync();
            //     user = JsonConvert.DeserializeObject<User>(response);
            // }
            //
            // var name = (
            //     from n in _eachDb.Names
            //     from ns in _eachDb.NameSearches.InnerJoin(k => k.Id == n.NameSearchId)
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == ns.ApplicationId)
            //     where n.Id == nameId
            //           && n.Status == 1001
            //           && ns.Service == 13
            //           && ns.ReasonForSearch == 1
            //           && a.UserId == user.Sub
            //     select new
            //     {
            //         n.Id,
            //         n.Value,
            //         n.NameSearchId,
            //         a.SortingOffice
            //     }
            // ).FirstOrDefault();
            //
            // if (name != null)
            // {
            //     var service = (
            //         from value in _poleDb.Services
            //         where value.Description == "private limited company"
            //         select value
            //     ).FirstOrDefault();
            //
            //     int status = 1002;
            //
            //     if (service != null)
            //     {
            //         var entity = (
            //             from p in _eachDb.PvtEntities
            //             where p.NameId != null
            //                   && p.NameId == name.Id
            //             select p
            //         ).FirstOrDefault();
            //
            //         string pvtEntityId = "";
            //         int? applicationId = null;
            //         int entityRecordsInserted = 0;
            //
            //         if (entity == null)
            //         {
            //             applicationId = _eachDb.Applications
            //                 .Value(a => a.UserId, user.Sub)
            //                 .Value(a => a.ServiceId, service.Id)
            //                 .Value(a => a.DateSubmitted, DateTime.Now)
            //                 .Value(a => a.Status, status)
            //                 .Value(a => a.SortingOffice, name.SortingOffice)
            //                 .InsertWithInt32Identity();
            //
            //             if (applicationId != null)
            //             {
            //                 pvtEntityId = Guid.NewGuid().ToString();
            //                 entityRecordsInserted = _eachDb.PvtEntities
            //                     .Value(p => p.Id, pvtEntityId)
            //                     .Value(p => p.ApplicationId, applicationId)
            //                     .Value(p => p.NameId, name.Id)
            //                     .Insert();
            //             }
            //         }
            //         else
            //         {
            //             pvtEntityId = entity.Id;
            //             applicationId = (
            //                 from a in _eachDb.Applications
            //                 where a.Id == entity.ApplicationId
            //                 select a.Id
            //             ).FirstOrDefault();
            //             entityRecordsInserted = 1;
            //         }
            //
            //         var cities = (
            //             from ct in _shwaDb.Cities
            //             where ct.CountryCode.Equals("ZWE")
            //             select new City
            //             {
            //                 Id = ct.ID,
            //                 Name = ct.Name
            //             }).ToList();
            //
            //         var countries = (
            //             from c in _shwaDb.Countries
            //             select new Country
            //             {
            //                 Code = c.Code,
            //                 Name = c.Name
            //             }).ToList();
            //
            //         var gen = (
            //             from g in _poleDb.Genders
            //             select new Gender
            //             {
            //                 Id = g.Id,
            //                 Value = g.Description
            //             }
            //         ).ToList();
            //
            //         foreach (var gender in gen)
            //         {
            //             gender.Format();
            //         }
            //
            //         if (entityRecordsInserted > 0)
            //         {
            //             return Ok(new NewNameSearchApplicationDto
            //             {
            //                 Value = name.Value,
            //                 ApplicationId = applicationId,
            //                 PvtEntityId = pvtEntityId,
            //                 Cities = cities,
            //                 Countries = countries,
            //                 Genders = gen
            //             });
            //         }
            //     }
            // }

            return Ok(await _privateEntityService.CreateApplicationAsync(_user, nameId, industrySector));
        }

        [AllowAnonymous]
        [HttpPost("o")]
        public async Task<IActionResult> SubmitOffice([FromBody] NewPrivateEntityOfficeRequestDto dto)
        {
            // var application = (
            //     from a in _eachDb.Applications
            //     where a.Id == officeInformationDto.ApplicationId
            //     select a
            // ).FirstOrDefault();
            //
            // if (application != null)
            // {
            //     var pvtApplication = (
            //         from pvt in _eachDb.PvtEntities
            //         where pvt.Id == officeInformationDto.PvtEntityId
            //         select pvt
            //     ).FirstOrDefault();
            //
            //     if (pvtApplication != null)
            //     {
            //         if (pvtApplication.OfficeId == null)
            //         {
            //             var officeId = _eachDb.Offices
            //                 .Value(o => o.PhysicalAddress, officeInformationDto.Office.PhysicalAddress)
            //                 .Value(o => o.PostalAddress, officeInformationDto.Office.PostalAddress)
            //                 .Value(o => o.City, officeInformationDto.Office.OfficeCity)
            //                 .Value(o => o.MobileNumber, officeInformationDto.Office.MobileNumber)
            //                 .Value(o => o.TelephoneNumber, officeInformationDto.Office.TelNumber)
            //                 .Value(o => o.EmailAddress, officeInformationDto.Office.EmailAddress)
            //                 .InsertWithInt32Identity();
            //
            //             if (officeId != null)
            //             {
            //                 pvtApplication.OfficeId = officeId;
            //                 if (_eachDb.Update(pvtApplication) == 1)
            //                 {
            //                     return NoContent();
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             var savedOffice = (
            //                 from o in _eachDb.Offices
            //                 where o.Id == pvtApplication.OfficeId
            //                 select o
            //             ).FirstOrDefault();
            //
            //             savedOffice.PhysicalAddress = officeInformationDto.Office.PhysicalAddress;
            //             savedOffice.PostalAddress = officeInformationDto.Office.PostalAddress;
            //             savedOffice.City = officeInformationDto.Office.OfficeCity;
            //             savedOffice.MobileNumber = officeInformationDto.Office.MobileNumber;
            //             savedOffice.TelephoneNumber = officeInformationDto.Office.TelNumber;
            //             savedOffice.EmailAddress = officeInformationDto.Office.EmailAddress;
            //
            //             if (_eachDb.Update(savedOffice) == 1)
            //             {
            //                 return NoContent();
            //             }
            //         }
            //     }
            // }
            //
            // return BadRequest("Something went wrong");

            return Ok(await _privateEntityService.InsertOfficeAsync(_user, dto));
        }

        [AllowAnonymous]
        [HttpPost("memo")]
        public async Task<IActionResult> SubmitClauses([FromBody] NewMemorandumRequestDto dto)
        {
            // var entitity = (
            //     from p in _eachDb.PvtEntities
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == p.ApplicationId)
            //     where a.Id == liabilityShareClausesDto.ApplicationId
            //           && p.Id == liabilityShareClausesDto.PvtEntityId
            //     select p
            // ).FirstOrDefault();
            //
            // if (entitity != null)
            // {
            //     if (entitity.MemorundumId == null)
            //     {
            //         var memoId = _eachDb.Memorundums
            //             .Value(m => m.LiabilityClause, liabilityShareClausesDto.Clauses.LiabilityClause)
            //             .Value(m => m.ShareClause, liabilityShareClausesDto.Clauses.ShareClause)
            //             .InsertWithInt32Identity();
            //
            //         if (memoId != null && memoId > 0)
            //         {
            //             entitity.MemorundumId = memoId;
            //             if (_eachDb.Update(entitity) == 1)
            //                 return Ok(memoId);
            //         }
            //     }
            //     else
            //     {
            //         var memo = (
            //             from m in _eachDb.Memorundums
            //             where m.Id == entitity.MemorundumId
            //             select m
            //         ).FirstOrDefault();
            //
            //         memo.LiabilityClause = liabilityShareClausesDto.Clauses.LiabilityClause;
            //         memo.ShareClause = liabilityShareClausesDto.Clauses.ShareClause;
            //         if (_eachDb.Update(memo) == 1)
            //             return Ok(memo.Id);
            //     }
            // }
            return Ok(await _privateEntityService.InsertMemorandumOfAssociationAsync(_user, dto));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("ob")]
        public async Task<IActionResult> SubmitObjects([FromBody] NewMemorandumOfAssociationObjectsRequestDto dto)
        {
            // var entity = (
            //     from p in _eachDb.PvtEntities
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == p.ApplicationId)
            //     where a.Id == memorandumObjectsDto.ApplicationId
            //           && p.Id == memorandumObjectsDto.PvtEntityId
            //     select p
            // ).FirstOrDefault();
            //
            // if (entity != null)
            // {
            //     if (entity.MemorundumId != null)
            //     {
            //         int count = 0;
            //         foreach (var objective in memorandumObjectsDto.Objects)
            //         {
            //             int? objectiveId = null;
            //             if (objective.Id == 0)
            //             {
            //                 objectiveId = _eachDb.MemoObjects
            //                     .Value(o => o.Value, objective.Objective)
            //                     .Value(o => o.MemorundumId, entity.MemorundumId)
            //                     .InsertWithInt32Identity();
            //             }
            //             else
            //             {
            //                 var objectiveFromDb = (
            //                     from o in _eachDb.MemoObjects
            //                     where o.Id == objective.Id
            //                     select o
            //                 ).FirstOrDefault();
            //
            //                 if (objectiveFromDb != null)
            //                 {
            //                     objectiveFromDb.Value = objective.Objective;
            //                     if (_eachDb.Update(objectiveFromDb) == 1)
            //                         objectiveId = objectiveFromDb.Id;
            //                 }
            //             }
            //
            //             if (objectiveId != null && objectiveId > 0)
            //             {
            //                 count++;
            //                 continue;
            //             }
            //             else
            //                 break;
            //         }
            //
            //         if (count == memorandumObjectsDto.Objects.Count)
            //         {
            //             return NoContent();
            //         }
            //     }
            // }

            return Ok(await _privateEntityService.InsertMemorandumObjectsAsync(_user, dto));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("a")]
        public async Task<IActionResult> SubmitTableArticle([FromBody] NewArticleOfAssociationRequestDto dto)
        {
            // var entity = (
            //     from p in _eachDb.PvtEntities
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == p.ApplicationId)
            //     where a.Id == articleTableDto.ApplicationId
            //           && p.Id == articleTableDto.PvtEntityId
            //     select p
            // ).FirstOrDefault();
            //
            // if (entity != null)
            // {
            //     if (entity.ArticlesId == null)
            //     {
            //         int repackagedArticleId = 0;
            //         short populate = 1;
            //         if (articleTableDto.Table.TableOfArticles.Equals("table A"))
            //         {
            //             var articleId = _eachDb.ArticleOfAssociations
            //                 .Value(a => a.TableA, populate)
            //                 .InsertWithInt32Identity();
            //             if (articleId != null)
            //             {
            //                 repackagedArticleId = (int) articleId;
            //             }
            //         }
            //         else if (articleTableDto.Table.TableOfArticles.Equals("table B"))
            //         {
            //             var articleId = _eachDb.ArticleOfAssociations
            //                 .Value(a => a.TableB, populate)
            //                 .InsertWithInt32Identity();
            //             if (articleId != null)
            //             {
            //                 repackagedArticleId = (int) articleId;
            //             }
            //         }
            //         else
            //         {
            //             var articleId = _eachDb.ArticleOfAssociations
            //                 .Value(a => a.Other, populate)
            //                 .InsertWithInt32Identity();
            //             if (articleId != null)
            //             {
            //                 repackagedArticleId = (int) articleId;
            //             }
            //         }
            //
            //         if (repackagedArticleId > 0)
            //         {
            //             entity.ArticlesId = repackagedArticleId;
            //             if (_eachDb.Update(entity) == 1)
            //                 return NoContent();
            //         }
            //     }
            //     else
            //     {
            //         var articleTable = (
            //             from a in _eachDb.ArticleOfAssociations
            //             where a.Id == entity.ArticlesId
            //             select a
            //         ).FirstOrDefault();
            //
            //         if (articleTable != null)
            //         {
            //             articleTable.TableA = null;
            //             articleTable.TableB = null;
            //             articleTable.Other = null;
            //             short tableValue = 1;
            //             if (articleTableDto.Table.TableOfArticles.Equals("table A"))
            //             {
            //                 articleTable.TableA = tableValue;
            //             }
            //             else if (articleTableDto.Table.TableOfArticles.Equals("table B"))
            //             {
            //                 articleTable.TableB = tableValue;
            //             }
            //             else
            //             {
            //                 articleTable.Other = tableValue;
            //             }
            //
            //             if (_eachDb.Update(articleTable) == 1)
            //                 return NoContent();
            //         }
            //     }
            // }
            return Ok(await _privateEntityService.InsertArticlesOfAssociationAsync(_user, dto));
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("sc")]
        public async Task<IActionResult> SubmitAmendedArticles([FromBody] NewShareClausesRequestDto dto)
        {
            // var entity = (
            //     from p in _eachDb.PvtEntities
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == p.ApplicationId)
            //     where a.Id == amendedArticleDto.ApplicationId
            //           && p.Id == amendedArticleDto.PvtEntityId
            //     select p
            // ).FirstOrDefault();
            //
            // if (entity != null)
            // {
            //     if (entity.ArticlesId != null)
            //     {
            //         int count = 0;
            //         foreach (var amendedArticle in amendedArticleDto.AmendedArticles)
            //         {
            //             int? amendedArticleId = null;
            //             if (amendedArticle.Id == 0)
            //             {
            //                 amendedArticleId = _eachDb.AmmendedArticles
            //                     .Value(a => a.Value, amendedArticle.Article)
            //                     .Value(a => a.ArticleId, entity.ArticlesId)
            //                     .InsertWithInt32Identity();
            //             }
            //             else
            //             {
            //                 var amendedArticleFromDb = (
            //                     from a in _eachDb.AmmendedArticles
            //                     where a.Id == amendedArticle.Id
            //                     select a
            //                 ).FirstOrDefault();
            //
            //                 if (amendedArticleFromDb != null)
            //                 {
            //                     amendedArticleFromDb.Value = amendedArticle.Article;
            //                     if (_eachDb.Update(amendedArticleFromDb) == 1)
            //                         amendedArticleId = amendedArticleFromDb.Id;
            //                 }
            //             }
            //
            //             if (amendedArticleId != null && amendedArticleId > 0)
            //             {
            //                 count++;
            //                 continue;
            //             }
            //             else
            //                 break;
            //         }
            //
            //         if (count == amendedArticleDto.AmendedArticles.Count)
            //         {
            //             return NoContent();
            //         }
            //     }
            // }

            return Ok(await _privateEntityService.InsertShareClauseAsync(_user, dto));

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("p")]
        public async Task<IActionResult> SubmitShareHoldingPeople([FromBody] NewShareHoldersRequestDto dto)
        {
            // var entity = (
            //     from p in _eachDb.PvtEntities
            //     from a in _eachDb.Applications.InnerJoin(k => k.Id == p.ApplicationId)
            //     where a.Id == shareHoldingPersonDto.ApplicationId
            //           && p.Id == shareHoldingPersonDto.PvtEntityId
            //     select p
            // ).FirstOrDefault();
            //
            // if (entity != null)
            // {
            //     int pple = 0;
            //     foreach (var person in shareHoldingPersonDto.People)
            //     {
            //         var subscriberInDb = (
            //             from s in _eachDb.Subcribers
            //             where s.NationalId.Equals(person.NationalId)
            //             select s
            //         ).FirstOrDefault();
            //
            //         if (subscriberInDb == null)
            //         {
            //             int? subscriberId = null;
            //             try
            //             {
            //                 subscriberId = _eachDb.Subcribers
            //                     .Value(s => s.CountryCode, person.NationalId)
            //                     .Value(s => s.NationalId, person.NationalId)
            //                     .Value(s => s.Surname, person.MemberSurname.ToUpper())
            //                     .Value(s => s.FirstName, person.MemberName.ToUpper())
            //                     .Value(s => s.Gender, 1)
            //                     .Value(s => s.PhysicalAddress, person.PhyAddress.ToUpper())
            //                     .InsertWithInt32Identity();
            //             }
            //             catch (Exception ex)
            //             {
            //                 var a = 0;
            //             }
            //
            //             if (subscriberId != null)
            //             {
            //                 short? director = null;
            //                 short? member = null;
            //                 short? secretary = null;
            //
            //                 if (person.IsDirector)
            //                     director = 1;
            //                 if (person.IsMember)
            //                     member = 1;
            //                 if (person.IsSecretary)
            //                     secretary = 1;
            //
            //                 var roleId = _eachDb.Roles.Value(r => r.Director, director).Value(r => r.Member, member)
            //                     .Value(r => r.Secretary, secretary).InsertWithInt32Identity();
            //
            //
            //                 // if (person.IsDirector)
            //                 //     roles.Value(r => r.Director, val);
            //                 // if ()
            //                 //     roles.Value(r => r.Member, val);
            //                 // if (person.IsSecretary)
            //                 //     roles.Value(r => r.Secretary, val);
            //
            //
            //                 if (roleId > 0)
            //                 {
            //                     var subscriptionId = _eachDb.Subscriptions
            //                         .Value(s => s.Ordinary, 100)
            //                         .Value(s => s.Preference, 100)
            //                         .InsertWithInt32Identity();
            //
            //                     if (subscriberId != null)
            //                     {
            //                         var pvtSubscriber = _eachDb.PvtEntityHasSubcribers
            //                             .Value(s => s.Entity, entity.Id)
            //                             .Value(s => s.Subcriber, subscriberId)
            //                             .Value(s => s.RolesId, roleId)
            //                             .Value(s => s.SubscriptionId, subscriptionId)
            //                             .Insert();
            //                         if (pvtSubscriber > 0)
            //                             pple++;
            //                         else
            //                             break;
            //                     }
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             var subscriberHasSub = (
            //                 from s in _eachDb.PvtEntityHasSubcribers
            //                 where s.Entity == entity.Id
            //                       && s.Subcriber == subscriberInDb.Id
            //                 select s
            //             ).FirstOrDefault();
            //
            //             if (subscriberHasSub != null)
            //             {
            //                 subscriberInDb.CountryCode = person.PeopleCountry;
            //                 subscriberInDb.NationalId = person.NationalId;
            //                 subscriberInDb.Surname = person.MemberSurname;
            //                 subscriberInDb.FirstName = person.MemberName;
            //                 //Handle gender here
            //                 subscriberInDb.PhysicalAddress = person.PeopleCountry;
            //
            //                 if (_eachDb.Update(subscriberInDb) == 1)
            //                 {
            //                     var roles = (
            //                         from r in _eachDb.Roles
            //                         where r.Id == subscriberHasSub.RolesId
            //                         select r
            //                     ).FirstOrDefault();
            //
            //                     roles.Director = null;
            //                     roles.Member = null;
            //                     roles.Secretary = null;
            //
            //                     short val = 1;
            //
            //                     if (person.IsDirector)
            //                         roles.Director = val;
            //                     if (person.IsMember)
            //                         roles.Member = val;
            //                     if (person.IsSecretary)
            //                         roles.Secretary = val;
            //
            //                     if (_eachDb.Update(roles) == 1)
            //                     {
            //                         var subscription = (
            //                             from s in _eachDb.Subscriptions
            //                             where s.Id == subscriberHasSub.SubscriptionId
            //                             select s
            //                         ).FirstOrDefault();
            //
            //                         if (subscription != null)
            //                         {
            //                             subscription.Ordinary = long.Parse(person.OrdShares);
            //                             subscription.Ordinary = long.Parse(person.PrefShares);
            //
            //                             if (_eachDb.Update(subscription) == 1)
            //                                 pple++;
            //                             else
            //                                 break;
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }
            //
            //     if (pple == shareHoldingPersonDto.People.Count)
            //         return NoContent();
            // }
            return Ok(await _privateEntityService.InsertMembersAsync(_user, dto));

            return BadRequest();
        }

        [HttpPost("e")]
        public IActionResult SubmitShareHoldingEntities([FromBody] ShareHoldingEntityDto shareHoldingEntityDto)
        {
            return NoContent();
        }

        [AllowAnonymous]
        [HttpHead("s")]
        public async Task<IActionResult> SubmitApplication(int applicationId)
        {
            // var application = (
            //     from a in _eachDb.Applications
            //     where a.Id == applicationId
            //     select a
            // ).FirstOrDefault();
            //
            // if (application != null)
            // {
            //     using (var client = new HttpClient())
            //     {
            //         var paymentDataDto = new PaymentDataDto
            //         {
            //             Email = "brightonkofu@outlook.com",
            //             Service = 2,
            //             UserId = Guid.Parse(application.UserId)
            //         };
            //
            //         //TODO: get email from authority
            //
            //
            //         var responce = await client
            //             .PostAsJsonAsync<PaymentDataDto>("https://localhost:44375/api/Payments/Service",
            //                 paymentDataDto);
            //
            //
            //         if (responce.IsSuccessStatusCode)
            //         {
            //             var status = (
            //                 from s in _poleDb.Status
            //                 where s.Description.Equals("pending")
            //                 select s.Id
            //             ).FirstOrDefault();
            //
            //             if (application.Status != status)
            //             {
            //                 application.Status = status;
            //                 if (_eachDb.Update(application) == 1)
            //                     return Ok();
            //             }
            //         }
            //         else
            //         {
            //             return BadRequest("Insufficient funds");
            //         }
            //     }
            // }

            if (await _privateEntityService.SubmitApplicationAsync(_user, applicationId) > 0)
                return NoContent();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("resubmit/{applicationId}")]
        public async Task<IActionResult> ResubmitApplication(int applicationId)
        {
            var resubmitApplicationAsync = await _privateEntityService.ResubmitApplicationAsync(_user, applicationId);
            if (resubmitApplicationAsync != null)
                return Ok(resubmitApplicationAsync);
            else return BadRequest("Could not save application");
        }
    }
}