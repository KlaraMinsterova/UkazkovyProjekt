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
    public class KeywordApplicantQueryObject : QueryObjectBase<KeywordApplicantDto, KeywordsApplicant, KeywordApplicantFilterDto, IQuery<KeywordsApplicant>>
    {
        public KeywordApplicantQueryObject(IMapper mapper, IQuery<KeywordsApplicant> query) : base(mapper, query) { }

        protected override IQuery<KeywordsApplicant> ApplyWhereClause(IQuery<KeywordsApplicant> query, KeywordApplicantFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterKeywordApplicantApplicantId(filter), definedPredicates);
            AddIfDefined(FilterKeywordApplicantKeywords(filter), definedPredicates);

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

        private static SimplePredicate FilterKeywordApplicantApplicantId(KeywordApplicantFilterDto filter)
        {
            if (filter.ApplicantId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(KeywordsApplicant.ApplicantId), ValueComparingOperator.Equal, filter.ApplicantId);
        }

        private static CompositePredicate FilterKeywordApplicantKeywords(KeywordApplicantFilterDto filter)
        {
            if (filter.KeywordNumbers == null || !filter.KeywordNumbers.Any())
            {
                return null;
            }

            var keywordsPredicates = new List<IPredicate>(filter.KeywordNumbers
                .Select(keywordNumber => new SimplePredicate(
                    nameof(KeywordsApplicant.KeywordNumber),
                    ValueComparingOperator.Equal,
                    keywordNumber)));
            return new CompositePredicate(keywordsPredicates, LogicalOperator.OR);
        }
    }
}
