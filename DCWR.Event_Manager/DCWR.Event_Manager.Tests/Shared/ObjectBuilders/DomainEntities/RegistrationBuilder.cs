using System;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Registrations;
using DCWR.Event_Manager.Tests.Infrastructure;

namespace DCWR.Event_Manager.Tests.Shared.ObjectBuilders.DomainEntities
{
    public class RegistrationBuilder
    {
        public Guid Id { get; private set; }
        public Guid EventId { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public RegistrationBuilder()
        {
            Id = GuidIdGenerator.Instance.Generate();
            EventId = GuidIdGenerator.Instance.Generate();
            Email = RandomGenerator.GetWord();
            Name = RandomGenerator.GetWord();
            PhoneNumber = RandomGenerator.GetWord();
        }

        public Registration Build()
        {
            return new Registration(
                id: Id,
                eventId: EventId,
                email: Email,
                name: Name,
                phoneNumber: PhoneNumber
            );
        }

        public RegistrationBuilder WithEventId(Guid eventId)
        {
            EventId = eventId;
            return this;
        }
    }
}
