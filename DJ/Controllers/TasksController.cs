using System.Linq;
using DJ.DataModels;
using DJ.Dtos;
using DJ.Models;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Task = DJ.Models.Task;

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

        [HttpGet("{taskId}")]
        public IActionResult GetTask(int taskId)
        {
            var application = (
                    from c in _eachDb.Applications
                    join d in _eachDb.NameSearches on c.Id equals d.ApplicationId
                    join e in _eachDb.Name on d.Id equals e.NameSearchId
                    where c.TaskId == taskId
                        select new
                        {
                            c,
                            d,
                            e
                        }
                ).ToList(); 
            return Ok(application);
        }
    }
}