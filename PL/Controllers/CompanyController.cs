using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTOs;
using BL.DTOs.Enums;
using BL.DTOs.Filters;
using BL.Facades;
using PL.Models.Application;
using PL.Models.Company;
using PL.Models.JobOffer;

namespace PL.Controllers
{
    [Authorize(Roles = "Company")]
    public class CompanyController : Controller
    {
        public ApplicantFacade ApplicantFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }
        public JobOfferFacade JobOfferFacade { get; set; }
        public ApplicationFacade ApplicationFacade { get; set; }

        #region Company Profile

        public async Task<ActionResult> Index()
        {
            var company = await CompanyFacade.GetCompanyAccordingToUsernameAsync(User.Identity.Name);

            return View(company);
        }

        public async Task<ActionResult> ChangeCompanyInformation()
        {
            var applicant = await CompanyFacade.GetCompanyAccordingToUsernameAsync(User.Identity.Name);

            return View(applicant);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeCompanyInformation(CompanyDto companyDto)
        {
            await CompanyFacade.EditCompanyAsync(companyDto);

            return RedirectToAction("Index", "Company");
        }

        #endregion

        #region Applicant

        public async Task<ActionResult> ApplicantList()
        {
            var result = await ApplicantFacade.GetAllApplicantsAsync();
            var model = new ApplicantListModel { Applicants = new List<ApplicantDto>(result.Items) };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ApplicantList(ApplicantListModel model)
        {
            model.Filter = new ApplicantFilterDto();
            if (model.Keyword != null)
            {
                model.Filter.KeywordNumbers = new[] {(int) model.Keyword};
            }

            var result = await ApplicantFacade.GetApplicantsAsync(model.Filter);
            model.Applicants = new List<ApplicantDto>(result.Items);

            return View(model);
        }

        public async Task<ActionResult> ApplicantDetail(Guid id)
        {
            var model = await ApplicantFacade.GetApplicantAsync(id);

            return View(model);
        }

        #endregion

        #region Job Offer

        public async Task<ActionResult> JobOfferList()
        {
            var company = await CompanyFacade.GetCompanyAccordingToUsernameAsync(User.Identity.Name);
            var filter = new JobOfferFilterDto { CompanyId = company.Id };
            var result = await JobOfferFacade.GetJobOffersAsync(filter);
            var model = new JobOfferListModel { Filter = filter, JobOffers = new List<JobOfferDto>(result.Items) };

            return View(model);
        }

        public ActionResult JobOfferCreate()
        {
            var model = new JobOfferEditModel { JobOffer = new JobOfferDto(), Keywords = new List<bool>() };
            for (int i = 0; i < Enum.GetNames(typeof(Keyword)).Length; i++)
            {
                model.Keywords.Add(false);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> JobOfferCreate(JobOfferEditModel model)
        {
            var company = await CompanyFacade.GetCompanyAccordingToUsernameAsync(User.Identity.Name);
            model.JobOffer.CompanyId = company.Id;
            Guid id = await JobOfferFacade.CreateJobOfferAsync(model.JobOffer);
            await JobOfferFacade.EditJobOffersKeywordsAsync(id, model.Keywords);

            return RedirectToAction("JobOfferList", "Company");
        }

        public async Task<ActionResult> JobOfferEdit(Guid id)
        {
            var jobOffer = await JobOfferFacade.GetJobOfferAsync(id);
            var model = new JobOfferEditModel { JobOffer = jobOffer, Keywords = new List<bool>() };
            for (int i = 0; i < Enum.GetNames(typeof(Keyword)).Length; i++)
            {
                model.Keywords.Add(jobOffer.KeywordNumbers.Contains(i));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> JobOfferEdit(JobOfferEditModel model)
        {
            await JobOfferFacade.EditJobOfferAsync(model.JobOffer);
            await JobOfferFacade.EditJobOffersKeywordsAsync(model.JobOffer.Id, model.Keywords);

            return RedirectToAction("JobOfferList", "Company");
        }

        #endregion

        #region Application

        public async Task<ActionResult> ApplicationList(Guid jobOfferId)
        {
            var filter = new ApplicationFilterDto { JobOfferId = jobOfferId };
            var result = await ApplicationFacade.GetApplicationsAsync(filter);
            var model = new ApplicationListModel { Filter = filter, Applications = new List<ApplicationDto>(result.Items), JobOfferId = jobOfferId };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ApplicationList(ApplicationListModel model)
        {
            var filter = new ApplicationFilterDto { JobOfferId = model.JobOfferId };
            if (model.State != null)
            {
                filter.StateNumber = (int)model.State;
            }

            var result = await ApplicationFacade.GetApplicationsAsync(filter);
            model.Applications = new List<ApplicationDto>(result.Items);

            return View(model);
        }

        public async Task<ActionResult> ApplicationDetail(Guid id)
        {
            var model = await ApplicationFacade.GetApplicationAsync(id);

            return View(model);
        }

        public async Task<ActionResult> ApplicationAccept(Guid id, Guid jobOfferId)
        {
            var application = await ApplicationFacade.GetApplicationAsync(id);
            application.State = ApplicationState.Accepted;
            await ApplicationFacade.EditApplicationAsync(application);

            return RedirectToAction("ApplicationList", "Company", new { jobOfferId });
        }

        public async Task<ActionResult> ApplicationReject(Guid id, Guid jobOfferId)
        {
            var application = await ApplicationFacade.GetApplicationAsync(id);
            application.State = ApplicationState.Rejected;
            await ApplicationFacade.EditApplicationAsync(application);

            return RedirectToAction("ApplicationList", "Company", new { jobOfferId });
        }

        #endregion
    }
}