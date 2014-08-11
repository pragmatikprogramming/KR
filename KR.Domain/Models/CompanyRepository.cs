using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;
using KR.Domain.HelperClasses;

namespace KR.Domain.Models
{
    public class CompanyRepository : ICompanyRepository
    {
        public void AddCompany(Companies m_Company)
        {
            DBCompany.AddCompany(m_Company);
        }

        public void UpdateCompany(Companies m_Company)
        {
            DBCompany.UpdateCompany(m_Company);
        }

        public void DeleteCompany()
        {
        }

        public void SearchCompanies()
        {
        }

        public Companies RetrieveOne(int id)
        {
            Companies m_Company = DBCompany.RetrieveOne(id);
            return m_Company;
        }

        public List<Companies> RetrieveAll()
        {
            List<Companies> m_Companies = DBCompany.RetrieveAll();

            return m_Companies;
        }

        public List<CompanyType> GetCompanyTypes()
        {
            List<CompanyType> m_CompanyTypes = DBCompany.GetCompanyTypes();
            return m_CompanyTypes;
        }

        public List<GlobalDataTypes.ContactSelect> GetContacts(int id)
        {
            List<GlobalDataTypes.ContactSelect> m_Contacts = DBCompany.GetContacts(id);

            return m_Contacts;
        }

        public List<Companies> Pagination(int pageNum, string filter)
        {
            List<Companies> m_Companies = DBCompany.Pagination(pageNum, filter);
            return (m_Companies);
        }

        public int GetNumCompanies(string filter)
        {
            int numCompanies = DBCompany.GetNumCompanies(filter);

            return numCompanies;

        }
    }
}