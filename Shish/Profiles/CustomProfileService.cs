using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Shish.Data;
using Shish.Models;

namespace Shish.Profiles {
    public class CustomProfileService : IProfileService {
        protected UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _db;

        public CustomProfileService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext
        )
        {
            _userManager = userManager;
            _db = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var personal = _db.PersonalInfo.First(a => a.UserId == user.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, personal.Names + " " + personal.Surname),
                new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles),
                    IdentityServerConstants.ClaimValueTypes.Json),
                new Claim("Policy", user.Policy)
            };
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = true;
            // (user != null) && user.IsActive;
        }
    }
}