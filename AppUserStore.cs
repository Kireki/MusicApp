using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MusicApp.Models;

namespace MusicApp
{
    public class AppUserStore<TUser> : IUserStore<TUser> where TUser : User
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
