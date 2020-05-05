using System;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;
using BL.QueryObjects.Common;
using BL.Services.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Applicants
{
    public class ApplicantService : CrudQueryServiceBase<Applicant, ApplicantDto, ApplicantFilterDto>, IApplicantService
    {
        public ApplicantService(IMapper mapper, IRepository<Applicant> applicantRepository, QueryObjectBase<ApplicantDto, Applicant, ApplicantFilterDto, IQuery<Applicant>> applicantQueryObject)
            : base(mapper, applicantRepository, applicantQueryObject) { }

        protected override async Task<Applicant> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<ApplicantDto, ApplicantFilterDto>> ListApplicantAsync(ApplicantFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
