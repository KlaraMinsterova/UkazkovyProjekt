using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure;

namespace DAL.Entities
{
    public class FavoriteJob : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Applicant Applicant { get; set; }

        [ForeignKey(nameof(Applicant))]
        public Guid ApplicantId { get; set; }

        public virtual JobOffer JobOffer { get; set; }

        [ForeignKey(nameof(JobOffer))]
        public Guid JobOfferId { get; set; }

        public string TableName { get; } = nameof(DbContextPV179.FavoriteJobs);
    }
}
