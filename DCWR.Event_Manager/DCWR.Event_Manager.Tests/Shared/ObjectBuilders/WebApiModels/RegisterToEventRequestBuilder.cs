using DCWR.Event_Manager.Tests.Infrastructure;
using DCWR.Event_Manager.WebApp.React.Controllers.Events.Contracts;

namespace DCWR.Event_Manager.Tests.Shared.ObjectBuilders.WebApiModels
{
    public class RegisterToEventRequestBuilder
    {
        public string Email { get;private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }

        public RegisterToEventRequestBuilder()
        {
            Email = RandomGenerator.GetWord();
            Name = RandomGenerator.GetWord();
            PhoneNumber = RandomGenerator.GetWord();
        }

        public RegisterToEventRequest Build()
        {
            return new RegisterToEventRequest()
            {
                Email = Email,
                Name = Name,
                PhoneNumber = PhoneNumber
            };
        }
    }
}
