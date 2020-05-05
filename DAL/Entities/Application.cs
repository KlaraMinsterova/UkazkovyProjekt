using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;
using Infrastructure;

namespace DAL.Entities
{
    public class Application : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Applicant Applicant { get; set; }

        [ForeignKey(nameof(Applicant))]
        public Guid ApplicantId { get; set; }

        public virtual JobOffer JobOffer { get; set; }

        [ForeignKey(nameof(JobOffer))]
        public Guid JobOfferId { get; set; }

        public string Text { get; set; }

        public virtual int StateId
        {
            get => (int)State;
            set => State = (ApplicationState)value;
        }

        [EnumDataType(typeof(ApplicationState))]
        public ApplicationState State { get; set; }

        public string TableName { get; } = nameof(DbContextPV179.Applications);
    }
}
