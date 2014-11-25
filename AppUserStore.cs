using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicApp.Models;

namespace MusicApp
{
    public class AppUserStore : IUserPasswordStore<User, int>
    {
        private readonly AppDbContext _dbContext;

        public AppUserStore(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CheckUserArg(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task CreateAsync(User user)
        {
            CheckUserArg(user);

            return Task.Factory.StartNew(() =>
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChangesAsync();
            });
        }

        public Task UpdateAsync(User user)
        {
            CheckUserArg(user);

            return Task.Factory.StartNew(() =>
            {
                var updatable = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
                if (updatable != null) { _dbContext.Entry(updatable).CurrentValues.SetValues(user); }
                _dbContext.SaveChangesAsync();
            });
        }

        public Task DeleteAsync(User user)
        {
            CheckUserArg(user);

            return Task.Factory.StartNew(() =>
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChangesAsync();
            });
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.FromResult(_dbContext.Users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Task.FromResult(_dbContext.Users.FirstOrDefault(u => u.UserName == userName));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            CheckUserArg(user);
            return Task.Factory.StartNew(() =>
            {
                user.PasswordHash = passwordHash;
            });
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            CheckUserArg(user);
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            CheckUserArg(user);
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
