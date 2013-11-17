using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Competition : Entity<Competition>
    {
        public Competition()
        {
            this.ShippingAddresses = new List<Address>();
            this.Stewards = new List<User>();
            this.Judges = new List<User>();
        }

        public virtual string Name { get; set; }
        public virtual Club HostClub { get; set; }
        public virtual User Organizer { get; set; }
        public virtual Uri Website { get; set; }
        public virtual bool AHASanctioned { get; set; }
        public virtual DateTime RegistrationStartDate { get; set; }
        public virtual DateTime RegistrationEndDate { get; set; }
        public virtual DateTime EntryDueDate { get; set; }
        public virtual DateTime EventDate { get; set; }

        public virtual Address Location { get; set; }
        public virtual IList<Address> ShippingAddresses { get; protected set; }

        public virtual IList<User> Judges { get; protected set; }
        public virtual IList<User> Stewards { get; protected set; }

        public virtual IList<Entry> Entries { get; set; }
    }
}
