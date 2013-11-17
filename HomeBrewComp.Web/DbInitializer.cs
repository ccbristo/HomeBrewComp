using System.Web;
using HomeBrewComp.Domain;
using HomeBrewComp.Persistence;
using NHibernate;

namespace HomeBrewComp.Web
{
    public static class DbInitializer
    {
        public static void Initialize()
        {
            NHibernateSetup.Drop(true);
            NHibernateSetup.Create(true);
            using (var session = NHibernateSetup.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    InitializeData(session);
                    transaction.Commit();
                }
            }
        }

        private static  void InitializeData(ISession session)
        {
            var club = new Club { Name = "BBG" };

            var address = new Address("5921 Old Fox Trail", string.Empty, "Greensboro", "NC", "27407");
            session.Save(address);

            var user = new User
            {
                FirstName = "Chris",
                LastName = "Bristol",
                UserName = "cbristol",
                Password = "myPass",
                EmailAddress = "ccbristo@hotmail.com",
                Address = address,
                Club = club,
            };

            //for (int i = 0; i < 10; i++)
            //{
            //    var competition = new Competition
            //    {
            //        Name = "Competition " + i,
                    
            //    }
            //}

            session.Save(user);
        }
    }
}