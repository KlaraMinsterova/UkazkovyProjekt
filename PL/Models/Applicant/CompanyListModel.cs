using System.Collections.Generic;
using BL.DTOs;
using BL.DTOs.Filters;

namespace PL.Models.Applicant
{
    public class CompanyListModel
    {
        public CompanyFilterDto Filter { get; set; }

        public IList<CompanyDto> Companies { get; set; }
    }
}