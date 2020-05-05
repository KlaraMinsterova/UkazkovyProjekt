using System.ComponentModel.DataAnnotations;

namespace PL.Models.Accounts
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}