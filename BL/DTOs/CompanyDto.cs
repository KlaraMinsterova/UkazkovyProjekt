using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class CompanyDto : DtoBase
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "This is not a valid email address!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required!")]
        [Phone(ErrorMessage = "This is not a valid phone number!")]
        [Display(Name = "Tel.")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(128, ErrorMessage = "Name can be maximum 128 characters long!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [MaxLength(1024, ErrorMessage = "Address can be maximum 1024 characters long!")]
        public string Address { get; set; }
    }
}
