using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;

namespace BL.Services.Applications
{
    public interface IApplicationService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<ApplicationDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(ApplicationDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(ApplicationDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available DTOs (for given type)</returns>
        Task<QueryResultDto<ApplicationDto, ApplicationFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets products according to given filter
        /// </summary>
        /// <param name="filter">The products filter</param>
        /// <returns>Filtered results</returns>
        Task<QueryResultDto<ApplicationDto, ApplicationFilterDto>> ListApplicationsAsync(ApplicationFilterDto filter);

        /// <summary>
        /// Checks if given applicant already has an application for given offer
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobOfferId"></param>
        /// <returns></returns>
        Task<bool> CheckIfApplied(Guid applicantId, Guid jobOfferId);
    }
}
