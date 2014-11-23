using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;


namespace MusicApp
{
    public class AppUser : IdentityUser
    {
        public int Age { get; set; }
        public string Country { get; set; }
    }
}
