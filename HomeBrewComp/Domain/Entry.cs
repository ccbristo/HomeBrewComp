namespace HomeBrewComp.Domain
{
    public class Entry : Entity<Entry>
    {
        private Entry()
        { }

        public Entry(string name, int number, SubStyle subStyle, string specialIngredients)
        {
            this.Name = name;
            this.Number = number;
            this.Style = subStyle;
            this.SpecialIngredients = specialIngredients;
        }

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
