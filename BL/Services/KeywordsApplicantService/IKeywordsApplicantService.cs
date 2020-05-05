using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;

namespace BL.Services.KeywordsApplicantService
{
    public interface IKeywordsApplicantService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<KeywordApplicantDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Gets DTOs of KeywordApplicant according to filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<QueryResultDto<KeywordApplicantDto, KeywordApplicantFilterDto>> ListKeywordsApplicantAsync(KeywordApplicantFilterDto filter);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(KeywordApplicantDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(KeywordApplicantDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets ids of the applicant 
        /// </summary>
        /// <param name="keywords">keywords of applicant</param>
        /// <returns>ids of applicants with specified keywords</returns>
        Task<Guid[]> GetApplicantIdsByKeywordNumbersAsync(params int[] keywords);
           
        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available DTOs (for given type)</returns>
        Task<QueryResultDto<KeywordApplicantDto, KeywordApplicantFilterDto>> ListAllAsync();
    }
}
