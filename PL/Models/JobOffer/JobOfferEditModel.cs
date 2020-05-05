using BL.DTOs;
using System.Collections.Generic;

namespace PL.Models.JobOffer
{
    public class JobOfferEditModel
    {
        public JobOfferDto JobOffer { get; set; }

        public IList<bool> Keywords { get; set; }
    }
}