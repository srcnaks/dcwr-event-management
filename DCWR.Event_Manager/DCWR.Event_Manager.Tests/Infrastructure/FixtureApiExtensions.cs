using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DCWR.Event_Manager.WebApp.React.Controllers.Users.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    public static class FixtureApiExtensions
    {
        public static async Task<HttpResponseMessage> PostAsync<TRequest>(this Fixture fixture,
            string url,
            TRequest content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = CreateRequestContent(content)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");
            return await fixture.Client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PostAsync<TRequest>(this Fixture fixture,
            string url,
            Guid userId,
            TRequest content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = CreateRequestContent(content)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", fixture.GenerateJwtTokenForUser(userId));
            return await fixture.Client.SendAsync(request);
        }

        public static async Task<TResponse> GetAsync<TResponse>(this Fixture fixture,
            string url,
            object queryParameters = null)
        {
            var response = await fixture.GetAsync(url, queryParameters);
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content<TResponse>();
            return content;
        }

        public static async Task<TResponse> GetAsync<TResponse>(this Fixture fixture,
            string url,
            Guid userId,
            object queryParameters = null)
        {
            var response = await fixture.GetAsync(url, userId, queryParameters);
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content<TResponse>();
            return content;
        }

        public static async Task<T> Content<T>(this HttpResponseMessage httpResponse) =>
            JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());

        public static async Task<HttpResponseMessage> GetAsync(this Fixture fixture,
            string url,
            Guid userId,
            object queryParameters = null)
        {
            url = JoinQueryStringParameters(url, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", fixture.GenerateJwtTokenForUser(userId));
            var response = await fixture.Client.SendAsync(request);
            return response;
        }

        public static async Task<HttpResponseMessage> GetAsync(this Fixture fixture,
            string url,
            object queryParameters = null)
        {
            url = JoinQueryStringParameters(url, queryParameters);
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await fixture.Client.SendAsync(request);
            return response;
        }

        private static string JoinQueryStringParameters(string url, object parameters)
        {
            if (parameters == null)
                return url;

            var dictionary = new RouteValueDictionary(parameters);
            var queryString = string.Join("&", dictionary.Where(x => x.Value != null).Select(x => $"{x.Key}={x.Value}"));
            return $"{url}/?{queryString}";
        }

        private static StringContent CreateRequestContent(object content)
        {
            string json;

            if (content is string result)
                json = result;
            else
                json = JsonConvert.SerializeObject(content);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static string GenerateJwtTokenForUser(this Fixture fixture, Guid userId)
        {
            var generator = fixture.GetService<IJwtTokenGenerator>();
            return generator.GenerateJwtToken(userId);
        }
    }
}
