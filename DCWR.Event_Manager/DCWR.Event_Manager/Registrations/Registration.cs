using System;
using System.ComponentModel.DataAnnotations.Schema;
using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Registrations
{
    public class Registration : IEntity
    {
        public Guid Id { get; }
        public Guid EventId { get; }
        public string Email { get; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        [ForeignKey("EventId")]
        public Event Event { get; }

        public Registration(
            Guid id,
            Guid eventId,
            string email,
            string name, 
            string phoneNumber)
        {
            Id = id;
            EventId = eventId;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
