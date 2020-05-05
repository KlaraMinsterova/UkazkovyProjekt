using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Company : User
    { 
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Address { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public List<JobOffer> JobOffers { get; set; }      
    }
}
