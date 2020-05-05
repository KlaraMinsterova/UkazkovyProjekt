using System;
using System.Linq;
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

namespace BL.Services.Applications
{
    public class ApplicationService : CrudQueryServiceBase<Application, ApplicationDto, ApplicationFilterDto>,
        IApplicationService
    {
        public ApplicationService(IMapper mapper, IRepository<Application> applicationRepository,
            QueryObjectBase<ApplicationDto, Application, ApplicationFilterDto, IQuery<Application>>
                applicationQueryObject)
            : base(mapper, applicationRepository, applicationQueryObject)
        {
        }

        protected override async Task<Application> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<ApplicationDto, ApplicationFilterDto>> ListApplicationsAsync(
            ApplicationFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public async Task<bool> CheckIfApplied(Guid applicantId, Guid jobOfferId)
        {
            var filter = new ApplicationFilterDto {ApplicantId = applicantId, JobOfferId = jobOfferId};
            var result = await Query.ExecuteQuery(filter);

            return result.Items.Any();
        }
    }
}
