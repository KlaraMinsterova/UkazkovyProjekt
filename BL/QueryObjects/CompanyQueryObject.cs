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
    public class CompanyQueryObject : QueryObjectBase<CompanyDto, Company, CompanyFilterDto, IQuery<Company>>
    {
        public CompanyQueryObject(IMapper mapper, IQuery<Company> query) : base(mapper, query) { }

        protected override IQuery<Company> ApplyWhereClause(IQuery<Company> query, CompanyFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Name))
            {
                return query;
            }

            return query.Where(new SimplePredicate(nameof(Company.Name), ValueComparingOperator.StringContains, filter.Name));
        }
    }
}
