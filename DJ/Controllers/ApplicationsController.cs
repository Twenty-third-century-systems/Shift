﻿using System;
using System.Collections.Generic;
using System.Linq;
using DJ.DataModels;
using DJ.Dtos;
using DJ.Models;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application = DJ.Models.Application;
using Task = DJ.Models.Task;

namespace DJ.Controllers {
    [Route("applications")]
    public class ApplicationsController : Controller {
        private EachDB _eachDb;
        private PoleDB _poleDb;

        public ApplicationsController(EachDB eachDb, PoleDB poleDb)
        {
            _eachDb = eachDb;
            _poleDb = poleDb;
        }

        // GET
        [HttpGet("all")]
        public IActionResult All()
        {
            var unAllocatedApplications =
            (
                from application in _eachDb.Applications
                // where application.CreditId != null && ================ impliment after credit system
                where application.TaskId == null
                select application
            ).ToList();

            List<Application> nameSearches = new List<Application>();
            List<Application> pvtEntity = new List<Application>();

            if (unAllocatedApplications.Count > 0)
            {
                foreach (var unAllocatedApplication in unAllocatedApplications)
                {
                    var serviceFromDb =
                    (
                        from serv in _poleDb.Services
                        where serv.Id == unAllocatedApplication.ServiceId
                        select serv
                    ).FirstOrDefault();

                    if (serviceFromDb != null)
                    {
                        string service = serviceFromDb.Description;

                        if (!string.IsNullOrEmpty(service) && service.Equals("name search"))
                        {
                            nameSearches.Add(new Application
                            {
                                Id = unAllocatedApplication.Id,
                                Service = service.ToUpper(),
                                User = unAllocatedApplication.UserId,
                                SubmissionDate =
                                    Convert.ToDateTime(unAllocatedApplication.DateSubmitted.ToString("dd MMM yyy"))
                            });
                        }
                        else
                        {
                            pvtEntity.Add(new Application
                            {
                                Id = unAllocatedApplication.Id,
                                Service = service.ToUpper(),
                                User = unAllocatedApplication.UserId,
                                SubmissionDate =
                                    Convert.ToDateTime(unAllocatedApplication.DateSubmitted.ToString("dd MMM yyyy"))
                            });
                        }
                    }
                }
            }

            AllApplicationsDto allApplications = new AllApplicationsDto
            {
                NameSearchApplications = nameSearches,
                PvtEntityApplications = pvtEntity
            };

            return Ok(allApplications);
        }

        [HttpPost("allocate")]
        public IActionResult AllocateApplicationsToExaminers([FromBody] TaskFromPrincipalDto task)
        {
            if (task.Id == 0)
            {
                var count = 0;
                var service = (
                    from s in _poleDb.Services
                    where s.Description.Equals(task.Service.ToLower())
                    select s
                ).FirstOrDefault();

                if (service != null)
                {
                    var taskId = _eachDb.Task
                        .Value(t => t.ExaminerId, task.Examiner)
                        .Value(t => t.DateAssigned, DateTime.Now)
                        .Value(t => t.AssignedBy, "BK")
                        .Value(t => t.ExpectedDateOfCompletion, task.DateOfCompletion)
                        .InsertWithInt32Identity();

                    if (taskId != null && taskId > 0)
                    {
                        var applications = (
                            from a in _eachDb.Applications
                            where a.ServiceId == service.Id
                                  && a.TaskId == null
                            select a
                        ).ToList();

                        if (applications.Count > 0)
                        {
                            for (int i = 0; i < task.NumberOfApplications; i++)
                            {
                                applications[i].TaskId = taskId;
                                count += _eachDb.Update(applications[i]);
                            }

                            if (count == task.NumberOfApplications)
                            {
                                task.Id = taskId;
                                return Created("", task);
                            }
                        }
                        else
                        {
                            return NotFound("The are no pending applications");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Something went wrong in saving application");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Something went wrong in saving application");
                }
            }
            else
            {
                return BadRequest("Invalid task");
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("allocated")]
        public IActionResult GetTasks()
        {
            var examiner = "ex 1";
            var tasks = (
                from t in _eachDb.Task
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
                                tasksToExaminer.NameSearchTasks.Add(new Task
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
                        return NotFound("No allocated tasks at the moment");
                    }
                }

                return Ok(tasksToExaminer);
            }
            else
            {
                return NotFound("No allocated tasks at the moment");
            }
        }
    }
}