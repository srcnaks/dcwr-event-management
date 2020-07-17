using DCWR.Event_Manager.Contracts.Users.Entities;

namespace DCWR.Event_Manager.Users
{
    internal static class UsersContractMapperExtensions
    {
        internal static UserData ToContract(this User user)
        {
            return new UserData(
                id: user.Id,
                userName: user.UserName
            );
        }
    }
}
