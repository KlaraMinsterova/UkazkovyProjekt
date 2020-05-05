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
    public class ApplicationQueryObject : QueryObjectBase<ApplicationDto, Application, ApplicationFilterDto, IQuery<Application>>
    {
        public ApplicationQueryObject(IMapper mapper, IQuery<Application> query) : base(mapper, query) { }

        protected override IQuery<Application> ApplyWhereClause(IQuery<Application> query, ApplicationFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterApplicationApplicantId(filter), definedPredicates);
            AddIfDefined(FilterApplicationJobOfferId(filter), definedPredicates);
            AddIfDefined(FilterApplicationState(filter), definedPredicates);

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

        private static SimplePredicate FilterApplicationApplicantId(ApplicationFilterDto filter)
        {
            if (filter.ApplicantId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(Application.ApplicantId), ValueComparingOperator.Equal, filter.ApplicantId);
        }

        private static SimplePredicate FilterApplicationJobOfferId(ApplicationFilterDto filter)
        {
            if (filter.JobOfferId.Equals(Guid.Empty))
            {
                return null;
            }

            return new SimplePredicate(nameof(Application.JobOfferId), ValueComparingOperator.Equal, filter.JobOfferId);
        }

        private static SimplePredicate FilterApplicationState(ApplicationFilterDto filter)
        {
            if (filter.StateNumber == null)
            {
                return null;
            }

            return new SimplePredicate(nameof(Application.StateId), ValueComparingOperator.Equal, filter.StateNumber);
        }
    }
}
