using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomeBrewComp.Domain
{
    [System.Diagnostics.DebuggerDisplay("{Title}")]
    public abstract class Enumeration<T>
        where T : Enumeration<T>
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        protected Enumeration()
        {
        }

        protected Enumeration(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        private static IEnumerable<T> _all;
        public static IEnumerable<T> All
        {
            get
            {
                return _all ?? (_all =
                    typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(field => (T)field.GetValue(null)).ToList());
            }
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}