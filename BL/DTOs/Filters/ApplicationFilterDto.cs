using BL.DTOs.Common;
using System;

namespace BL.DTOs.Filters
{
    public class ApplicationFilterDto : FilterDtoBase
    {
        public Guid ApplicantId { get; set; }

        public Guid JobOfferId { get; set; }

        public int? StateNumber { get; set; }
    }
}