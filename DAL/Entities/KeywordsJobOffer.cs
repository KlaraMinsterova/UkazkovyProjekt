using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;
using Infrastructure;

namespace DAL.Entities
{
    public class KeywordsJobOffer : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual JobOffer JobOffer { get; set; }

        [ForeignKey(nameof(JobOffer))]
        public Guid JobOfferId { get; set; }

        [Required]
        public virtual int KeywordNumber
        {
            get => (int)Keyword;
            set => Keyword = (Keyword)value;
        }

        [EnumDataType(typeof(Keyword))]
        public Keyword Keyword { get; set; }

        public string TableName { get; } = nameof(DbContextPV179.KeywordsJobOffers);
    }
}
