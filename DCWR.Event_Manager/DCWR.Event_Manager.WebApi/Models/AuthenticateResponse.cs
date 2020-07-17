using System;

namespace DCWR.Event_Manager.WebApi.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; }
        public string Username { get; }
        public string Token { get; }

        public AuthenticateResponse(Guid id, string username, string token)
        {
            Id = id;
            Username = username;
            Token = token;
        }
    }
}
