using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Filters;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObjects
{
    public class KeywordJobOfferQueryObject : QueryObjectBase<KeywordJobOfferDto, KeywordsJobOffer, KeywordJobOfferFilterDto, IQuery<KeywordsJobOffer>>
    {
        public KeywordJobOfferQueryObject(IMapper mapper, IQuery<KeywordsJobOffer> query) : base(mapper, query) { }

        protected override IQuery<KeywordsJobOffer> ApplyWhereClause(IQuery<KeywordsJobOffer> query, KeywordJobOfferFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterKeywordJobOfferJobOfferId(filter), definedPredicates);
            AddIfDefined(FilterKeywordJobOfferKeywords(filter), definedPredicates);

            if (definedPredicates.Count == 0)
            {
                return query;
            }

            if (definedPredicates.Count == 1)
            {
                return query.Where(definedPredicates.First());
            }

            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private static void AddIfDefined(IPredicate predicate, ICollection<IPredicate> definedPredicates)
        {
            if (predicate != null)
            {
                definedPredicates.Add(predicate);
            }
        }

        private static SimplePredicate FilterKeywordJobOfferJobOfferId(KeywordJobOfferFilterDto filter)
        {
            if (filter.JobOfferId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(KeywordsJobOffer.JobOfferId), ValueComparingOperator.Equal, filter.JobOfferId);
        }

        private static CompositePredicate FilterKeywordJobOfferKeywords(KeywordJobOfferFilterDto filter)
        {
            if (filter.KeywordNumbers == null || !filter.KeywordNumbers.Any())
            {
                return null;
            }

            var keywordsPredicates = new List<IPredicate>(filter.KeywordNumbers
                .Select(keywordNumber => new SimplePredicate(
                    nameof(KeywordsJobOffer.KeywordNumber),
                    ValueComparingOperator.Equal,
                    keywordNumber)));
            return new CompositePredicate(keywordsPredicates, LogicalOperator.OR);
        }
    }
}
