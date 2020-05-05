using System.Linq;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Common;
using BL.DTOs.Filters;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.Config
{
    public static class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<ApplicantDto, Applicant>()
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.Keywords, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteJobs, opt => opt.Ignore());
            config.CreateMap<Applicant, ApplicantDto>()
                .ForMember(dest => dest.KeywordString, opt => opt.MapFrom(applicant => string.Join(", ", applicant.Keywords.Select(keyword => keyword.Keyword))))
                .ForMember(dest => dest.KeywordNumbers, opt => opt.MapFrom(applicant => applicant.Keywords.Select(keyword => keyword.KeywordNumber).ToArray()));

            config.CreateMap<ApplicationDto, Application>()
                .ForMember(dest => dest.JobOffer, opt => opt.Ignore())
                .ForMember(dest => dest.Applicant, opt => opt.Ignore());
            config.CreateMap<Application, ApplicationDto>();

            config.CreateMap<CompanyDto, Company>()
                .ForMember(dest => dest.JobOffers, opt => opt.Ignore());
            config.CreateMap<Company, CompanyDto>();

            config.CreateMap<FavoriteJobDto, FavoriteJob>().ReverseMap()
                .ForMember(dest => dest.JobOffer, opt => opt.Ignore())
                .ForMember(dest => dest.Applicant, opt => opt.Ignore());
            config.CreateMap<FavoriteJob, FavoriteJobDto>().ReverseMap();

            config.CreateMap<JobOfferDto, JobOffer>()
                .ForMember(dest => dest.Company, opt => opt.Ignore())
                .ForMember(dest => dest.Applications, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteJobs, opt => opt.Ignore())
                .ForMember(dest => dest.Keywords, opt => opt.Ignore());
            config.CreateMap<JobOffer, JobOfferDto>()
                .ForMember(dest => dest.KeywordString, opt => opt.MapFrom(jobOffer => string.Join(", ", jobOffer.Keywords.Select(keyword => keyword.Keyword))))
                .ForMember(dest => dest.KeywordNumbers, opt => opt.MapFrom(jobOffer => jobOffer.Keywords.Select(keyword => keyword.KeywordNumber).ToArray()));

            config.CreateMap<KeywordApplicantDto, KeywordsApplicant>()
                .ForMember(dest => dest.Applicant, opt => opt.Ignore());
            config.CreateMap<KeywordsApplicant, KeywordApplicantDto>();

            config.CreateMap<KeywordJobOfferDto, KeywordsJobOffer>()
                .ForMember(dest => dest.JobOffer, opt => opt.Ignore());
            config.CreateMap<KeywordsJobOffer, KeywordJobOfferDto>();

            config.CreateMap<User, UserDto>().ReverseMap();
            config.CreateMap<User, UserApplicantCreateDto>().ReverseMap();
            config.CreateMap<User, UserCompanyCreateDto>().ReverseMap();

            config.CreateMap<Company, UserCompanyCreateDto>().ReverseMap();
            config.CreateMap<Applicant, UserApplicantCreateDto>().ReverseMap();

            config.CreateMap<QueryResult<Applicant>, QueryResultDto<ApplicantDto, ApplicantFilterDto>>();
            config.CreateMap<QueryResult<Application>, QueryResultDto<ApplicationDto, ApplicationFilterDto>>();
            config.CreateMap<QueryResult<Company>, QueryResultDto<CompanyDto, CompanyFilterDto>>();
            config.CreateMap<QueryResult<FavoriteJob>, QueryResultDto<FavoriteJobDto, FavoriteJobFilterDto>>();
            config.CreateMap<QueryResult<JobOffer>, QueryResultDto<JobOfferDto, JobOfferFilterDto>>();
            config.CreateMap<QueryResult<KeywordsApplicant>, QueryResultDto<KeywordApplicantDto, KeywordApplicantFilterDto>>();
            config.CreateMap<QueryResult<KeywordsJobOffer>, QueryResultDto<KeywordJobOfferDto, KeywordJobOfferFilterDto>>();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDto, UserFilterDto>>();
        }
    }
}
