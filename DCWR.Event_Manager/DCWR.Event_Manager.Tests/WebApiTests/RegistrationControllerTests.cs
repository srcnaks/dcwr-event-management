using System.Linq;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Registrations;
using DCWR.Event_Manager.Tests.Infrastructure;
using DCWR.Event_Manager.Tests.Shared.ObjectBuilders.DomainEntities;
using DCWR.Event_Manager.Tests.Shared.ObjectBuilders.WebApiModels;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DCWR.Event_Manager.Tests.WebApiTests
{
    [Collection(WebApiTestCollection.Name)]
    public class RegistrationControllerTests
    {
        private readonly Fixture fixture;

        public RegistrationControllerTests(Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GivenEvent_WhenRegisterToEvent_ThenRegistered()
        {
            // given 
            var @event = new EventBuilder().Build();
            await fixture.AddEntityAsync(@event);

            var request = new RegisterToEventRequestBuilder().Build();

            // when
            var httpResponse = await fixture.PostAsync(
                url: $"api/events/{@event.Id}/registrations",
                content: request
            );

            // then
            httpResponse.IsSuccessStatusCode.Should().BeTrue();

            var stored = await fixture.GetContext()
                .Set<Registration>()
                .SingleOrDefaultAsync(x => x.EventId == @event.Id);
            stored.Should().NotBeNull();
            stored.Name.Should().Be(request.Name);
            stored.Email.Should().Be(request.Email);
            stored.PhoneNumber.Should().Be(request.PhoneNumber);

            // dispose
            await fixture.DeleteEntityAsync(stored);
            await fixture.DeleteEntityAsync(@event);
        }

        [Fact]
        public async Task GivenEventWithAttendees_WhenGetRegistrationsOfEvent_ThenListOfAttendeesReceived()
        {
            // given 
            var @event = new EventBuilder().Build();
            await fixture.AddEntityAsync(@event);

            var attendees = Enumerable.Range(0, 6)
                .Select(_ => new Shared.ObjectBuilders.DomainEntities.RegistrationBuilder()
                    .WithEventId(@event.Id)
                    .Build()
                )
                .ToArray();
            foreach (var registration in attendees)
            {
                await fixture.AddEntityAsync(registration);
            }

            var userId = GuidIdGenerator.Instance.Generate();

            // when
            var response = await fixture.GetAsync<PagedResponse<AttendeeData>>(
                url: $"api/events/{@event.Id}/registrations",
                userId
            );

            // then
            response.Results.Should().HaveSameCount(attendees);
            foreach (var stored in attendees)
            {
                var received = response.Results.Should().ContainSingle(x => x.Id == stored.Id).Subject;
                AssertAttendee(received, stored);
            }

            // dispose
            foreach (var registration in attendees)
            {
                await fixture.DeleteEntityAsync(registration);
            }
        }

        private void AssertAttendee(AttendeeData received, Registration stored)
        {
            received.Name.Should().Be(stored.Name);
            received.Email.Should().Be(stored.Email);
            received.PhoneNumber.Should().Be(stored.PhoneNumber);
        }
    }
}
