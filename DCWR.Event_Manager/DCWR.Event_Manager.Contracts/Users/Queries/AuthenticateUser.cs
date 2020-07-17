using DCWR.Event_Manager.Contracts.Users.Entities;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Users.Queries
{
    public class AuthenticateUser : IQuery<UserData>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public AuthenticateUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
