using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp
{
    public static class EnumerableExtensions
    {
        public static bool In<T>(this T item, params T[] options)
        {
            return In(item, (IEnumerable<T>)options);
        }

        public static bool In<T>(this T item, IEnumerable<T> options)
        {
            return options.Contains(item);
        }
    }
}
