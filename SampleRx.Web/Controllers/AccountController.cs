using SampleRx.Dal;
using SampleRx.Web.Models;
using SampleRx.Web.Utils;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace SampleRx.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private RxDefaultDal dal = new RxDefaultDal();
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Create a login object
            var login = new RxCompanyLogin
            {
                RxLoginEmail = model.Email,
                RxLoginActive = true,
                RxLoginHash = Helpers.getHash(model.Password)
            };
            var company = dal.ValidateRxCompanyForLogin(login, out string error);
            if (company != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, true);
                //Save the login object
                Helpers.SaveSessionValue(company, AppConst.LoginObject);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
       
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Helpers.SaveSessionValue(null, AppConst.LoginObject);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Dispose();
            }
            base.Dispose(disposing);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create a login object
                var login = new RxCompanyLogin
                {
                    RxLoginCompanyId = model.CompanyId,
                    RxLoginEmail = model.Email,
                    RxLoginActive = true,
                    RxLoginHash = Helpers.getHash(model.Password)
                };
                if (dal.RegisterRxCompanyForLogin(login, out string error)){
                    var company = dal.ValidateRxCompanyForLogin(login, out error);
                    if (company != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        //Save the login object
                        Helpers.SaveSessionValue(company, AppConst.LoginObject);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }
    }
}