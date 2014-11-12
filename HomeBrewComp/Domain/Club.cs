using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Club : AggregateRoot<Club>
    {
        protected Club()
            : this(null)
        { }

        public Club(string name)
        {
            this.Name = name;
        }

        public virtual string Name { get; set; }
    }
}
