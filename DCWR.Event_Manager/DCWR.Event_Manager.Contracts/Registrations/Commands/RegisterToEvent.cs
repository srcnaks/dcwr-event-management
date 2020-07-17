using System;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Registrations.Commands
{
    public class RegisterToEvent : ICommand
    {
        public Guid CreatedId { get; set; }
        public Guid EventId { get; }
        public string Email { get; }
        public string Name { get; }
        public string PhoneNumber { get; }

        public RegisterToEvent(
            Guid eventId, 
            string email, 
            string name, 
            string phoneNumber)
        {
            EventId = eventId;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
