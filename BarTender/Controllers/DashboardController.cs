using System.Net.Http;
using System.Threading.Tasks;
using BarTender.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TurnTable.ExternalServices.Values;

namespace BarTender.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller {
        private readonly IValueService _valueService;

        public DashboardController(IValueService valueService)
        {
            _valueService = valueService;
        }

        // [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetExternalUserDash()
        {
            User user;
            using (var client = new HttpClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.SetBearerToken(accessToken);
                var response = await client.GetAsync("https://localhost:5001/connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    var userDetailsFromAuth = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(userDetailsFromAuth);
                }
                else
                {
                    // TODO: to substitute with NOT ALLOWED
                    return Unauthorized();
                }
            }
            
            return Ok(await _valueService.GetUserDashBoardValuesAsync(user.Sub));
        }
    }
}