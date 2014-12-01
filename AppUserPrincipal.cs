using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(ClaimsPrincipal principal) : base(principal)
        {
        }

        public int Id
        {
            get
            {
                int id;
                int.TryParse(this.FindFirst("Id").Value, out id);
                return id;
            }
        }

        public string Name
        {
            get { return this.FindFirst(ClaimTypes.Name).Value; }
        }

        public string FacebookUserId
        {
            get { return this.FindFirst("FacebookUserId").Value; }
        }

        public string FacebookAccessToken
        {
            get { return this.FindFirst("FacebookAccessToken").Value; }
        }
    }
}
