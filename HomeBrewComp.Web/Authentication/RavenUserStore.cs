using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Raven.Client;
using HomeBrewComp.Domain;
using HomeBrewComp.Web.Models;
using HomeBrewComp.Indexes;

namespace HomeBrewComp.Web.Authentication
{
    public class RavenUserStore : IUserStore<User>, IUserLoginStore<User>, IUserPasswordStore<User>, IUserLockoutStore<User, string>
    {
        private readonly IDocumentSession Session;

        public RavenUserStore(IDocumentSession session)
        {
            this.Session = session;
        }

        public Task CreateAsync(User user)
        {
            Session.Store(user);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            var user = Session.Load<User>(userId);
            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var user = Session.Query<User>().SingleOrDefault(u => u.UserName == userName);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user)
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            if (Session == null) // only possible if this is partially constructed
                return;

            Session.SaveChanges();
            Session.Dispose();
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            user.Logins.Add(new Login(login.LoginProvider, login.ProviderKey));
            return Task.FromResult(0);
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            var user = Session.Query<User>()
                .SingleOrDefault(u => u.Logins.Any(l => l.Provider == login.LoginProvider &&
                    l.ProviderKey == login.ProviderKey));

            return Task.FromResult(user);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            IList<UserLoginInfo> loginInfo = user.Logins.Select(login => new UserLoginInfo(login.Provider, login.ProviderKey))
                .ToList();

            return Task.FromResult(loginInfo);
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            var toRemove = user.Logins.Single(l => l.Provider == login.LoginProvider);
            user.Logins.Remove(toRemove);
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            bool hasPassword = !string.IsNullOrEmpty(user.PasswordHash);
            return Task.FromResult(hasPassword);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(true);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(user.LockoutEndDate);
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(user.AccessFailedCount++);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDate = lockoutEnd;
            return Task.FromResult(0);
        }
    }
}