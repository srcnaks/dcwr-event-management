using System;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Users
{
    public class User : IEntity
    {
        public Guid Id { get; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }

        private User()
        {
            /* Framework only */
        }

        public User(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
            PasswordHash = "";
        }

        public void UpdatePasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
