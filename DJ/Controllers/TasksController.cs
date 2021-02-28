using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Cooler.DataModels;
using DJ.Dtos;
using DJ.Models;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using TurnTable.InternalServices;
using Application = Cooler.DataModels.Application;
using Name = DJ.Models.Name;
using NameSearch = DJ.Models.NameSearch;

namespace DJ.Controllers {
    [Route("api/tasks")]
    public class TasksController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;
        private ShwaDB _shwaDb;
        private readonly ITaskService _taskService;

        public TasksController(EachDB eachDb, PoleDB poleDb, ShwaDB shwaDb, ITaskService taskService)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
            _shwaDb = shwaDb;
            _taskService = taskService;
        }

        [AllowAnonymous]
        [HttpGet("{examinerId}")]
        public IActionResult GetTasks(string examinerId)
        {
            var tasks = (
                from t in _eachDb.Tasks
                where t.ExaminerId == examinerId
                select t
            ).ToList();

            if (tasks.Count > 0)
            {
                TasksToExaminerDto tasksToExaminer = new TasksToExaminerDto();

                foreach (var task in tasks)
                {
                    var applications = (
                        from s in _eachDb.Applications
                        where s.TaskId == task.Id
                              && s.DateExamined == null
                        select s
                    ).ToList();

                    if (applications.Count > 0)
                    {
                        var service = (
                            from t in _poleDb.Services
                            where t.Id == applications[0].ServiceId
                            select t.Description
                        ).FirstOrDefault();

                        if (!string.IsNullOrEmpty(service))
                        {
                            if (service.Equals("name search"))
                            {
                                tasksToExaminer.NameSearchTasks.Add(new TaskSummary
                                {
                                    Id = task.Id,
                                    ApplicationCount = applications.Count,
                                    Service = service.ToUpper(),
                                    ExpectedDateOfCompletion = task.ExpectedDateOfCompletion
                                });
                            }
                            else if (service.Equals("private limited company"))
                            {
                                tasksToExaminer.PvtEntityTasks.Add(new TaskSummary
                                {
                                    Id = task.Id,
                                    ApplicationCount = applications.Count,
                                    Service = service.ToUpper(),
                                    ExpectedDateOfCompletion = task.ExpectedDateOfCompletion
                                });
                            }
                            else
                            {
                                return NotFound("No allocated tasks at the moment");
                            }
                        }
                        else
                        {
                            return NotFound("No allocated tasks at the moment");
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                return Ok(tasksToExaminer);
            }
            else
            {
                return NotFound("No allocated tasks at the moment");
            }
        }

        [HttpGet("{taskId}/ns")]
        public IActionResult GetNameSearchTask(int taskId)
        {
            List<NameSearchTaskDto> nameSearchTasks = new List<NameSearchTaskDto>();

            var applications = (
                from application in _eachDb.Applications
                where application.TaskId == taskId
                select application
            ).ToList();

            if (applications.Count > 0)
            {
                foreach (var application in applications)
                {
                    var nameSearch = (
                        from n in _eachDb.NameSearches
                        where n.ApplicationId == application.Id
                        select n
                    ).FirstOrDefault();

                    if (nameSearch != null)
                    {
                        var names = (
                            from name in _eachDb.Names
                            where name.NameSearchId == nameSearch.Id
                            select name
                        ).ToList();

                        var service = (
                            from serv in _poleDb.Services
                            where serv.Id == application.ServiceId
                            select serv.Description
                        ).FirstOrDefault();

                        if (names.Count > 0)
                        {
                            var nameSearchTaskDto = new NameSearchTaskDto
                            {
                                Application = new Models.Application
                                {
                                    Id = application.Id,
                                    Service = service,
                                    User = application.UserId,
                                    SubmissionDate = application.DateSubmitted,
                                    Examined = application.DateExamined == null ? false : true
                                },
                                NameSearch = new NameSearch
                                {
                                    Id = nameSearch.Id,
                                    ReasonForSearch = (
                                        from r in _poleDb.ReasonForSearches
                                        where r.Id == nameSearch.ReasonForSearch
                                        select r.Description
                                    ).FirstOrDefault(),
                                    TypeOfEntity = (
                                        from s in _poleDb.Services
                                        where s.Id == nameSearch.Service
                                        select s.Description
                                    ).FirstOrDefault(),
                                    Designation = (
                                        from designation in _poleDb.Designations
                                        where designation.Id == nameSearch.DesignationId
                                        select designation.Description
                                    ).FirstOrDefault(),
                                    Justification = nameSearch.Justification
                                }
                            };

                            foreach (var name in names)
                            {
                                nameSearchTaskDto.NameSearch.Names.Add(new Name
                                {
                                    Id = name.Id,
                                    Value = name.Value,
                                    Status = (
                                        from s in _poleDb.Status
                                        where s.Id == name.Status
                                        select s.Description
                                    ).FirstOrDefault(),
                                    NameSearchId = name.NameSearchId
                                });
                            }

                            nameSearchTasks.Add(nameSearchTaskDto);
                        }
                    }
                    else
                    {
                        return BadRequest("Task has incorrect data");
                    }
                }
            }
            else
            {
                return BadRequest("Task is Empty");
            }

            return Ok(nameSearchTasks);
        }

        [AllowAnonymous]
        [HttpGet("{taskId}/pla")]
        public async Task<IActionResult> GetPvtApplicationTask(int taskId)
        {
            // List<PvtApplicationTaskDto> pvtApplicationTasks = new List<PvtApplicationTaskDto>();
            //
            // var applications = (
            //     from application in _eachDb.Applications
            //     where application.TaskId == taskId
            //     select application
            // ).ToList();
            //
            // if (applications.Count > 0)
            // {
            //     foreach (var application in applications)
            //     {
            //         PvtApplicationTaskDto applicationTaskDto = new PvtApplicationTaskDto();
            //         var service = (
            //             from s in _poleDb.Services
            //             where s.Id == application.ServiceId
            //             select s.Description
            //         ).FirstOrDefault();
            //         if (service != null)
            //         {
            //             applicationTaskDto.Application = new Models.Application
            //             {
            //                 Id = application.Id,
            //                 Service = service,
            //                 User = application.UserId,
            //                 SubmissionDate = application.DateSubmitted,
            //                 Examined = false
            //             };
            //
            //             if (application.DateExamined != null)
            //             {
            //                 applicationTaskDto.Application.Examined = true;
            //             }
            //         }
            //
            //         var pvtEntity = (
            //             from p in _eachDb.PvtEntities
            //             where p.ApplicationId == application.Id
            //             select p
            //         ).FirstOrDefault();
            //
            //         if (pvtEntity != null)
            //         {
            //             if (pvtEntity.NameId == null)
            //             {
            //                 break;
            //             }
            //
            //             var name = (
            //                 from n in _eachDb.Names
            //                 where n.Id == pvtEntity.NameId
            //                 select n
            //             ).FirstOrDefault();
            //             var status = (
            //                 from s in _poleDb.Status
            //                 where s.Id == application.Status
            //                 select s.Description
            //             ).FirstOrDefault();
            //             applicationTaskDto.Name = new Name
            //             {
            //                 Id = name.Id,
            //                 Value = name.Value,
            //                 Status = status,
            //                 NameSearchId = name.NameSearchId
            //             };
            //
            //             if (pvtEntity.OfficeId == null)
            //             {
            //                 break;
            //             }
            //
            //             var office = (
            //                 from o in _eachDb.Offices
            //                 where o.Id == pvtEntity.OfficeId
            //                 select o
            //             ).FirstOrDefault();
            //             if (office != null)
            //             {
            //                 var city = (
            //                     from c in _shwaDb.Cities
            //                     where c.ID == office.City
            //                     select c.Name
            //                 ).FirstOrDefault();
            //                 applicationTaskDto.RegisteredRegOffice = new RegOffice
            //                 {
            //                     PhysicalAddress = office.PhysicalAddress,
            //                     PostalAddress = office.PostalAddress,
            //                     OfficeCity = city,
            //                     EmailAddress = office.EmailAddress,
            //                     TelNumber = office.TelephoneNumber,
            //                     MobileNumber = office.MobileNumber
            //                 };
            //             }
            //
            //             if (pvtEntity.ArticlesId == null)
            //             {
            //                 break;
            //             }
            //
            //             var article = (
            //                 from a in _eachDb.ArticleOfAssociations
            //                 where a.Id == pvtEntity.ArticlesId
            //                 select a
            //             ).FirstOrDefault();
            //             PvtApplicationArticlesOfAssociation articlesOfAssociation =
            //                 new PvtApplicationArticlesOfAssociation();
            //             if (article != null)
            //             {
            //                 if (article.Other != null)
            //                 {
            //                     articlesOfAssociation.ArticleTable = new ArticleTable
            //                     {
            //                         TableOfArticles = "Other"
            //                     };
            //                     var amendedArticles = (
            //                         from a in _eachDb.AmmendedArticles
            //                         where a.ArticleId == article.Id
            //                         select a
            //                     ).ToList();
            //                     if (amendedArticles.Count > 0)
            //                     {
            //                         foreach (var a in amendedArticles)
            //                         {
            //                             articlesOfAssociation.AmendedArticles.Add(
            //                                 new AmendedArticle
            //                                 {
            //                                     Article = a.Value
            //                                 });
            //                         }
            //                     }
            //                 }
            //
            //                 if (article.TableA != null)
            //                 {
            //                     articlesOfAssociation.ArticleTable = new ArticleTable
            //                     {
            //                         TableOfArticles = "Table A"
            //                     };
            //                 }
            //                 else
            //                 {
            //                     articlesOfAssociation.ArticleTable = new ArticleTable
            //                     {
            //                         TableOfArticles = "Table B"
            //                     };
            //                 }
            //             }
            //
            //
            //             applicationTaskDto.ArticlesOfAssociation = articlesOfAssociation;
            //
            //             if (pvtEntity.MemorundumId == null)
            //             {
            //                 break;
            //             }
            //
            //             var memorandum = (
            //                 from m in _eachDb.Memorundums
            //                 where m.Id == pvtEntity.MemorundumId
            //                 select m
            //             ).FirstOrDefault();
            //             if (memorandum != null)
            //             {
            //                 MemorandumOfAssociation memorandumOfAssociation = new MemorandumOfAssociation();
            //                 memorandumOfAssociation.LiabilityShareClauses = new LiabilityShareClauses
            //                 {
            //                     LiabilityClause = memorandum.LiabilityClause,
            //                     ShareClause = memorandum.ShareClause
            //                 };
            //                 var objects = (
            //                     from m in _eachDb.MemoObjects
            //                     where m.MemorundumId == memorandum.Id
            //                     select m
            //                 ).ToList();
            //                 if (objects.Count > 0)
            //                 {
            //                     foreach (var memoObject in objects)
            //                     {
            //                         memorandumOfAssociation.Objectives.Add(new SingleObjective
            //                         {
            //                             Objective = memoObject.Value
            //                         });
            //                     }
            //                 }
            //
            //                 applicationTaskDto.MemorandumOfAssociation = memorandumOfAssociation;
            //             }
            //
            //             var subscriptions = (
            //                 from s in _eachDb.PvtEntityHasSubcribers
            //                 where s.Entity == pvtEntity.Id
            //                 select s
            //             ).ToList();
            //
            //             if (subscriptions.Count > 0)
            //             {
            //                 foreach (var subscription in subscriptions)
            //                 {
            //                     var subscriber = (
            //                         from s in _eachDb.Subcribers
            //                         where s.Id == subscription.Subcriber
            //                         select s
            //                     ).FirstOrDefault();
            //
            //                     if (subscriber != null)
            //                     {
            //                         var country = (
            //                             from c in _shwaDb.Countries
            //                             where c.Code == "ZWE"
            //                             select c.Name
            //                         ).FirstOrDefault();
            //
            //                         var roles = (
            //                             from r in _eachDb.Roles
            //                             where r.Id == subscription.RolesId
            //                             select r
            //                         ).FirstOrDefault();
            //
            //                         var subsAmounts = (
            //                             from s in _eachDb.Subscriptions
            //                             where s.Id == subscription.SubscriptionId
            //                             select s
            //                         ).FirstOrDefault();
            //
            //                         if (country != null && roles != null && subsAmounts != null)
            //                         {
            //                             applicationTaskDto.ShareHolders.Add(new ShareHolderPerson
            //                             {
            //                                 PeopleCountry = country,
            //                                 NationalId = subscriber.NationalId,
            //                                 MemberSurname = subscriber.Surname,
            //                                 MemberName = subscriber.FirstName,
            //                                 Gender = "Male",
            //                                 PhyAddress = subscriber.PhysicalAddress,
            //                                 IsSecretary = roles.Secretary == null ? true : false,
            //                                 IsMember = roles.Member == null ? true : false,
            //                                 IsDirector = roles.Director == null ? true : false,
            //                                 OrdShares = subsAmounts.Ordinary,
            //                                 PrefShares = subsAmounts.Preference
            //                             });
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //
            //         pvtApplicationTasks.Add(applicationTaskDto);
            //     }
            // }
            //
            // if (pvtApplicationTasks.Count > 0)
            // {
            //     return Ok(pvtApplicationTasks);
            // }
            return Ok(await _taskService.GetPrivateEntityTaskApplicationAsync(taskId));

            return BadRequest();
        }
    }
}