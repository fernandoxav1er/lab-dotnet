using System.ComponentModel.DataAnnotations;

namespace FinShark.API.Dtos.Account
{
    public class LoginDto
    {
        [Required] 
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}
