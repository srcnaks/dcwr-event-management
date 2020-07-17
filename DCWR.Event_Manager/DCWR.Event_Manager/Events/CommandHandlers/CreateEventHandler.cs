using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.Events.Validators;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Events.CommandHandlers
{
    public class CreateEventHandler : 
        ICommandHandler<CreateEvent>
    {
        private readonly IEventBuilder eventBuilder;
        private readonly IEntityRepository<Event> eventRepository;
        private readonly ICreateEventValidator createEventValidator;

        public CreateEventHandler(
            IEventBuilder eventBuilder,
            IEntityRepository<Event> eventRepository, 
            ICreateEventValidator createEventValidator)
        {
            this.eventBuilder = eventBuilder;
            this.eventRepository = eventRepository;
            this.createEventValidator = createEventValidator;
        }

        public async Task HandleAsync(CreateEvent command)
        {
            await createEventValidator.ValidateAsync(command);
            var @event = eventBuilder.BuildWith(command);
            command.CreatedId = @event.Id;
            await eventRepository.AddAsync(@event);
        }
    }
}
