using DCWR.Event_Manager.Contracts.Registrations.Commands;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Registrations
{
    public interface IRegistrationBuilder
    {
        Registration BuildWith(RegisterToEvent command);
    }

    public class RegistrationBuilder : IRegistrationBuilder
    {
        private readonly IGuidIdGenerator guidIdGenerator;

        public RegistrationBuilder(IGuidIdGenerator guidIdGenerator)
        {
            this.guidIdGenerator = guidIdGenerator;
        }

        public Registration BuildWith(RegisterToEvent command)
        {
            return new Registration(
                id: guidIdGenerator.Generate(),
                eventId: command.EventId,
                email: command.Email,
                name: command.Name,
                phoneNumber: command.PhoneNumber
            );
        }
    }
}
