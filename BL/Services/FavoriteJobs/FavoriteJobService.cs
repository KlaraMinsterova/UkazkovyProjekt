using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.QueryObjects.Common;
using BL.Services.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.FavoriteJobs
{
    public class FavoriteJobService : CrudQueryServiceBase<FavoriteJob, FavoriteJobDto, FavoriteJobFilterDto>, IFavoriteJobService
    {
        public FavoriteJobService(IMapper mapper, IRepository<FavoriteJob> favoriteJobRepository, QueryObjectBase<FavoriteJobDto, FavoriteJob, FavoriteJobFilterDto, IQuery<FavoriteJob>> favoriteJobQueryObject)
            : base(mapper, favoriteJobRepository, favoriteJobQueryObject) { }

        protected override async Task<FavoriteJob> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<Guid[]> GetJobOfferIdsByApplicantIdAsync(Guid applicantId)
        {
            var queryResult = await Query.ExecuteQuery(new FavoriteJobFilterDto { ApplicantId = applicantId });
            return queryResult.Items.Select(job => job.JobOfferId).ToArray();
        }

        public async Task<bool> CheckIfFavorite(Guid applicantId, Guid jobOfferId)
        {
            var filter = new FavoriteJobFilterDto { ApplicantId = applicantId, JobOfferId = jobOfferId };
            var result = await Query.ExecuteQuery(filter);

            return result.Items.Any();
        }

        public async Task<Guid> GetFavoriteByApplicantAndOffer(Guid applicantId, Guid jobOfferId)
        {
            var filter = new FavoriteJobFilterDto { ApplicantId = applicantId, JobOfferId = jobOfferId };
            var result = await Query.ExecuteQuery(filter);

            return result.Items.Select(job => job.Id).FirstOrDefault();
        }
    }
}
