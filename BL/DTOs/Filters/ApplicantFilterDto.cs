using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filters
{
    public class ApplicantFilterDto : FilterDtoBase
    {
        public string LastName { get; set; }

        public Guid[] ApplicantIds { get; set; }

        public int[] KeywordNumbers { get; set; }
    }
}
