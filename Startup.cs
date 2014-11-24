using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MusicApp.Models;
using Owin;

namespace MusicApp
{
    public class Startup
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/home/login")
            });

            UserManagerFactory = () =>
            {
                var userManager = new UserManager<User>(
                    new AppUserStore())
                userManager.UserValidator = new UserValidator<User>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                userManager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();
                return userManager;
            };
        }
    }
}
