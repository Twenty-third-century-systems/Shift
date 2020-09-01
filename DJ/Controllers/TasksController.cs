using System.Collections.Generic;
using System.Linq;
using Cooler.DataModels;
using DJ.Dtos;
using DJ.Models;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Application = Cooler.DataModels.Application;
using Name = DJ.Models.Name;
using NameSearch = DJ.Models.NameSearch;

namespace DJ.Controllers {
    [Route("api/tasks")]
    public class TasksController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;

        public TasksController(EachDB eachDb, PoleDB poleDb)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        [HttpGet("")]
        public IActionResult GetTasks()
        {
            var examiner = "ex 1";
            var tasks = (
                from t in _eachDb.Tasks
                where t.ExaminerId == examiner
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

        [HttpGet("{taskId}")]
        public IActionResult GetTask(int taskId)
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
    }
}