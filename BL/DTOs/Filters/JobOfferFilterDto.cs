using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filters
{
    public class JobOfferFilterDto : FilterDtoBase
    {
        public Guid CompanyId { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public Guid[] JobOfferIds { get; set; }

        public int[] KeywordNumbers { get; set; }
    }
}
