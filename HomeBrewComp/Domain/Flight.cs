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

        public virtual IList<SubStyle> SubStyles { get; set; }
        public virtual IList<User> Judges { get; set; }
        public virtual TimeAssignment Time { get; set; }
    }
}
