using Xunit;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    [CollectionDefinition(WebApiTestCollection.Name)]
    public class WebApiTestCollection : ICollectionFixture<Fixture>
    {
        public const string Name = "WebApiTests";
    }
}
