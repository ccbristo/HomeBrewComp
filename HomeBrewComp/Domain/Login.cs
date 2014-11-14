using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBrewComp.Domain
{
    public class Login
    {
        private Login()
        { }

        public Login(string provider, string providerKey)
        {
            this.Provider = provider;
            this.ProviderKey = providerKey;
        }

        public string Provider { get; private set; }
        public string ProviderKey { get; private set; }
    }
}
