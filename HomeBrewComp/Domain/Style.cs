using System.Collections.Generic;

namespace HomeBrewComp.Domain
{
    public class Style : AggregateRoot<Style>
    {
        public Style()
        {
            this.SubStyles = new List<SubStyle>();
        }

        public virtual string Name { get; set; }
        public virtual string Number { get; set; }
        public virtual IList<SubStyle> SubStyles { get; set; }
    }

    public class SubStyle : Entity<SubStyle>
    {
        public virtual char Indicator { get; set; }
        public virtual string Name { get; set; }
    }
}
