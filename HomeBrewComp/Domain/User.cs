namespace HomeBrewComp.Domain
{
    [System.Diagnostics.DebuggerDisplay("{EmailAddress}")]
    public class User : AggregateRoot<User>
    {
        private User()
        { }

        public User(string login, string password, string firstName, string lastName, string emailAddress, string phoneNumber, string ahaNumber, string bjcpId, Address address)
        {
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.AHANumber = ahaNumber;
            this.BJCPId = bjcpId;
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public Address Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public string AHANumber { get; private set; }
        public string BJCPId { get; private set; }

        public void Enter(Competition competition)
        {
            Participant participant = new Participant(this.Id, this.FirstName, this.LastName, this.EmailAddress, this.PhoneNumber, this.Address);
            competition.AddParticipant(participant);
        }
    }
}