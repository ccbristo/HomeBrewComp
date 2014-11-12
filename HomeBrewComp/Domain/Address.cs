using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBrewComp.Domain
{
    public class Address : ValueObject<Address>
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

        public string Line1 { get; private set; }
        public string Line2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
    }
}
