using System;

namespace DCWR.Event_Manager.Contracts.Users.Entities
{
    public class UserData
    {
        public Guid Id { get; }
        public string UserName { get; }

        public UserData(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}
