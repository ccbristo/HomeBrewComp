using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Participant : Entity<Participant>
    {
        public Participant(string userId, string firstName, string lastName, string emailAddress, string phoneNumber, Address address)
        {
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.Entries = new List<Entry>();
        }

        public string UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public Address Address { get; private set; }

        public List<Entry> Entries { get; protected set; }

        internal void RegisterEntry(int entryNumber, string name, SubStyle style, string specialIngredients)
        {
            var entry = new Entry(name, entryNumber, style, specialIngredients);
            this.Entries.Add(entry);
        }
    }
}
