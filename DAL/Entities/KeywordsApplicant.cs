using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;
using Infrastructure;

namespace DAL.Entities
{
    public class KeywordsApplicant : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Applicant Applicant { get; set; }

        [ForeignKey(nameof(Applicant))]
        public Guid ApplicantId { get; set; }

        [Required]
        public virtual int KeywordNumber
        {
            get => (int)Keyword;
            set => Keyword = (Keyword)value;
        }

        [EnumDataType(typeof(Keyword))]
        public Keyword Keyword { get; set; }

        public string TableName { get; } = nameof(DbContextPV179.KeywordsApplicants);
    }
}
