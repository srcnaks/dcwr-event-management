using DCWR.Event_Manager.Contracts.Registrations.Entities;

namespace DCWR.Event_Manager.Registrations
{
    internal static class RegistrationsContractMapperExtensions
    {
        internal static AttendeeData toAttendeeData(this Registration registration)
        {
            return new AttendeeData(
                id: registration.Id,
                email: registration.Email,
                name: registration.Name,
                phoneNumber: registration.PhoneNumber
            );
        }
    }
}
