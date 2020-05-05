using System;
using System.Threading.Tasks;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;
using BL.Facades.Common;
using BL.Services.Companies;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class CompanyFacade : FacadeBase
    {
        private readonly ICompanyService companyService;
        private readonly IUserCompanyService userService;

        public CompanyFacade(IUnitOfWorkProvider unitOfWorkProvider, ICompanyService companyService, IUserCompanyService userService) : base(unitOfWorkProvider)
        {
            this.companyService = companyService;
            this.userService = userService;
        }

        public async Task<CompanyDto> GetCompanyAccordingToUsernameAsync(string username)
        {
            using (UnitOfWorkProvider.Create())
            {
                var user = await userService.GetUserAccordingToUsernameAsync(username);
                return await companyService.GetAsync(user.Id);
            }
        }

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>all companies</returns>
        public async Task<QueryResultDto<CompanyDto, CompanyFilterDto>> GetAllCompaniesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await companyService.ListAllAsync();
            }
        }

        /// <summary>
        /// Performs company registration
        /// </summary>
        /// <param name="userCompanyCreateDto">company registration details</param>
        /// <returns>Registered company account ID</returns>
        public async Task<Guid> RegisterCompany(UserCompanyCreateDto userCompanyCreateDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var id = await userService.RegisterUserAsync(userCompanyCreateDto);
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
        /// GetAsync company according to ID
        /// </summary>
        /// <param name="id">ID of the company</param>
        /// <returns>The company with given ID, null otherwise</returns>
        public async Task<CompanyDto> GetCompanyAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await companyService.GetAsync(id);
            }
        }

        /// <summary>
        /// Gets companies according to filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<QueryResultDto<CompanyDto, CompanyFilterDto>> GetCompaniesAsync(CompanyFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var companyListQueryResult = await companyService.ListCompaniesAsync(filter);

                return companyListQueryResult;
            }
        }

        /// <summary>
        /// Creates company
        /// </summary>
        /// <param name = "company" > product </param >
        public async Task<Guid> CreateCompanyAsync(CompanyDto company)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var companyId = companyService.Create(company);
                await uow.Commit();
                return companyId;
            }
        }

        /// <summary>
        /// Updates company
        /// </summary>
        /// <param name="companyDto">Company details</param>
        public async Task<bool> EditCompanyAsync(CompanyDto companyDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await companyService.GetAsync(companyDto.Id, false) == null)
                {
                    return false;
                }

                await companyService.Update(companyDto);
                await uow.Commit();
                return true;
            }
        }

        /// <summary>
        /// Deletes company with given Id
        /// </summary>
        /// <param name="id">Id of the company to delete</param>
        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await companyService.GetAsync(id, false) == null)
                {
                    return false;
                }

                companyService.Delete(id);
                await uow.Commit();
                return true;
            }
        }
    }
}

