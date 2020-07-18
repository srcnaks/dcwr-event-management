using Microsoft.AspNetCore.Identity;

namespace DCWR.Event_Manager.Users
{
    public interface IPasswordManager
    {
        string GenerateHash(User user, string password);
        bool ValidatePassword(User user, string password);
    }

    public class PasswordManager : IPasswordManager
    {
        private readonly IPasswordHasher<User> passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public string GenerateHash(User user, string password)
        {
            return passwordHasher.HashPassword(user, password);
        }

        public bool ValidatePassword(User user, string password)
        {
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
