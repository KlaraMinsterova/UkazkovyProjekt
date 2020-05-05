using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Applicant : User
    {
        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        public string Education { get; set; }

        public string Skills { get; set; }

        public string Experience { get; set; }

        public string Email { get; set; }

        public string Tel { get; set; }

        public List<FavoriteJob> FavoriteJobs { get; set; }

        public virtual List<KeywordsApplicant> Keywords { get; set; }

        public List<Application> Applications { get; set; }
    }
}
