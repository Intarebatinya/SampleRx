namespace SampleRx.Web.Utils
{
    public class AppConst
    {
        #region Request Querystring parms
        public const string RxCompanyKey = "key";
        public const string RxPageNumber = "pageNumber";
        public const string RxPageSize = "pageSize";
        #endregion

        #region Default values
        public const int pageNumber = 1;
        public const int pageSize = 10000;
        #endregion

        #region Session Keys
        public const string LoginObject = "LoginObject";
        #endregion
    }
}