using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Club : Entity<Club>
    {
        public virtual string Name { get; set; }
    }
}
