using System;
using System.Linq;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Tests.Infrastructure;
using DCWR.Event_Manager.Tests.Shared.ObjectBuilders.WebApiModels;
using FluentAssertions;
using Xunit;
using EventBuilder = DCWR.Event_Manager.Tests.Shared.ObjectBuilders.DomainEntities.EventBuilder;

namespace DCWR.Event_Manager.Tests.WebApiTests
{
    [Collection(WebApiTestCollection.Name)]
    public class EventsControllerTests
    {
        private readonly Fixture fixture;

        public EventsControllerTests(Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task WhenCreateEvent_ThenEventCreated()
        {
            // given
            var userId = GuidIdGenerator.Instance.Generate();
            var request = new CreateEventRequestBuilder().Build();

            // when
            var httpResponse = await fixture.PostAsync("api/events", userId, request);

            // then
            //httpResponse.IsSuccessStatusCode.Should().BeTrue();
            var content = await httpResponse.Content.ReadAsStringAsync();
            Guid.TryParse(content, out var createdId).Should().BeTrue();
            createdId.Should().NotBeEmpty();

            // dispose
            await fixture.DeleteEntityAsync<Event>(createdId);
        }

        [Fact]
        public async Task GivenEvents_WhenGetEvents_ThenEventListRetrieved()
        {
            // given
            var events = Enumerable.Range(0, 3)
                .Select(_ => new EventBuilder().Build())
                .ToArray();

            foreach (var @event in events)
            {
                await fixture.AddEntityAsync(@event);
            }

            // when
            var response = await fixture.GetAsync<PagedResponse<EventData>>("api/events");

            // then
            response.Results.Should().HaveSameCount(events);
            foreach (var stored in events)
            {
                var received = response.Results.Should().ContainSingle(x => x.Id == stored.Id).Subject;
                AssertEvent(received, stored);
            }

            // dispose
            foreach (var @event in events)
            {
                await fixture.DeleteEntityAsync(@event);
            }
        }

        private void AssertEvent(EventData received, Event stored)
        {
            received.Name.Should().Be(stored.Name);
            received.Description.Should().Be(stored.Description);
            received.Location.Should().Be(stored.Location);
            received.StartTime.Should().Be(stored.StartTime);
            received.EndTime.Should().Be(stored.EndTime);
        }
    }
}
