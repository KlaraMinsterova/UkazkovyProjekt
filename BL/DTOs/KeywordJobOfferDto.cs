using System;
using BL.DTOs.Common;
using BL.DTOs.Enums;

namespace BL.DTOs
{
    public class KeywordJobOfferDto : DtoBase
    {
        public JobOfferDto JobOffer { get; set; }

        public Guid JobOfferId { get; set; }

        public Keyword Keyword { get; set; }
    }
}
