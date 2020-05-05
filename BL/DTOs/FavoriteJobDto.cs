using System;
using BL.DTOs.Common;

namespace BL.DTOs
{
    public class FavoriteJobDto : DtoBase
    {
        public ApplicantDto Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public JobOfferDto JobOffer { get; set; }

        public Guid JobOfferId { get; set; }
    }
}
