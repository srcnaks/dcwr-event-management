using System;
using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Tests.Shared.ObjectBuilders.Contracts;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DCWR.Event_Manager.Tests.UnitTests.Events
{
    public class EventBuilderTests
    {
        private readonly IGuidIdGenerator guidIdGenerator;
        private readonly EventBuilder eventBuilder;

        public EventBuilderTests()
        {
            guidIdGenerator = Substitute.For<IGuidIdGenerator>();
            eventBuilder = new EventBuilder(guidIdGenerator);
        }

        [Fact]
        public void GivenCreateEvent_WhenBuildWith_ThenIdGeneratorCalled()
        {
            // given
            var command = new CreateEventBuilder().Build();

            // when
            var @event = eventBuilder.BuildWith(command);

            // then
            guidIdGenerator.Received(1).Generate();
        }

        [Fact]
        public void GivenCreateEvent_WhenBuildWith_ThenEventBuilt()
        {
            // given
            var eventId = Guid.NewGuid();
            guidIdGenerator.Generate().Returns(eventId);
            var command = new CreateEventBuilder().Build();

            // when
            var @event = eventBuilder.BuildWith(command);

            // then
            @event.Id.Should().Be(eventId);
            @event.Name.Should().Be(command.Name);
            @event.Description.Should().Be(command.Description);
            @event.Location.Should().Be(command.Location);
            @event.StartTime.Should().Be(command.StartTime);
            @event.EndTime.Should().Be(command.EndTime);
        }
    }
}
