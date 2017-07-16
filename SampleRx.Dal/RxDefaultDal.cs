using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace SampleRx.Dal
{
    public class RxDefaultDal : IRxDal, IDisposable
    {
        private RxDefaultDalDbContext dal = new RxDefaultDalDbContext();
        

        public bool RegisterRxCompanyForLogin(RxCompanyLogin login, out string error)
        {
            error = string.Empty;
            try
            {
                var company = dal.RxCompanies.FirstOrDefault(c => c.RxCompanyId.Equals(login.RxLoginCompanyId) &&
                                                                  c.RxCompanyActive);
                if (company == null)
                {
                    error = $"The ID {login.RxLoginCompanyId} does not correspond to any Company.";
                    return false;
                }
                dal.RxCompanyLogins.Add(login);
                dal.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                error = "A server error has occurred.";
            }
            return false;
        }

        public RxCompany ValidateRxCompanyForLogin(RxCompanyLogin login, out string error)
        {
            error = string.Empty;
            try
            {
                var log = dal.RxCompanyLogins.FirstOrDefault(
                    l => l.RxLoginActive && l.RxLoginEmail.Equals(login.RxLoginEmail, StringComparison.CurrentCultureIgnoreCase)
                       && l.RxLoginHash.Equals(login.RxLoginHash));
                if (log == null)
                    error = "Invalid credentials";
                else
                    return dal.RxCompanies.FirstOrDefault(c => c.RxCompanyId.Equals(log.RxLoginCompanyId)
                                                            && c.RxCompanyActive);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                error = "A server error has occurred.";
            }
            return null;
        }

        public List<RxDataEntry> GetRxCompanyData(string companyKey, int pageNumber, int pageSize, out string error)
        {
            error = string.Empty;
            try
            {
                var company = dal.RxCompanies.FirstOrDefault(c => c.RxCompanyAccessKey.Equals(companyKey) && 
                                                                  c.RxCompanyActive);
                if (company == null)
                {
                    error = $"The key {companyKey} does not correspond to any Company.";
                    return null;
                }
                return dal.RxDataEntries.Include("RxDrug")
                                            .Where(e => e.EntryCompanyId.Equals(company.RxCompanyId))
                                            .OrderBy(e => e.EntryId)
                                            .Skip(pageNumber * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                error = "A server error has occurred.";
            }
            return null;
        }

        public void Dispose()
        {
            dal.Dispose();
        }
    }
}
