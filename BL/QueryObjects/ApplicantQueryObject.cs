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
    public class ApplicantQueryObject : QueryObjectBase<ApplicantDto, Applicant, ApplicantFilterDto, IQuery<Applicant>>
    {
        public ApplicantQueryObject(IMapper mapper, IQuery<Applicant> query) : base(mapper, query) { }

        protected override IQuery<Applicant> ApplyWhereClause(IQuery<Applicant> query, ApplicantFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterApplicantName(filter), definedPredicates);
            AddIfDefined(FilterApplicantIds(filter), definedPredicates);

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

        private static SimplePredicate FilterApplicantName(ApplicantFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.LastName))
            {
                return null;
            }

            return new SimplePredicate(nameof(Applicant.LastName), ValueComparingOperator.Equal, filter.LastName);
        }

        private static CompositePredicate FilterApplicantIds(ApplicantFilterDto filter)
        {
            if (filter.ApplicantIds == null)
            {
                return null;
            }

            if (!filter.ApplicantIds.Any())
            {
                filter.ApplicantIds = new[] { Guid.Empty };
            }

            var idPredicates = new List<IPredicate>(filter.ApplicantIds
                .Select(id => new SimplePredicate(
                    nameof(Applicant.Id),
                    ValueComparingOperator.Equal,
                    id)));
            return new CompositePredicate(idPredicates, LogicalOperator.OR);
        }
    }
}
