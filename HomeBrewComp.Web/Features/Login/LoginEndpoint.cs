using System;
using System.Linq;
using FubuMVC.Core.Continuations;
using HomeBrewComp.Domain;
using HomeBrewComp.Web.Features.Home;
using NHibernate;
using NHibernate.Linq;

namespace HomeBrewComp.Web.Features.Login
{
    public class LoginEndpoint
    {
        private readonly ISession mSession;

        public LoginEndpoint(ISession session)
        {
            mSession = session;
        }

        public LoginRequest get_Login()
        {
            return new LoginRequest();
        }

        public LoginRequest post_Authenticate(LoginRequest request)
        {
            var users = (from u in mSession.Query<User>()
                         where u.UserName == request.Username
                         select u).ToList();

            if (users.Count > 1)
                throw new InvalidOperationException("Multiple users have the same username.");

            if (users.Count != 1 || !users[0].Password.Equals(request.Password))
                return new LoginRequest();

            return new LoginRequest
            {
                RedirectTo = FubuContinuation.RedirectTo<HomeRequest>()
            };
        }
    }
}