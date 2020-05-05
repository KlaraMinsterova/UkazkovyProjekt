using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;

namespace BL.Services.Applicants
{
    public interface IApplicantService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<ApplicantDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Gets applicant according to given filter
        /// </summary>
        /// <param name="filter">The applicants filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<ApplicantDto, ApplicantFilterDto>> ListApplicantAsync(ApplicantFilterDto filter);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(ApplicantDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(ApplicantDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available DTOs (for given type)</returns>
        Task<QueryResultDto<ApplicantDto, ApplicantFilterDto>> ListAllAsync();
    }
}
