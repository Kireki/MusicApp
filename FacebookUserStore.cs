using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MusicApp.Models;

namespace MusicApp
{
    public class FacebookUserStore<TUser> : IUserLoginStore<TUser> where TUser : FacebookUser
    {
    }
}
