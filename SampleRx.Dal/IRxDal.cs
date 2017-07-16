using System.Collections.Generic;

namespace SampleRx.Dal
{
    interface IRxDal
    {
        bool RegisterRxCompanyForLogin(RxCompanyLogin company, out string error);
        RxCompany ValidateRxCompanyForLogin(RxCompanyLogin compony, out string error);
        List<RxDataEntry> GetRxCompanyData(string companyKey, int pageNumber, int pageSize, out string error);
    }
}
