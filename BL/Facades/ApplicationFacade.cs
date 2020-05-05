using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;
using BL.Facades.Common;
using BL.Services.Applications;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class ApplicationFacade : FacadeBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider, IApplicationService applicationService) : base(unitOfWorkProvider)
        {
            this.applicationService = applicationService;
        }

        /// <summary>
        /// Gets all applications
        /// </summary>
        /// <returns>all applications</returns>
        public async Task<QueryResultDto<ApplicationDto, ApplicationFilterDto>> GetAllApplicationsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicationService.ListAllAsync();
            }
        }

        /// <summary>
        /// Gets applications according to filter
        /// </summary>
        /// <returns>applications</returns>
        public async Task<QueryResultDto<ApplicationDto, ApplicationFilterDto>> GetApplicationsAsync(ApplicationFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicationService.ListApplicationsAsync(filter);
            }
        }

        /// <summary>
        /// GetAsync application according to ID
        /// </summary>
        /// <param name="id">ID of the application</param>
        /// <returns>The application with given ID, null otherwise</returns>
        public async Task<ApplicationDto> GetApplicationAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicationService.GetAsync(id);
            }
        }

        /// <summary>
        /// Creates application
        /// </summary>
        /// <param name="application">product</param>
        public async Task<Guid> CreateApplicationAsync(ApplicationDto application)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var applicationId = applicationService.Create(application);
                await uow.Commit();
                return applicationId;
            }
        }

        /// <summary>
        /// Updates application
        /// </summary>
        /// <param name="applicationDto">Application details</param>
        public async Task<bool> EditApplicationAsync(ApplicationDto applicationDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await applicationService.GetAsync(applicationDto.Id, false) == null)
                {
                    return false;
                }

                await applicationService.Update(applicationDto);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Deletes application with given Id
        /// </summary>
        /// <param name="id">Id of the application to delete</param>
        public async Task<bool> DeleteApplicationAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await applicationService.GetAsync(id, false) == null)
                {
                    return false;
                }

                applicationService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Checks if given applicant already has an application for given offer
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="jobOfferId"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfApplied(Guid applicantId, Guid jobOfferId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicationService.CheckIfApplied(applicantId, jobOfferId);
            }
        }
    }
}
