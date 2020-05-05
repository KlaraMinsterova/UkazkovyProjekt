using System.Collections.Generic;
using BL.DTOs;
using BL.DTOs.Filters;

namespace PL.Models.JobOffer
{
    public class JobOfferListModel
    {
        public bool ApplyKeywords { get; set; }

        public bool ApplyFavorite { get; set; }

        public IList<JobOfferDto> JobOffers { get; set; }

        public JobOfferFilterDto Filter { get; set; }
    }
}