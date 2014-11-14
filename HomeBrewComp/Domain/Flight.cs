using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeBrewComp.Domain
{
    public class Flight : Entity<Flight>
    {
        public Flight()
        {
            this.SubStyles = new List<SubStyle>();
        }

        public List<SubStyle> SubStyles { get; set; }
        public List<User> Judges { get; set; }
        public TimeAssignment Time { get; set; }
    }
}
