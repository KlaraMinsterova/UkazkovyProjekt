using System;
using BL.DTOs.Common;
using BL.DTOs.Enums;

namespace BL.DTOs
{
    public class ApplicationDto : DtoBase
    {
        public ApplicantDto Applicant { get; set; }

        public Guid ApplicantId { get; set; }

        public JobOfferDto JobOffer { get; set; }

        public Guid JobOfferId { get; set; }

        public string Text { get; set; }

        public ApplicationState State { get; set; }
    }
}
