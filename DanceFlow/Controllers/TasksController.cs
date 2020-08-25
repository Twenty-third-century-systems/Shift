using Microsoft.AspNetCore.Mvc;

namespace DanceFlow.Controllers {
    [Route("tasks")]
    public class TasksController : Controller {
        // GET
        [HttpGet("allocated")]
        public IActionResult Tasks()
        {
            return View();
        }

        [HttpGet("{task}")]
        public IActionResult Task(int task)
        {
            return View();
        }

        [HttpGet("name-search/{task}/{nameSearch}")]
        public IActionResult NameSearch(int task, string nameSearch)
        {
            return Ok();
        }
        
        [HttpGet("pvt-entity/{task}/{nameSearch}")]
        public IActionResult PvtEntity(int task, string nameSearch)
        {
            return Ok();
        }
    }
}