using System;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;
using BL.QueryObjects.Common;
using BL.Services.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Companies
{
    public class CompanyService : CrudQueryServiceBase<Company, CompanyDto, CompanyFilterDto>, ICompanyService
    {
        public CompanyService(IMapper mapper, IRepository<Company> companyRepository, QueryObjectBase<CompanyDto, Company, CompanyFilterDto, IQuery<Company>> companyQueryObject)
            : base(mapper, companyRepository, companyQueryObject) { }

        protected override async Task<Company> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<CompanyDto, CompanyFilterDto>> ListCompaniesAsync(CompanyFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
