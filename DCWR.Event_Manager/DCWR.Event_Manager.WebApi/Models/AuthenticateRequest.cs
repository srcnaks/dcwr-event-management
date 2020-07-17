using System.ComponentModel.DataAnnotations;

namespace DCWR.Event_Manager.WebApi.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}