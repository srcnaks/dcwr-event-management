using System;
using DCWR.Event_Manager.Contracts.Registrations.Commands;

namespace DCWR.Event_Manager.Registrations
{
    public interface IRegistrationBuilder
    {
        Registration BuildWith(RegisterToEvent command);
    }

    public class RegistrationBuilder : IRegistrationBuilder
    {
        public Registration BuildWith(RegisterToEvent command)
        {
            return new Registration(
                Guid.NewGuid(),
                command.EventId,
                command.Email,
                command.Name,
                command.PhoneNumber
            );
        }
    }
}
