using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBrewComp.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Document;

namespace HomebrewComp.Tests
{
    [TestClass]
    public abstract class UnitTestBase
    {
        public static readonly DocumentStore DocumentStore = new DocumentStore();
        protected IDocumentSession Session;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            DocumentStore.ConnectionStringName = "HomeBrewComp";
            DocumentStore.Initialize();

            using(var session = DocumentStore.OpenSession())
            {
                InitializeData(session);
                session.SaveChanges();
            }
        }

        private static void InitializeData(IDocumentSession session)
        {
            var adminUser = session.Query<User>().SingleOrDefault(u => u.UserName == "admin");
            if (adminUser == null)
            {
                adminUser = new User("admin", "Admin", "User", "ccbristo@hotmail.com", "555-123-4567", "aha1234", "F01324", new Address("123 Here St", "Unit 2", "Greensboro", "NC", "27407"));

                session.Store(adminUser);  
            }

            var testClub = session.Query<Club>().SingleOrDefault(c => c.Name == "Test Club");
            if (testClub == null)
            {
                testClub = new Club("Test Club");
                session.Store(testClub);
            }
        }

        [TestInitializeAttribute]
        public void InitializeTest()
        {
            Session = DocumentStore.OpenSession();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Session.SaveChanges();
            Session.Dispose();
            Session = null;
        }
    }
}
