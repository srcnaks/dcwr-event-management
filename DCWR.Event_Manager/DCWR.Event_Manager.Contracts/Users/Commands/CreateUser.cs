using System;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Users.Commands
{
    public class CreateUser : ICommand
    {
        public Guid CreatedId { get; set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public CreateUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
