using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Facades.Common;
using BL.Services.FavoriteJobs;
using BL.Services.JobOffers;
using BL.Services.KeywordsJobOfferService;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class JobOfferFacade : FacadeBase
    {
        private readonly IJobOfferService jobOfferService;
        private readonly IKeywordsJobOfferService keywordsJobOfferService;
        private readonly IFavoriteJobService favoriteJobService;

        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IFavoriteJobService favoriteJobService,
            IKeywordsJobOfferService keywordsJobOfferService, IJobOfferService jobOfferService) : base(
            unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
            this.keywordsJobOfferService = keywordsJobOfferService;
            this.favoriteJobService = favoriteJobService;
        }

        /// <summary>
        /// Gets all job offers according to page
        /// </summary>
        /// <returns>all job offers</returns>
        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> GetAllJobOffersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.ListAllAsync();
            }
        }

        /// <summary>
        /// GetAsync jobOffer according to ID
        /// </summary>
        /// <param name="id">ID of the jobOffer</param>
        /// <returns>The jobOffer with given ID, null otherwise</returns>
        public async Task<JobOfferDto> GetJobOfferAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetAsync(id);
            }
        }

        /// <summary>
        /// Creates jobOffer
        /// </summary>
        /// <param name="jobOffer">product</param>
        public async Task<Guid> CreateJobOfferAsync(JobOfferDto jobOffer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var jobOfferId = jobOfferService.Create(jobOffer);
                await uow.Commit();
                return jobOfferId;
            }
        }

        /// <summary>
        /// Updates jobOffer
        /// </summary>
        /// <param name="jobOfferDto">JobOffer details</param>
        public async Task<bool> EditJobOfferAsync(JobOfferDto jobOfferDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await jobOfferService.GetAsync(jobOfferDto.Id, false) == null)
                {
                    return false;
                }

                await jobOfferService.Update(jobOfferDto);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Deletes jobOffer with given Id
        /// </summary>
        /// <param name="id">Id of the jobOffer to delete</param>
        public async Task<bool> DeleteJobOfferAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await jobOfferService.GetAsync(id, false) == null)
                {
                    return false;
                }

                jobOfferService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Gets job offers according to filter
        /// </summary>
        /// <param name="filter">job offers filter</param>
        /// <returns></returns>
        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> GetJobOffersAsync(JobOfferFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (filter.KeywordNumbers != null)
                {
                    filter.JobOfferIds =
                        await keywordsJobOfferService.GetJobOfferIdsByKeywordNumbersAsync(filter.KeywordNumbers);
                }

                var jobOfferListQueryResult = await jobOfferService.ListJobOfferAsync(filter);

                return jobOfferListQueryResult;
            }
        }

        /// <summary>
        /// Gets favorite job offers of given applicant
        /// </summary>
        /// <param name="applicantId">id of applicant</param>
        /// <returns></returns>
        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> GetFavoriteJobOffersAsync(Guid applicantId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var jobOfferIds = await favoriteJobService.GetJobOfferIdsByApplicantIdAsync(applicantId);
                var filter = new JobOfferFilterDto {JobOfferIds = jobOfferIds};
                var jobOfferListQueryResult = await jobOfferService.ListJobOfferAsync(filter);

                return jobOfferListQueryResult;
            }
        }

        /// <summary>
        /// Saves job offer as favorite for given applicant
        /// </summary>
        /// <param name="favoriteJob"></param>
        /// <returns></returns>
        public async Task<Guid> CreateFavoriteJobAsync(FavoriteJobDto favoriteJob)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var favoriteJobId = favoriteJobService.Create(favoriteJob);
                await uow.Commit();
                return favoriteJobId;
            }
        }

        /// <summary>
        /// Checks if given job offer is already favorite for given applicant
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobOfferId"></param>
        /// <returns>id if exists or empty guid</returns>
        public async Task<bool> CheckIfFavorite(Guid applicantId, Guid jobOfferId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await favoriteJobService.CheckIfFavorite(applicantId, jobOfferId);
            }
        }

        /// <summary>
        /// Gets if of favorite job offer
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobOfferId"></param>
        /// <returns>id if exists or empty guid</returns>
        public async Task<Guid> GetFavoriteByApplicantAndOffer(Guid applicantId, Guid jobOfferId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await favoriteJobService.GetFavoriteByApplicantAndOffer(applicantId, jobOfferId);
            }
        }

        /// <summary>
        /// Deletes jobOffer with given Id
        /// </summary>
        /// <param name="id">Id of the jobOffer to delete</param>
        public async Task<bool> DeleteFavoriteJobOfferAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await favoriteJobService.GetAsync(id, false) == null)
                {
                    return false;
                }

                favoriteJobService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Updates keywords of given job offer
        /// </summary>
        /// <param name="jobOfferId"></param>
        /// <param name="newKeywords"></param>
        /// <returns></returns>
        public async Task<bool> EditJobOffersKeywordsAsync(Guid jobOfferId, IList<bool> newKeywords)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var filter = new KeywordJobOfferFilterDto { JobOfferId = jobOfferId };
                var currentKeywords = await keywordsJobOfferService.ListKeywordsJobOfferAsync(filter);

                foreach (KeywordJobOfferDto keyword in currentKeywords.Items)
                {
                    keywordsJobOfferService.Delete(keyword.Id);
                }

                for (int i = 0; i < newKeywords.Count; i++)
                {
                    if (newKeywords[i])
                    {
                        var keywordDto = new KeywordJobOfferDto { JobOfferId = jobOfferId, Keyword = (Keyword)i };
                        keywordsJobOfferService.Create(keywordDto);
                    }
                }

                await uow.Commit();
                return true;
            }
        }
    }
}
