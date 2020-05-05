using System.Collections.Generic;
using BL.DTOs;
using BL.DTOs.Enums;
using BL.DTOs.Filters;

namespace PL.Models.Company
{
    public class ApplicantListModel
    {
        public ApplicantFilterDto Filter { get; set; }

        public IList<ApplicantDto> Applicants { get; set; }

        public Keyword? Keyword { get; set; }
    }
}