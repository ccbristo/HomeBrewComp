using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBrewComp.Domain
{
    public class Competition : AggregateRoot<Competition>
    {
        protected Competition()
            : this(null, 0, DateTime.MinValue, null, false)
        { }

        public Competition(string name, int hostClubId, DateTime date, Address location, bool ahaSanctioned)
        {
            this.Name = name;
            this.HostClubId = hostClubId;
            this.EventDate = date;
            this.Location = location;
            this.ShippingAddresses = new List<Address>();
            this.StewardIds = new List<int>();
            this.JudgeIds = new List<int>();
            this.Participants = new List<Participant>();
        }

        public string Name { get; set; }
        public int HostClubId { get; private set; }
        public User Organizer { get; set; }
        public Uri Website { get; set; }
        public bool AHASanctioned { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }
        public DateTime EntryDueDate { get; set; }
        public DateTime EventDate { get; set; }

        public Address Location { get; set; }
        public List<Address> ShippingAddresses { get; protected set; }

        public List<int> JudgeIds { get; protected set; }
        public List<int> StewardIds { get; protected set; }

        private List<Participant> Participants { get; set; }

        public void RegisterEntry(string userId, string name, SubStyle style, string specialIngredients)
        {
            int entryNumber = this.GenerateEntryNumber(style);
            var participant = this.Participants.SingleOrDefault(p => p.UserId == userId);

            if (participant == null)
                throw new InvalidOperationException("A user must be entered in a competition to register entries.");

            participant.RegisterEntry(entryNumber, name, style, specialIngredients);
        }

        private int GenerateEntryNumber(SubStyle style)
        {
            return 10;
        }

        internal void AddParticipant(Participant participant)
        {
            this.Participants.Add(participant);
        }
    }
}
