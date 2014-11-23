using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace MusicApp
{
    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<AppUser, string>
    {
        public async override Task<ClaimsIdentity> CreateAsync(UserManager<AppUser, string> manager,
            AppUser user, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            return identity;
        }
    }
}
