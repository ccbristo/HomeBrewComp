using System;
using System.Linq;
using HomeBrewComp.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client.Document;

namespace HomebrewComp.Tests
{
    [TestClass]
    public class RegistrationTests : UnitTestBase
    {
        public int TestHostClubId = 1;
        private static readonly Address TestLocation = new Address("line 1", "line 2", "city", "state", "27407");

        [TestMethod]
        public void Participants_Can_Register_Entries()
        {
            var competition = new Competition("test comp", TestHostClubId, DateTime.Now, TestLocation, ahaSanctioned: true);
            var user = new User("login", "pass", "First Name", "Last Name", "email@internet.com", "555-123-4567",
                "aha1234", "F0753", new Address("line 1", "line 2", "city", "state", "12345"));
            var user2 = new User("login2", "pass", "First Name 2", "Last Name 2", "email@internet.com", "555-123-4567",
                "aha1234", "F0753", new Address("line 1", "line 2", "city", "state", "12345"));

            Session.Store(user);
            Session.Store(user2);

            user.Enter(competition);

            Session.Store(competition);

            competition.RegisterEntry(user.Id, "entry name", new SubStyle(), "special ingredients");
        }
    }
}
