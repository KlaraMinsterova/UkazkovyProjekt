using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTOs;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Facades;
using PL.Models.Applicant;
using PL.Models.Application;
using PL.Models.JobOffer;

namespace PL.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantController : Controller
    {
        public ApplicantFacade ApplicantFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }
        public JobOfferFacade JobOfferFacade { get; set; }
        public ApplicationFacade ApplicationFacade { get; set; }

        #region Applicant profile

        public async Task<ActionResult> Index()
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);

            return View(applicant);
        }

        public async Task<ActionResult> ChangeApplicantInformation()
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            var model = new ApplicantEditModel {Applicant = applicant, Keywords = new List<bool>()};
            for (int i = 0; i < Enum.GetNames(typeof(Keyword)).Length; i++)
            {
                model.Keywords.Add(applicant.KeywordNumbers.Contains(i));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeApplicantInformation(ApplicantEditModel model)
        {
            await ApplicantFacade.EditApplicantAsync(model.Applicant);
            await ApplicantFacade.EditApplicantsKeywordsAsync(model.Applicant.Id, model.Keywords);

            return RedirectToAction("Index", "Applicant");
        }

        #endregion

        #region Company

        public async Task<ActionResult> CompanyList()
        {
            var result = await CompanyFacade.GetAllCompaniesAsync();
            var model = new CompanyListModel { Companies = new List<CompanyDto>(result.Items) };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CompanyList(CompanyListModel model)
        {
            var result = await CompanyFacade.GetCompaniesAsync(model.Filter);
            model.Companies = new List<CompanyDto>(result.Items);

            return View(model);
        }

        #endregion

        #region Job Offer

        public async Task<ActionResult> JobOfferList()
        {
            var result = await JobOfferFacade.GetAllJobOffersAsync();
            var model = new JobOfferListModel { JobOffers = new List<JobOfferDto>(result.Items) };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> JobOfferList(JobOfferListModel model)
        {
            if (!model.ApplyFavorite)
            {
                var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
                if (model.ApplyKeywords)
                {
                    model.Filter.KeywordNumbers = applicant.KeywordNumbers;
                }

                var result = await JobOfferFacade.GetJobOffersAsync(model.Filter);
                model.JobOffers = new List<JobOfferDto>(result.Items);
            }
            else
            {
                var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
                var result = await JobOfferFacade.GetFavoriteJobOffersAsync(applicant.Id);
                model = new JobOfferListModel { JobOffers = new List<JobOfferDto>(result.Items), ApplyFavorite = true };
            }

            return View(model);
        }

        public async Task<ActionResult> JobOfferDetail(Guid id)
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            var model = new JobOfferDetailModel
            {
                JobOffer = await JobOfferFacade.GetJobOfferAsync(id),
                AlreadyApplied = await ApplicationFacade.CheckIfApplied(applicant.Id, id),
                AlreadyFavorite = await JobOfferFacade.CheckIfFavorite(applicant.Id, id)
            };

            return View(model);
        }

        public async Task<ActionResult> FavoriteJobCreate(Guid jobOfferId)
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            var favoriteJob = new FavoriteJobDto
            {
                JobOfferId = jobOfferId,
                ApplicantId = applicant.Id
            };
            await JobOfferFacade.CreateFavoriteJobAsync(favoriteJob);

            return RedirectToAction("JobOfferList", "Applicant");
        }

        public async Task<ActionResult> FavoriteJobDelete(Guid jobOfferId)
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            Guid favoriteJobId = await JobOfferFacade.GetFavoriteByApplicantAndOffer(applicant.Id, jobOfferId);
            await JobOfferFacade.DeleteFavoriteJobOfferAsync(favoriteJobId);

            return RedirectToAction("JobOfferList", "Applicant");
        }

        #endregion

        #region Application

        public async Task<ActionResult> ApplicationCreate(Guid jobOfferId)
        {
            var jobOffer = await JobOfferFacade.GetJobOfferAsync(jobOfferId);
            var model = new ApplicationDto { JobOffer = jobOffer, JobOfferId = jobOfferId };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ApplicationCreate(ApplicationDto model)
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            model.ApplicantId = applicant.Id;
            model.State = ApplicationState.Undecided;
            await ApplicationFacade.CreateApplicationAsync(model);

            Guid favoriteJobId = await JobOfferFacade.GetFavoriteByApplicantAndOffer(applicant.Id, model.JobOfferId);
            if (favoriteJobId != Guid.Empty)
            {
                await JobOfferFacade.DeleteFavoriteJobOfferAsync(favoriteJobId);
            }

            return RedirectToAction("ApplicationList", "Applicant");
        }

        public async Task<ActionResult> ApplicationList()
        {
            var applicant = await ApplicantFacade.GetApplicantAccordingToUsernameAsync(User.Identity.Name);
            var filter = new ApplicationFilterDto { ApplicantId = applicant.Id };
            var result = await ApplicationFacade.GetApplicationsAsync(filter);
            var model = new ApplicationListModel { Filter = filter, Applications = new List<ApplicationDto>(result.Items), ApplicantId = applicant.Id };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ApplicationList(ApplicationListModel model)
        {
            var filter = new ApplicationFilterDto { ApplicantId = model.ApplicantId };
            if (model.State != null)
            {
                filter.StateNumber = (int)model.State;
            }

            var result = await ApplicationFacade.GetApplicationsAsync(filter);
            model.Applications = new List<ApplicationDto>(result.Items);

            return View(model);
        }

        #endregion
    }
}