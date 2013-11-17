namespace HomeBrewComp.Domain
{
    [System.Diagnostics.DebuggerDisplay("{EmailAddress}")]
    public class User : Entity<User>
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual Address Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual Club Club { get; set; }
        public virtual string AHANumber { get; set; }
        public virtual string BJCPId { get; set; }
    }
}