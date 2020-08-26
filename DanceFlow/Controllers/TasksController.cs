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

        [HttpGet("name-search/{task}")]
        public IActionResult NameSearches(int task)
        {
            
            return View();
        }
        
        [HttpGet("pvt-entity/{task}")]
        public IActionResult PvtEntity(int task)
        {
            return View();
        }
    }
}