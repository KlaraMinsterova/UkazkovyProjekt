using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;

namespace DAL.Entities
{
    public class JobOffer : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Company Company { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid? CompanyId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Position { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(64)]
        public string Location { get; set; }

        public virtual List<KeywordsJobOffer> Keywords { get; set; }

        public List<FavoriteJob> FavoriteJobs { get; set; }

        public List<Application> Applications { get; set; }

        public string TableName { get; } = nameof(DbContextPV179.JobOffers);
    }
}
