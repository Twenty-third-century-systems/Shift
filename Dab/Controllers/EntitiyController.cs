using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Dtos;
using Dab.Dtos;
using Dab.Globals;
using Dab.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dab.Controllers {
    [Route("entity")]
    public class EntitiyController : Controller {
        // GET
        [HttpGet("{nameId}/new")]
        public async Task<IActionResult> NewPrivateEntity(int nameId)
        {
            var nameClaim = User.Claims.Where(c => c.Type.Equals("name") && c.Issuer.Equals("https://localhost:5001"))
                .FirstOrDefault();
            ViewBag.User = nameClaim.Value;

            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var responce = await client.GetAsync($"{ApiUrls.NameOnApplication}/{nameId}/name").Result.Content.ReadAsStringAsync();
                var name = JsonConvert.DeserializeObject<Name>(responce);
                ViewBag.NameRacho = name.Value;
            }

            return View();
        }

        [HttpGet("names/reg")]
        public async Task<IActionResult> RegisteredNames()
        {
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync(ApiUrls.RegisteredNames).Result.Content
                    .ReadAsStringAsync();
                return Ok(JsonConvert.DeserializeObject<List<RegisteredNameDto>>(response));
            }
        }
    }
}