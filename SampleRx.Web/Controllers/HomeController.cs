using SampleRx.Dal;
using SampleRx.Web.Utils;
using System.Web.Mvc;

namespace SampleRx.Web.Models
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Helpers.GetSessionValue(AppConst.LoginObject) is RxCompany company)
            {
                return View(company);
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}