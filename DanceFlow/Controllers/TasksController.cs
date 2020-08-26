using System.Net.Http;
using System.Threading.Tasks;
using DanceFlow.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DanceFlow.Controllers {
    [Route("tasks")]
    public class TasksController : Controller {
        // GET
        [HttpGet("allocated")]
        public IActionResult Tasks()
        {
            return View();
        }

        [HttpGet("pending")]
        public async Task<IActionResult> Task(int task)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ApiUrls.AllAllocatedTasks	).Result.Content
                    .ReadAsStringAsync();
                TasksForExaminerDto tasks = JsonConvert.DeserializeObject<TasksForExaminerDto>(response);
                return Ok(tasks);
            }
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