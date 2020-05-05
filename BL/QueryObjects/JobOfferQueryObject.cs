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
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query) { }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterJobOfferCompanyId(filter), definedPredicates);
            AddIfDefined(FilterJobOfferPosition(filter), definedPredicates);
            AddIfDefined(FilterJobOfferLocation(filter), definedPredicates);
            AddIfDefined(FilterJobOfferIds(filter), definedPredicates);

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

        private static SimplePredicate FilterJobOfferCompanyId(JobOfferFilterDto filter)
        {
            if (filter.CompanyId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(JobOffer.CompanyId), ValueComparingOperator.Equal, filter.CompanyId);
        }

        private static SimplePredicate FilterJobOfferPosition(JobOfferFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Position))
            {
                return null;
            }

            return new SimplePredicate(nameof(JobOffer.Position), ValueComparingOperator.StringContains, filter.Position);
        }

        private static SimplePredicate FilterJobOfferLocation(JobOfferFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Location))
            {
                return null;
            }

            return new SimplePredicate(nameof(JobOffer.Location), ValueComparingOperator.Equal, filter.Location);
        }

        private static CompositePredicate FilterJobOfferIds(JobOfferFilterDto filter)
        {
            if (filter.JobOfferIds == null)
            {
                return null;
            }

            if (!filter.JobOfferIds.Any())
            {
                filter.JobOfferIds = new[] { Guid.Empty };
            }

            var idPredicates = new List<IPredicate>(filter.JobOfferIds
                .Select(id => new SimplePredicate(
                    nameof(JobOffer.Id),
                    ValueComparingOperator.Equal,
                    id)));
            return new CompositePredicate(idPredicates, LogicalOperator.OR);
        }
    }
}
