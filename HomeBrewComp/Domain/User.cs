using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace HomeBrewComp.Domain
{
    [System.Diagnostics.DebuggerDisplay("{UserName}")]
    public class User : AggregateRoot<User>, IUser<string>
    {
        private User()
        { }

        public User(string userName, string firstName, string lastName, string emailAddress, string phoneNumber, string ahaNumber, string bjcpId, Address address)
        {
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.AHANumber = ahaNumber;
            this.BJCPId = bjcpId;
            this.Logins = new List<Login>();
        }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public Address Address { get; private set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string AHANumber { get; set; }
        public string BJCPId { get; set; }
        public List<Login> Logins { get; private set; }

        public int AccessFailedCount { get; set; }
        public DateTimeOffset LockoutEndDate { get; set; }

        public void Enter(Competition competition)
        {
            Participant participant = new Participant(this.Id, this.FirstName, this.LastName, this.EmailAddress, this.PhoneNumber, this.Address);
            competition.AddParticipant(participant);
        }
    }
}