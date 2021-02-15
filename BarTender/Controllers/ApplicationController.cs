using System.Collections.Generic;
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
        private ShwaDB _shwaDb;

        public ApplicationController(EachDB eachDb, ShwaDB shwaDb)
        {
            _shwaDb = shwaDb;
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

        [AllowAnonymous]
        [HttpGet("pvt/sum/{applicationId}")]
        public IActionResult GetRegisteredPrivateEntitySummary(int applicationId)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == applicationId
                select a).SingleOrDefault();

            if (application != null)
            {
                var entity = (
                    from p in _eachDb.PvtEntities
                    where p.ApplicationId == application.Id
                    select p
                ).SingleOrDefault();

                if (entity != null)
                {
                    var office = (
                        from o in _eachDb.Offices
                        where o.Id == entity.OfficeId
                        select o
                    ).SingleOrDefault();

                    if (office != null)
                    {
                        var name = (
                            from n in _eachDb.Names
                            where n.Id == entity.NameId
                            select n
                        ).SingleOrDefault();

                        if (name != null)
                        {
                            var subscribers = (
                                from s in _eachDb.PvtEntityHasSubcribers
                                where s.Entity == entity.Id
                                select s
                            ).ToList();

                            if (subscribers.Count > 0)
                            {
                                var memorundum = (
                                    from m in _eachDb.Memorundums
                                    where m.Id == entity.MemorundumId
                                    select m
                                ).FirstOrDefault();

                                if (memorundum != null)
                                {
                                    var memoObjects = (
                                        from o in _eachDb.MemoObjects
                                        where o.MemorundumId == memorundum.Id
                                        select o
                                    ).ToList();

                                    if (memoObjects.Count > 0)
                                    {
                                        var articleOfAssociation = (
                                            from a in _eachDb.ArticleOfAssociations
                                            where a.Id == entity.ArticlesId
                                            select a
                                        ).SingleOrDefault();

                                        if (articleOfAssociation != null)
                                        {
                                            List<AmmendedArticle> ammendedArticles = new List<AmmendedArticle>();
                                            if (articleOfAssociation.Other != null)
                                            {
                                                ammendedArticles = (
                                                    from a in _eachDb.AmmendedArticles
                                                    where a.ArticleId == articleOfAssociation.Id
                                                    select a
                                                ).ToList();
                                            }

                                            return Ok(
                                                ConstructDto(
                                                    application,
                                                    entity,
                                                    name,
                                                    office,
                                                    memorundum,
                                                    memoObjects,
                                                    articleOfAssociation,
                                                    ammendedArticles,
                                                    subscribers)
                                            );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet("pvt/cert/{applicationId}")]
        public IActionResult GetRegisteredPrivateEntityCertificate(int applicationId)
        {
            var application = (
                from a in _eachDb.Applications
                where a.Id == applicationId
                select a).SingleOrDefault();

            if (application != null && application.ServiceId == 13 && application.DateExamined != null)
            {
                var pvtEntityNameIdAndReference = (
                    from p in _eachDb.PvtEntities
                    where p.ApplicationId == applicationId
                    select new
                    {
                        p.NameId,
                        p.Reference
                    }).SingleOrDefault();

                if (pvtEntityNameIdAndReference.NameId != null && pvtEntityNameIdAndReference.Reference != null)
                {
                    var name = (
                        from n in _eachDb.Names
                        where n.Id == pvtEntityNameIdAndReference.NameId.Value
                        select n.Value).SingleOrDefault();

                    if (name != null)
                    {
                        return Ok(new RegisteredPvtEntityDto
                        {
                            EntityName = name,
                            Reference = pvtEntityNameIdAndReference.Reference,
                            DateRegistered = application.DateExamined.Value
                        });
                    }
                }
            }

            return NotFound();
        }

        private PvtEntitySummaryDocDto ConstructDto(
            Application application,
            PvtEntity entity,
            Name name,
            Office office,
            Memorundum memorundum,
            List<MemoObject> memoObjects,
            ArticleOfAssociation articleOfAssociation,
            List<AmmendedArticle> ammendedArticle,
            List<PvtEntityHasSubcriber> subscribers)
        {
            PvtEntitySummaryDocDto.Company company = new PvtEntitySummaryDocDto.Company
            {
                Ref = entity.Reference,
                Name = name.Value,
                DateOfIncorporation = application.DateExamined.Value.ToString("d")
            };

            PvtEntitySummaryDocDto.Address address = new PvtEntitySummaryDocDto.Address
            {
                SituatedAt = office.PhysicalAddress,
                PostalAddress = office.PostalAddress,
                EmailAddress = office.EmailAddress
            };

            PvtEntitySummaryDocDto dto = new PvtEntitySummaryDocDto
            {
                EntitySummary = company,
                EntityAddress = address
            };

            foreach (var subscriber in subscribers)
            {
                var subcriber = (
                    from s in _eachDb.Subcribers
                    where s.Id == subscriber.Subcriber
                    select s
                ).SingleOrDefault();

                if (subcriber != null)
                {
                    var role = (
                        from r in _eachDb.Roles
                        where r.Id == subscriber.RolesId
                        select r
                    ).SingleOrDefault();

                    if (role != null)
                    {
                        var subscription = (
                            from s in _eachDb.Subscriptions
                            where s.Id == subscriber.SubscriptionId
                            select s
                        ).SingleOrDefault();

                        if (subscription != null)
                        {
                            if (role.Director != null)
                            {
                                var countryName = (
                                    from c in _shwaDb.Countries
                                    where c.Code == subcriber.CountryCode
                                    select c.Name
                                ).SingleOrDefault();

                                dto.Directors.Add(new PvtEntitySummaryDocDto.Principal
                                {
                                    DateOfIncorporation = application.DateExamined.Value.ToString("d"),
                                    ChristianNames = $"{subcriber.FirstName} {subcriber.Surname}",
                                    Ids = subcriber.NationalId,
                                    Nationality = subcriber.CountryCode,
                                    ResidentialAddress = subcriber.PhysicalAddress
                                });
                            }
                            else if (role.Secretary != null)
                            {
                                var countryName = (
                                    from c in _shwaDb.Countries
                                    where c.Code == subcriber.CountryCode
                                    select c.Name
                                ).SingleOrDefault();

                                dto.Secretary.Add(new PvtEntitySummaryDocDto.Principal
                                {
                                    DateOfIncorporation = application.DateExamined.Value.ToString("d"),
                                    ChristianNames = $"{subcriber.FirstName} {subcriber.Surname}",
                                    Ids = subcriber.NationalId,
                                    Nationality = subcriber.CountryCode,
                                    ResidentialAddress = subcriber.PhysicalAddress
                                });
                            }

                            dto.Subscribers.Add(new PvtEntitySummaryDocDto.Subscriber
                            {
                                FullNamesAndIds = $"{subcriber.FirstName} {subcriber.Surname} {subcriber.NationalId}",
                                OrdinaryShares = $"{subscription.Ordinary.Value}",
                                PreferenceShares = $"{subscription.Preference.Value}",
                                TotalShares = $"{subscription.Ordinary.Value + subscription.Preference.Value}"
                            });
                        }
                    }
                }
            }

            dto.LiabilityClause = memorundum.LiabilityClause;
            dto.ShareCapital = memorundum.ShareClause;

            if (memoObjects.Count > 0)
                dto.MajorObject = memoObjects.FirstOrDefault().Value;

            if (articleOfAssociation.Other == null)
            {
                foreach (var article in ammendedArticle)
                {
                    dto.ArticlesOfAssociation.Add(article.Value);
                }
            }
            else if (articleOfAssociation.TableA != null)
            {
                dto.ArticlesOfAssociation.Add("TABLE A");
            }
            else
            {
                dto.ArticlesOfAssociation.Add("TABLE B");
            }

            return dto;
        }
    }
}