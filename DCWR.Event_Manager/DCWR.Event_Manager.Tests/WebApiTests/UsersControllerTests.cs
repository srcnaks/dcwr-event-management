using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DCWR.Event_Manager.Tests.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace DCWR.Event_Manager.Tests.WebApiTests
{
    [Collection(WebApiTestCollection.Name)]
    public class UsersControllerTests
    {
        private readonly Fixture fixture;

        public UsersControllerTests(Fixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GivenCorrectCredentials_WhenAuthenticate_ThenTokenReceived()
        {
            // given
            var userName = "admin";
            var password = "admin";

            // when 
            var response = await PostAuthenticateRequest(userName, password);

            // then
            response.IsSuccessStatusCode.Should().BeTrue();
            var responseObj =
                JsonConvert.DeserializeObject<AuthenticateResponse>(await response.Content.ReadAsStringAsync());
            responseObj.Token.Should().NotBeEmpty();
            responseObj.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GivenWrongUserName_WhenAuthenticate_ThenBadRequest()
        {
            // given
            var userName = "wrong";
            var password = "admin";

            // when 
            var response = await PostAuthenticateRequest(userName, password);

            // then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenWrongPassword_WhenAuthenticate_ThenBadRequest()
        {
            // given
            var userName = "admin";
            var password = "wrong";

            // when 
            var response = await PostAuthenticateRequest(userName, password);

            // then
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<HttpResponseMessage> PostAuthenticateRequest(string userName, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/users/authenticate")
            {
                Content = CreateRequestContent(
                    new AuthenticateRequest()
                    {
                        Username = userName,
                        Password = password
                    }
                )
            };
            return await fixture.Client.SendAsync(request);
        }

        private static StringContent CreateRequestContent(AuthenticateRequest content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
