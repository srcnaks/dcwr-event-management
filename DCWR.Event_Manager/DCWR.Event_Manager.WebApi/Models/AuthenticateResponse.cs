using System;

namespace DCWR.Event_Manager.WebApi.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; }
        public string Token { get; }

        public AuthenticateResponse(Guid id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}
