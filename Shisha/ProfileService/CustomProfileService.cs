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
using Shisha.Data;
using Shisha.Models;

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
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>();

            if (_db.ExternalUsers.First(a => a.UserId == user.Id) == null)
            {
                var User = _db.ExternalUsers.First(a => a.UserId == user.Id);
                claims.Add(new Claim(JwtClaimTypes.Name, User.UserDetails.Names + " " + User.UserDetails.Surname));
            }
            else
            {
                var User = _db.InternalUsers.First(a => a.UserId == user.Id);
                claims.Add(new Claim(JwtClaimTypes.Name, User.UserDetails.Names + " " + User.UserDetails.Surname));
            }

            claims.Add(new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles),
                IdentityServerConstants.ClaimValueTypes.Json));
            foreach (var policy in user.Policies)
            {
                claims.Add(new Claim("Policy",policy.Value));
            }

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