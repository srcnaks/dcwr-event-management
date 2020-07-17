using System;
using System.Collections.Generic;
using System.Text;

namespace DCWR.Event_Manager.Contracts.Registrations.Entities
{
    public class AttendeeData
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string PhoneNumber { get; }

        public AttendeeData(
            Guid id,
            string email,
            string name,
            string phoneNumber)
        {
            Id = id;
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
