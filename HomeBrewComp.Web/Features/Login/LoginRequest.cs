using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core.Continuations;

namespace HomeBrewComp.Web.Features.Login
{
    public class LoginRequest : IRedirectable
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public FubuContinuation RedirectTo { get; set; }
    }
}