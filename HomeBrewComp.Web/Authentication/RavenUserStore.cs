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
        private readonly IAsyncDocumentSession Session;

        public RavenUserStore(IAsyncDocumentSession session)
        {
            this.Session = session;
        }

        public async Task CreateAsync(User user)
        {
            await Session.StoreAsync(user);
            await Session.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await Session.LoadAsync<User>(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await Session.Query<User>().SingleOrDefaultAsync(u => u.UserName == userName);
        }

        // WTF is this supposed to do? UoW is tracking changes, so just flush it here?
        public async Task UpdateAsync(User user)
        {
            await Session.SaveChangesAsync();
        }

        public void Dispose()
        {

        }

        public async Task AddLoginAsync(User user, UserLoginInfo login)
        {
            user.Logins.Add(new Login(login.LoginProvider, login.ProviderKey));
            await Session.SaveChangesAsync();
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            var user = await Session.Query<User>()
                .SingleOrDefaultAsync(u => u.Logins.Any(l => l.Provider == login.LoginProvider &&
                    l.ProviderKey == login.ProviderKey));

            return user;
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            IList<UserLoginInfo> loginInfo = user.Logins.Select(login => new UserLoginInfo(login.Provider, login.ProviderKey))
                .ToList();

            return Task.FromResult(loginInfo);
        }

        public async Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            var toRemove = user.Logins.Single(l => l.Provider == login.LoginProvider);
            user.Logins.Remove(toRemove);
            await Session.SaveChangesAsync();
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

        public async Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
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

        public async Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return user.AccessFailedCount++;
        }

        public async Task ResetAccessFailedCountAsync(User user)
        {
            user.AccessFailedCount = 0;
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDate = lockoutEnd;
        }
    }
}