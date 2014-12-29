﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public string UserName
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

        public string FirstLast
        {
            get
            {
                foreach (var claim in Claims)
                {
                    Debug.WriteLine(claim.Value + " " + claim.Type);
                }
                return this.FindFirst("FirstLast").Value;
            }
        }
    }
}
