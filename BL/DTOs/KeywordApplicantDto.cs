using System;
using BL.DTOs.Common;
using BL.DTOs.Enums;

namespace BL.DTOs
{
    public class KeywordApplicantDto : DtoBase
    {
        public ApplicantDto Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public Keyword Keyword { get; set; }
    }
}
