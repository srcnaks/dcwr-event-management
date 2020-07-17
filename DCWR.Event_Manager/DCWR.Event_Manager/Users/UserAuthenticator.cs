using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Users.Queries;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager.Users
{
    public interface IUserAuthenticator
    {
        Task<User> Authenticate(AuthenticateUser query);
    }
    internal class UserAuthenticator : EntityRepository<User>, IUserAuthenticator
    {
        private readonly IPasswordManager passwordManager;

        public UserAuthenticator(
            EventManagerDbContext dbContext,
            IPasswordManager passwordManager)
            : base(dbContext)
        {
            this.passwordManager = passwordManager;
        }

        public async Task<User> Authenticate(AuthenticateUser query)
        {
            var user = await GetUser(query.UserName);
            ValidatePassword(user, query.Password);
            return user;
        }

        private async Task<User> GetUser(string userName)
        {
            var user = await GetQuery().SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                throw new AuthenticationFailed();
            return user;
        }

        public void ValidatePassword(User user, string password)
        {
            var isValid = passwordManager.ValidatePassword(user, password);
            if (!isValid)
                throw new AuthenticationFailed();
        }
    }
}
