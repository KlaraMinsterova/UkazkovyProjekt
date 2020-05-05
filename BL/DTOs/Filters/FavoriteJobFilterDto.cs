using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filters
{
    public class FavoriteJobFilterDto : FilterDtoBase
    {
        public Guid ApplicantId { get; set; }

        public Guid JobOfferId { get; set; }
    }
}
