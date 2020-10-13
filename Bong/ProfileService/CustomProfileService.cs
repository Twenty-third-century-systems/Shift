using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Bong.Data;
using Bong.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bong.ProfileService {
    public class CustomProfileService : IProfileService {
        private UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public CustomProfileService(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext
        )
        {
            _context = dbContext;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var savedUser = _context.Users
                .Include(u => u.Policies)
                .Where(u => u.Id.Equals(user.Id))
                .FirstOrDefault();
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            var policies = new List<string>();
            foreach (var policy in savedUser.Policies)
            {
                var polcy = _context.Policies.Where(p => p.Id == policy.PolicyId).FirstOrDefault();
                policies.Add(polcy.Value);
            }

            claims.Add(new Claim(JwtClaimTypes.Name, savedUser.Firstname + " " + savedUser.Surname));
            claims.Add(new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles),
                IdentityServerConstants.ClaimValueTypes.Json));
            claims.Add(new Claim("policies", JsonSerializer.Serialize(policies),
                IdentityServerConstants.ClaimValueTypes.Json));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
            //Todo implement method
        }
    }
}