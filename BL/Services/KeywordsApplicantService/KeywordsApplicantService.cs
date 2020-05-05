using System;
using System.Linq;
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

namespace BL.Services.KeywordsApplicantService
{
    public class KeywordsApplicantService : CrudQueryServiceBase<KeywordsApplicant, KeywordApplicantDto, KeywordApplicantFilterDto>, IKeywordsApplicantService
    {
        public KeywordsApplicantService(IMapper mapper, IRepository<KeywordsApplicant> keywordsApplicantRepository, QueryObjectBase<KeywordApplicantDto, KeywordsApplicant, KeywordApplicantFilterDto, IQuery<KeywordsApplicant>> keywordsApplicantQueryObject)
            : base(mapper, keywordsApplicantRepository, keywordsApplicantQueryObject) { }

        protected override async Task<KeywordsApplicant> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    
        public async Task<Guid[]> GetApplicantIdsByKeywordNumbersAsync(params int[] keywordNumbers)
        {
            var queryResult = await Query.ExecuteQuery(new KeywordApplicantFilterDto { KeywordNumbers = keywordNumbers });
            return queryResult.Items.Select(keyword => keyword.ApplicantId).ToArray();
        }

        public async Task<QueryResultDto<KeywordApplicantDto, KeywordApplicantFilterDto>> ListKeywordsApplicantAsync(KeywordApplicantFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
