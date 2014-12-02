using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;

namespace HomeBrewComp.Web.Authentication
{
    public static class MicrosoftAuthentication
    {
        const string XmlSchemaString = "http://www.w3.org/2001/XMLSchema#string";

        public static void Configure(IAppBuilder app)
        {
            var options = new MicrosoftAccountAuthenticationOptions
            {
                CallbackPath = new PathString(ConfigurationManager.AppSettings["MicrosoftAuthenticationCallbackPath"]),
                ClientId = ConfigurationManager.AppSettings["MicrosoftClientId"],
                ClientSecret = ConfigurationManager.AppSettings["MicrosoftClientSecret"],
                Provider = new MicrosoftAccountAuthenticationProvider()
                {
                    OnAuthenticated = OnAuthenticatedHandler
                }
            };

            // http://msdn.microsoft.com/en-us/library/hh243646.aspx
            options.Scope.Add("wl.basic");
            options.Scope.Add("wl.emails");
            options.Scope.Add("wl.birthday");
            options.Scope.Add("wl.phone_numbers");
            options.Scope.Add("wl.postal_addresses");

            app.UseMicrosoftAccountAuthentication(options);
        }

        private static async System.Threading.Tasks.Task OnAuthenticatedHandler(MicrosoftAccountAuthenticatedContext context)
        {
            dynamic user = context.User;

            if (user.birth_day.Value != null && user.birth_month.Value != null && user.birth_year.Value != null)
            {
                int day = (int)user.birth_day.Value,
                        month = (int)user.birth_month.Value,
                        year = (int)user.birth_year.Value;

                var birthDate = new DateTime(year, month, day);
                context.Identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, birthDate.ToString("g"), XmlSchemaString, "Microsoft"));
            }

            var claims = new[]
                {
                     new { ClaimType = ClaimTypes.GivenName, Value = context.FirstName },
                     new { ClaimType = ClaimTypes.Surname, Value = context.LastName },
                     new { ClaimType = ClaimTypes.StreetAddress, Value = (string)user.addresses.personal.street.Value },
                     new { ClaimType = "Street2", Value =  (string)user.addresses.personal.street_2.Value },
                     new { ClaimType = "City", Value = (string)user.addresses.personal.city.Value },
                     new { ClaimType = ClaimTypes.StateOrProvince, Value = (string)user.addresses.personal.state.Value},
                     new { ClaimType = ClaimTypes.PostalCode, Value = (string)user.addresses.personal.postal_code.Value },
                     new { ClaimType = ClaimTypes.Country, Value = (string)user.addresses.personal.region.Value }
                }
                .Where(claim => claim.Value != null)
                .Select(claim => new Claim(claim.ClaimType, claim.Value, XmlSchemaString, "Microsoft"));

            context.Identity.AddClaims(claims);
        }
    }
}