using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBrewComp.Domain
{
    public class Address : Entity<Address>
    {
        public Address(string line1, string line2, string city, string state, string zip)
        {
            this.Line1 = line1;
            this.Line2 = line2;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        protected Address()
        { }

        public virtual string Line1 { get; protected set; }
        public virtual string Line2 { get; protected set; }
        public virtual string City { get; protected set; }
        public virtual string State { get; protected set; }
        public virtual string Zip { get; protected set; }
    }
}
