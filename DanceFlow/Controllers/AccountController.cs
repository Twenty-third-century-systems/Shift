using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanceFlow.Controllers {
    [Authorize]
    [Route("account")]
    public class AccountController : Controller {
        [HttpGet("sign-out")]
        public IActionResult SignOut()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44372"
            }, "ExamCookie", "ExamOidc");
        }
    }
}