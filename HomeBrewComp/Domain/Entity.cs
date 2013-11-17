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
        public virtual int Id { get; protected set; }

        private bool wasHashedBeforePersisted = false;

        protected Entity()
        { }

        public override int GetHashCode()
        {
            // if a hash code is ever generated before an Id is assigned
            // we want to continue using that has code for the lifetime of the object.
            // otherwise, we use the Id as the hash code.
            if (!this.IsPersistent)
                wasHashedBeforePersisted = true;

            if (wasHashedBeforePersisted)
                return base.GetHashCode();

            return this.Id;
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (((object)a == null) && ((object)b == null))
                return true;

            return (object)a != null && a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
                return true;

            T other = obj as T;

            if (other == null || !this.IsPersistent || !other.IsPersistent)
                return false;

            return this.Id == other.Id;
        }

        protected virtual bool IsPersistent
        {
            get { return this.Id != 0; }
        }
    }
}
