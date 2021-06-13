using System.ComponentModel.DataAnnotations;

namespace WebApi.Application.Dto
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}