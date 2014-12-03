using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public abstract class AppController : Controller
    {
        public UserPrincipal CurrentUserClaims
        {
            get
            {
                return new UserPrincipal(this.User as ClaimsPrincipal);
            }
        }
    }
}