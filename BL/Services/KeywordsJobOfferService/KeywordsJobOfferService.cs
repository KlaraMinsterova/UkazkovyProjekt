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

namespace BL.Services.KeywordsJobOfferService
{
    public class KeywordsJobOfferService : CrudQueryServiceBase<KeywordsJobOffer, KeywordJobOfferDto, KeywordJobOfferFilterDto>, IKeywordsJobOfferService
    {
        public KeywordsJobOfferService(IMapper mapper, IRepository<KeywordsJobOffer> keywordsJobOfferRepository, QueryObjectBase<KeywordJobOfferDto, KeywordsJobOffer, KeywordJobOfferFilterDto, IQuery<KeywordsJobOffer>> keywordsJobOfferQueryObject)
            : base(mapper, keywordsJobOfferRepository, keywordsJobOfferQueryObject) { }

        protected override async Task<KeywordsJobOffer> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<Guid[]> GetJobOfferIdsByKeywordNumbersAsync(params int[] keywordNumbers)
        {
            var queryResult = await Query.ExecuteQuery(new KeywordJobOfferFilterDto { KeywordNumbers = keywordNumbers });
            return queryResult.Items.Select(keyword => keyword.JobOfferId).ToArray();
        }

        public async Task<QueryResultDto<KeywordJobOfferDto, KeywordJobOfferFilterDto>> ListKeywordsJobOfferAsync(KeywordJobOfferFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
