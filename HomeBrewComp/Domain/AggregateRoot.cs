using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public abstract class AggregateRoot<T> : Entity<T>, IEquatable<T>
        where T : AggregateRoot<T>
    {
        public virtual string Id { get; private set; }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(obj, null))
                return false;

            if (obj.GetType() != typeof(T))
                return false;

            return Equals((T)obj);
        }

        public bool Equals(T other)
        {
            if (object.ReferenceEquals(this, other))
                return true;

            if (object.ReferenceEquals(other, null))
                return false;

            return string.Equals(this.Id, other.Id, StringComparison.Ordinal);
        }
    }
}
