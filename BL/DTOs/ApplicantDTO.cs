using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class ApplicantDto : DtoBase
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

        [MaxLength(1024, ErrorMessage = "Education can be maximum 1024 characters long!")]
        [DataType(DataType.MultilineText)]
        public string Education { get; set; }

        [MaxLength(1024, ErrorMessage = "Skills can be maximum 1024 characters long!")]
        [DataType(DataType.MultilineText)]
        public string Skills { get; set; }

        [MaxLength(1024, ErrorMessage = "Experience can be maximum 1024 characters long!")]
        [DataType(DataType.MultilineText)]
        public string Experience { get; set; }

        public int[] KeywordNumbers { get; set; }

        public string KeywordString { get; set; }
    }
}
