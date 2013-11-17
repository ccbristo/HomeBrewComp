using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBrewComp.Web.Features.Home
{
    public class HomeEndpoint
    {
        public HomeResponse Get(HomeRequest model)
        {
            return new HomeResponse();
        }
    }
}