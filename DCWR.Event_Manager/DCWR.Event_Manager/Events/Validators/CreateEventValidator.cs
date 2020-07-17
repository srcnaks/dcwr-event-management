using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Commands;

namespace DCWR.Event_Manager.Events.Validators
{
    public interface ICreateEventValidator
    {
        Task ValidateAsync(CreateEvent command);
    }

    public class CreateEventValidator: ICreateEventValidator
    {
        public async Task ValidateAsync(CreateEvent command)
        {
            // todo: add necessary validation here...
        }
    }
}
