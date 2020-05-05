using System;
using System.ComponentModel.DataAnnotations;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class JobOfferDto : DtoBase
    {
        public CompanyDto Company { get; set; }

        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Position is required!")]
        [MaxLength(128, ErrorMessage = "Position can be maximum 128 characters long!")]
        public string Position { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required!")]
        [MaxLength(64, ErrorMessage = "Location can be maximum 64 characters long!")]
        public string Location { get; set; }

        public int[] KeywordNumbers { get; set; }

        public string KeywordString { get; set; }
    }
}
