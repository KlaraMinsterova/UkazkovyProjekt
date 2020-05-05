using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Facades.Common;
using BL.Services.Applicants;
using BL.Services.KeywordsApplicantService;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class ApplicantFacade : FacadeBase
    {
        private readonly IApplicantService applicantService;
        private readonly IKeywordsApplicantService keywordsApplicantService;
        private readonly IUserApplicantService userService;

        public ApplicantFacade(IUnitOfWorkProvider unitOfWorkProvider, IKeywordsApplicantService keywordsApplicantService, IApplicantService applicantService, IUserApplicantService userService) : base(unitOfWorkProvider)
        {
            this.applicantService = applicantService;
            this.keywordsApplicantService = keywordsApplicantService;
            this.userService = userService;
        }

        /// <summary>
        /// Gets applicants according to filter and required page
        /// </summary>
        /// <param name="filter">applicants filter</param>
        /// <returns></returns>
        public async Task<QueryResultDto<ApplicantDto, ApplicantFilterDto>> GetApplicantsAsync(ApplicantFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (filter.KeywordNumbers != null)
                {
                    filter.ApplicantIds = await keywordsApplicantService.GetApplicantIdsByKeywordNumbersAsync(filter.KeywordNumbers);
                }

                var applicantListQueryResult = await applicantService.ListApplicantAsync(filter);

                return applicantListQueryResult;
            }
        }

        public async Task<ApplicantDto> GetApplicantAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                var user = await userService.GetUserAccordingToUsernameAsync(username);
                return await applicantService.GetAsync(user.Id);
            }
        }

        /// <summary>
        /// Gets all applicants
        /// </summary>
        /// <returns>all applicants</returns>
        public async Task<QueryResultDto<ApplicantDto, ApplicantFilterDto>> GetAllApplicantsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicantService.ListAllAsync();
            }
        }

        /// <summary>
        /// GetAsync applicant according to ID
        /// </summary>
        /// <param name="id">ID of the applicant</param>
        /// <returns>The applicant with given ID, null otherwise</returns>
        public async Task<ApplicantDto> GetApplicantAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await applicantService.GetAsync(id);
            }
        }

        /// <summary>
        /// Performs Applicant registration
        /// </summary>
        /// <param name="userApplicantCreateDto">Applicant registration details</param>
        /// <returns>Registered Applicant account ID</returns>
        public async Task<Guid> RegisterApplicant(UserApplicantCreateDto userApplicantCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var id = await userService.RegisterUserAsync(userApplicantCreateDto);
                await uow.Commit();
                return id;
            }
        }

        public async Task<(bool success, string roles)> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.AuthorizeUserAsync(username, password);
            }
        }

        public async Task<UserDto> GetUserAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToUsernameAsync(username);
            }
        }

        /// <summary>
        /// Updates applicant
        /// </summary>
        /// <param name="applicantDto">Applicant details</param>
        public async Task<bool> EditApplicantAsync(ApplicantDto applicantDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await applicantService.GetAsync(applicantDto.Id, false) == null)
                {
                    return false;
                }

                await applicantService.Update(applicantDto);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Deletes applicant with given Id
        /// </summary>
        /// <param name="id">Id of the applicant to delete</param>
        public async Task<bool> DeleteApplicantAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await applicantService.GetAsync(id, false) == null)
                {
                    return false;
                }

                applicantService.Delete(id);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Updates keywords of given applicant
        /// </summary>
        /// <param name="applicantId"></param>
        /// <param name="newKeywords"></param>
        /// <returns></returns>
        public async Task<bool> EditApplicantsKeywordsAsync(Guid applicantId, IList<bool> newKeywords)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var filter = new KeywordApplicantFilterDto { ApplicantId = applicantId };
                var currentKeywords = await keywordsApplicantService.ListKeywordsApplicantAsync(filter);

                foreach (KeywordApplicantDto keyword in currentKeywords.Items)
                {
                    keywordsApplicantService.Delete(keyword.Id);
                }

                for (int i = 0; i < newKeywords.Count; i++)
                {
                    if (newKeywords[i])
                    {
                        var keywordDto = new KeywordApplicantDto {ApplicantId = applicantId, Keyword = (Keyword)i};
                        keywordsApplicantService.Create(keywordDto);
                    }
                }

                await uow.Commit();
                return true;
            }
        }
    }
}
