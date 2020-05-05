using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filters
{
    public class KeywordApplicantFilterDto : FilterDtoBase
    {
        public Guid ApplicantId { get; set; }

        public int[] KeywordNumbers { get; set; }
    }
}
