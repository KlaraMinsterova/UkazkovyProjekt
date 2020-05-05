using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class UserDto : DtoBase
    {
        [Required]
        public string Username { get; set; }

        [Required, StringLength(100)]
        public string PasswordSalt { get; set; }

        [Required, StringLength(100)]
        public string PasswordHash { get; set; }
    
        public string Roles { get; set; }
    }
}
