using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public abstract class Entity<T>
        where T : Entity<T>
    {
        protected Entity()
        { }
    }
}
