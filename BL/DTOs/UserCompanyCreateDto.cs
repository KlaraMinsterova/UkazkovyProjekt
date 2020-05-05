using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class UserCompanyCreateDto : DtoBase
    {
        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(128, ErrorMessage = "Name can be maximum 128 characters long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [MaxLength(1024, ErrorMessage = "Address can be maximum 1024 characters long!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "This is not a valid email address!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Phone(ErrorMessage = "This is not a valid phone number!")]
        [Display(Name = "Tel.")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        [MaxLength(64, ErrorMessage = "Username can be maximum 64 characters long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length must be between 6 and 30")]
        public string Password { get; set; }

        public string Roles { get; set; }
    }
}
