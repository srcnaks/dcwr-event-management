using System;
using System.Linq;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Registrations
{
    public interface IAttendeeDataRetriever
    {
        Task<PagedResponse<AttendeeData>> GetAttendeeDataOfEvent(Guid eventId, int pageSize, int pageNumber);
    }

    internal class AttendeeDataRetriever : IAttendeeDataRetriever
    {
        private readonly IEntityRepository<Registration> registrationRepository;

        public AttendeeDataRetriever(IEntityRepository<Registration> registrationRepository)
        {
            this.registrationRepository = registrationRepository;
        }

        public async Task<PagedResponse<AttendeeData>> GetAttendeeDataOfEvent(Guid eventId, int pageSize, int pageNumber)
        {
            Predicate<Registration> predicate = null;
            var attendees = await registrationRepository.GetAsync(pageSize, pageNumber, predicate);
            var pagingInfo = await GetPagingInfo(pageSize, pageNumber, predicate);
            return new PagedResponse<AttendeeData>(
                attendees.Select(x => x.toAttendeeData()).ToArray(),
                pagingInfo
            );
        }

        private async Task<PagingInfo> GetPagingInfo(int pageSize, int pageNumber, Predicate<Registration> predicate)
        {
            var totalCount = await registrationRepository.GetCountAsync(predicate);
            return new PagingInfo(pageNumber, pageSize, totalCount);
        }
    }
}
