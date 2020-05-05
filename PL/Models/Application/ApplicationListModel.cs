using System;
using System.Collections.Generic;
using BL.DTOs;
using BL.DTOs.Enums;
using BL.DTOs.Filters;

namespace PL.Models.Application
{
    public class ApplicationListModel
    {
        public IList<ApplicationDto> Applications { get; set; }

        public ApplicationFilterDto Filter { get; set; }

        public Guid JobOfferId { get; set; }

        public Guid ApplicantId { get; set; }

        public ApplicationState? State { get; set; }
    }
}