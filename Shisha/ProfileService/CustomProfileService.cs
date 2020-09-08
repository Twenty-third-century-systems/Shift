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
using Microsoft.EntityFrameworkCore;
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

            if (_db.ExternalUsers.Where(a => a.UserId == user.Id).FirstOrDefault() != null)
            {
                var User = _db.ExternalUsers
                    .Include(c => c.UserDetails)
                    .Include(c=>c.Policies)
                    .Where(a => a.UserId == user.Id)
                    .FirstOrDefault();
                claims.Add(new Claim(JwtClaimTypes.Name, User.UserDetails.Names + " " + User.UserDetails.Surname));
                foreach (var policy in User.Policies)
                {
                    claims.Add(new Claim("Policy", policy.Value));
                }
            }
            else
            {
                var User = _db.InternalUsers
                    .Include(c => c.UserDetails)
                    .Where(a => a.UserId == user.Id)
                    .FirstOrDefault();
                claims.Add(new Claim(JwtClaimTypes.Name, User.UserDetails.Names + " " + User.UserDetails.Surname));
            }

            claims.Add(new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles),
                IdentityServerConstants.ClaimValueTypes.Json));

            // _db.Policies.Where(q => q. = user.Id).ToList();
            // foreach (var policy in user.Policies)
            // {
            //     claims.Add(new Claim("Policy", policy.Value));
            // }

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