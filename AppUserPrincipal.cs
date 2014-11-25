﻿using System;
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

        public string Name
        {
            get { return this.FindFirst(ClaimTypes.Name).Value; }
        }

        public string FacebookUser
        {
            get { return this.FindFirst("FacebookUser").Value; }
        }
    }
}
