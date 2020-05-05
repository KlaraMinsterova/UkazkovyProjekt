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

namespace BL.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        public JobOfferService(IMapper mapper, IRepository<JobOffer> jobOfferRepository, QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> jobOfferQueryObject)
            : base(mapper, jobOfferRepository, jobOfferQueryObject) { }

        protected override async Task<JobOffer> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> ListJobOfferAsync(JobOfferFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
