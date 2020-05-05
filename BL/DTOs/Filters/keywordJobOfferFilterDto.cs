using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filters
{
    public class KeywordJobOfferFilterDto : FilterDtoBase
    {
        public Guid JobOfferId { get; set; }

        public int[] KeywordNumbers { get; set; }
    }
}
