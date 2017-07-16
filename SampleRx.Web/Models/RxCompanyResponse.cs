using SampleRx.Dal;
using System.Collections.Generic;

namespace SampleRx.Web.Models
{
    public class RxCompanyResponse : RxBaseResponse
    {
        public List<RxCompany> Companies { get; set; }
    }
}