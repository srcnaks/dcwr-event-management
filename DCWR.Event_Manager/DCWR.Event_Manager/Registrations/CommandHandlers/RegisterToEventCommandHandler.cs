using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Commands;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Registrations.Validators;

namespace DCWR.Event_Manager.Registrations.CommandHandlers
{
    public class RegisterToEventCommandHandler :
        ICommandHandler<RegisterToEvent>
    {
        private readonly IRegisterToEventValidator registerToEventValidator;
        private readonly IRegistrationBuilder registrationBuilder;
        private readonly IEntityRepository<Registration> registrationRepository;

        public RegisterToEventCommandHandler(
            IRegisterToEventValidator registerToEventValidator, 
            IRegistrationBuilder registrationBuilder,
            IEntityRepository<Registration> registrationRepository)
        {
            this.registerToEventValidator = registerToEventValidator;
            this.registrationBuilder = registrationBuilder;
            this.registrationRepository = registrationRepository;
        }

        public async Task HandleAsync(RegisterToEvent command)
        {
            await registerToEventValidator.ValidateAsync(command);
            var registration = registrationBuilder.BuildWith(command);
            command.CreatedId = registration.Id;
            await registrationRepository.AddAsync(registration);
        }
    }
}
