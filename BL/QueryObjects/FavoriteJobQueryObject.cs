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
    public class FavoriteJobQueryObject : QueryObjectBase<FavoriteJobDto, FavoriteJob, FavoriteJobFilterDto, IQuery<FavoriteJob>>
    {
        public FavoriteJobQueryObject(IMapper mapper, IQuery<FavoriteJob> query) : base(mapper, query) { }

        protected override IQuery<FavoriteJob> ApplyWhereClause(IQuery<FavoriteJob> query, FavoriteJobFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterFavoriteJobApplicantId(filter), definedPredicates);
            AddIfDefined(FilterFavoriteJobJobOfferId(filter), definedPredicates);

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

        private static SimplePredicate FilterFavoriteJobApplicantId(FavoriteJobFilterDto filter)
        { 
            if (filter.ApplicantId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(FavoriteJob.ApplicantId), ValueComparingOperator.Equal, filter.ApplicantId);
        }

        private static SimplePredicate FilterFavoriteJobJobOfferId(FavoriteJobFilterDto filter)
        {
            if (filter.JobOfferId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(FavoriteJob.JobOfferId), ValueComparingOperator.Equal, filter.JobOfferId);
        }
    }
}
