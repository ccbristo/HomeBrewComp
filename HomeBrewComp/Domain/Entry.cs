using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Entry : Entity<Entry>
    {
        public virtual string Name { get; set; }
        public virtual bool CheckedIn { get; set; }
        public virtual int Number { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual SubStyle Style { get; set; }
        public virtual string SpecialIngredients { get; set; }
        public virtual Sweetness Sweetness { get; set; }
        public virtual Carbonation Carbonation { get; set; }

        public virtual decimal Score { get; set; }
        public virtual int Place { get; set; }
    }

    public enum Sweetness
    {
        Dry,
        Sweet
    }

    public enum Carbonation
    {
        Still,
        Sparkling
    }
}
