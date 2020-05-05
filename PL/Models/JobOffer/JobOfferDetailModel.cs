using BL.DTOs;

namespace PL.Models.JobOffer
{
    public class JobOfferDetailModel
    {
        public JobOfferDto JobOffer { get; set; }

        public bool AlreadyApplied { get; set; }

        public bool AlreadyFavorite { get; set; }
    }
}