using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BL.DTOs;
using BL.Facades;
using PL.Models.Accounts;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        public ApplicantFacade ApplicantFacade { get; set; }
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult RegisterApplicant()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterApplicant(UserApplicantCreateDto userApplicantCreateDto)
        {
            try
            {
                await ApplicantFacade.RegisterApplicant(userApplicantCreateDto);
                //FormsAuthentication.SetAuthCookie(userCreateDto.Username, false);

                var authTicket = new FormsAuthenticationTicket(1, userApplicantCreateDto.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Applicant");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }

        public ActionResult RegisterCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCompany(UserCompanyCreateDto userCompanyCreateDto)
        {
            try
            {
                await CompanyFacade.RegisterCompany(userCompanyCreateDto);
                //FormsAuthentication.SetAuthCookie(userCreateDto.Username, false);

                var authTicket = new FormsAuthenticationTicket(1, userCompanyCreateDto.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Company");
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            //tady to chce vyladit mužu jít přes applicantFacade / companyFacade - stejná věc na 2 místech
            (bool success, string roles) = await ApplicantFacade.Login(model.Username, model.Password);
            if (success)
            {
                var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    decodedUrl = Server.UrlDecode(returnUrl);
                }

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }

                if (roles == "Applicant")
                {
                    return RedirectToAction("Index", "Applicant");
                }

                return RedirectToAction("Index", "Company");
            }

            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
