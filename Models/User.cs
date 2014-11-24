using System;
using Microsoft.AspNet.Identity;

namespace MusicApp.Models
{
    public class User : IUser
    {
        public String Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
