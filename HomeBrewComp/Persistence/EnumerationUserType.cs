using System.Linq;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System;
using HomeBrewComp.Domain;

namespace HomeBrewComp.Persistence
{
    internal class EnumerationUserType<T> : IUserType
        where T : Enumeration<T>
    {
        object IUserType.Assemble(object cached, object owner)
        {
            return cached;
        }

        object IUserType.DeepCopy(object value)
        {
            return value;
        }

        object IUserType.Disassemble(object value)
        {
            return value;
        }

        bool IUserType.Equals(object x, object y)
        {
            var a = x as Enumeration<T>;
            var b = y as Enumeration<T>;

            return x == y;
        }

        int IUserType.GetHashCode(object x)
        {
            return x == null ? 0 : x.GetHashCode();
        }

        bool IUserType.IsMutable
        {
            get { return false; }
        }

        object IUserType.NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            int? id = (int?)NHibernateUtil.Int32.NullSafeGet(rs, names[0]);

            if (id == null)
                return null;

            var item = Enumeration<T>.All.SingleOrDefault(t => t.Id == id);

            if (item == null)
                throw new System.InvalidOperationException(string.Format(
                    "{0} does not contain a value with id={1}",
                        typeof(T).Name, id));

            return item;
        }

        void IUserType.NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            object dbValue = value == null ? (object)DBNull.Value : ((T)value).Id;
            NHibernate.NHibernateUtil.Int32.NullSafeSet(cmd, dbValue, index);
        }

        object IUserType.Replace(object original, object target, object owner)
        {
            return original;
        }

        System.Type IUserType.ReturnedType
        {
            get { return typeof(T); }
        }

        SqlType[] IUserType.SqlTypes
        {
            get
            {
                return new[] { SqlTypeFactory.Int32 };
            }
        }
    }
}