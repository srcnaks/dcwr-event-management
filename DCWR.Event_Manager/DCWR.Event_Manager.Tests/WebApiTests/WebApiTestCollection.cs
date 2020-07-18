using DCWR.Event_Manager.Tests.Infrastructure;
using Xunit;

namespace DCWR.Event_Manager.Tests.WebApiTests
{
    [CollectionDefinition(WebApiTestCollection.Name)]
    public class WebApiTestCollection : ICollectionFixture<Fixture>
    {
        public const string Name = "WebApiTests";
    }
}
