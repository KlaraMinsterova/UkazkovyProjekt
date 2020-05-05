using BL.DTOs;
using System.Collections.Generic;

namespace PL.Models.Applicant
{
    public class ApplicantEditModel
    {
        public ApplicantDto Applicant { get; set; }

        public IList<bool> Keywords { get; set; }
    }
}