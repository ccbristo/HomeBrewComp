using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBrewComp.Domain;
using Raven.Client.Indexes;

namespace HomeBrewComp.Indexes
{
    public class UserLoginIndex : AbstractIndexCreationTask<User, UserLoginIndex.Result>
    {
        public class Result
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
        }

        public UserLoginIndex()
        {
            Map = docs => from user in docs
                          from login in user.Logins
                          select new Result
                          {
                              UserId = user.Id,
                              UserName = user.UserName,
                              LoginProvider = login.Provider,
                              ProviderKey = login.ProviderKey
                          };
        }
    }
}
