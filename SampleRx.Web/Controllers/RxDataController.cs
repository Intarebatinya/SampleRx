using SampleRx.Dal;
using SampleRx.Web.Models;
using SampleRx.Web.Utils;
using System.Web;
using System.Web.Http;

namespace SampleRx.Web.Controllers
{
    public class RxDataController : ApiController
    {
        // GET: api/RxData?key=key&pageNumber=1&pageSize=100
        public RxDataResponse Get()
        {
            var ret = new RxDataResponse();
            var reqParms = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            if (reqParms != null)
            {
                //Request validation
                var companyKey = reqParms[AppConst.RxCompanyKey];
                if (string.IsNullOrWhiteSpace(companyKey))
                {
                    ret.Logs.Add($"{AppConst.RxCompanyKey} was not set. The request cannot be fullfilled");
                    return ret;
                }
                if (!int.TryParse(reqParms[AppConst.RxPageNumber], out int pageNumber))
                {
                    pageNumber = AppConst.pageNumber;
                    ret.Logs.Add($"{AppConst.RxPageNumber} was not set. Default value of {AppConst.pageNumber} is used.");
                }
                if (!int.TryParse(reqParms[AppConst.RxPageSize], out int pageSize))
                {
                    pageSize = AppConst.pageSize;
                    ret.Logs.Add($"{AppConst.RxPageSize} was not set. Default value of {AppConst.pageSize} is used.");
                }
                //Get Data
                var dal = new RxDefaultDal();
                ret.Data = dal.GetRxCompanyData(companyKey, pageNumber, pageSize, out string error);
                if(!string.IsNullOrWhiteSpace(error))
                    ret.Logs.Add(error);
            }
            return ret;
        }
    }
}
