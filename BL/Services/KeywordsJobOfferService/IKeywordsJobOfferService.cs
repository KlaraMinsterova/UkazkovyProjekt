using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;

namespace BL.Services.KeywordsJobOfferService
{
    public interface IKeywordsJobOfferService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<KeywordJobOfferDto> GetAsync(Guid entityId, bool withIncludes = true);


        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(KeywordJobOfferDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(KeywordJobOfferDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);


        /// <summary>
        /// Gets ids of the job offers
        /// </summary>
        /// <param name="keywordNumbers">keywords of job offer</param>
        /// <returns>ids of categories with specified name</returns>
        Task<Guid[]> GetJobOfferIdsByKeywordNumbersAsync(params int[] keywordNumbers);


        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available DTOs (for given type)</returns>
        Task<QueryResultDto<KeywordJobOfferDto, KeywordJobOfferFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets DTOs according to filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<QueryResultDto<KeywordJobOfferDto, KeywordJobOfferFilterDto>> ListKeywordsJobOfferAsync(KeywordJobOfferFilterDto filter);
    }
}
