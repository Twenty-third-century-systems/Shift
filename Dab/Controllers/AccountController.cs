using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dab.Controllers {
    [Route("account")]
    public class AccountController : Controller {
        [HttpGet("sign-out")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44381"
            }, "Cookies", "oidc");
        }
    }
}