using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MusicApp.Models;

namespace MusicApp
{
    public class AppUserClaimsIdentityFactory : ClaimsIdentityFactory<User, int>
    {
        public async override Task<ClaimsIdentity> CreateAsync(UserManager<User, int> manager,
            User user, string authenticationType)
        {
            var identity = await base.CreateAsync(manager, user, authenticationType);
            identity.AddClaim(new Claim("FacebookUserId", user.FacebookUserId));
            identity.AddClaim(new Claim("FacebookAccessToken", user.FacebookAccessToken));
            identity.AddClaim(new Claim("Id", user.Id.ToString(CultureInfo.InvariantCulture)));
            return identity;
        }
    }
}
