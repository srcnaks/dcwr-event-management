namespace DCWR.Event_Manager.WebApi.Models
{
    public class RegisterToEventRequest
    {
        public string Email { get; }
        public string Name { get; }
        public string PhoneNumber { get; }

        public RegisterToEventRequest(
            string email,
            string name,
            string phoneNumber)
        {
            Email = email;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
