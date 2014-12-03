using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MusicApp
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected UserPrincipal CurrentUserClaims
        {
            get
            {
                return new UserPrincipal(this.User as ClaimsPrincipal);
            }
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}
