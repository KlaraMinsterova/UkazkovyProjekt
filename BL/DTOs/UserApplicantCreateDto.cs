using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class UserApplicantCreateDto : DtoBase
    {
        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(64, ErrorMessage = "First name can be maximum 64 characters long!")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(64, ErrorMessage = "Last name can be maximum 64 characters long!")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

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
