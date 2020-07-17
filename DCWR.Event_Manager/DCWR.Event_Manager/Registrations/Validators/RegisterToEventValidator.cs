using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Commands;
using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Infrastructure.Exceptions;

namespace DCWR.Event_Manager.Registrations.Validators
{
    public interface IRegisterToEventValidator
    {
        Task ValidateAsync(RegisterToEvent command);
    }

    public class RegisterToEventValidator : IRegisterToEventValidator
    {
        private readonly IEntityRepository<Event> eventRepository;

        public RegisterToEventValidator(IEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task ValidateAsync(RegisterToEvent command)
        {
            var eventExists = await eventRepository.ExistsAsync(command.EventId);
            if (!eventExists)
            {
                throw EntityNotFound.Create<Event>(command.EventId);
            }
        }
    }
}
