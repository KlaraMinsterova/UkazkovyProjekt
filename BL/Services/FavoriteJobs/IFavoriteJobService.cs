using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;

namespace BL.Services.FavoriteJobs
{
    public interface IFavoriteJobService
    {
        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<FavoriteJobDto> GetAsync(Guid entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(FavoriteJobDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(FavoriteJobDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(Guid entityId);

        /// <summary>
        /// Gets ids of the job offers
        /// </summary>
        /// <param name="applicantId">id of applicant</param>
        /// <returns>ids of applicants with specified keywords</returns>
        Task<Guid[]> GetJobOfferIdsByApplicantIdAsync(Guid applicantId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available DTOs (for given type)</returns>
        Task<QueryResultDto<FavoriteJobDto, FavoriteJobFilterDto>> ListAllAsync();

        /// <summary>
        /// Checks if given job offer is already favorite for given applicant
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckIfFavorite(Guid applicantId, Guid jobOfferId);

        /// <summary>
        /// Gets id of favorite job offer
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobOfferId"></param>
        /// <returns>id</returns>
        Task<Guid> GetFavoriteByApplicantAndOffer(Guid applicantId, Guid jobOfferId);
    }
}
