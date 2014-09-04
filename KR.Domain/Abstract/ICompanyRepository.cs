using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;

namespace KR.Domain.Abstract
{
    public interface ICompanyRepository
    {
        void AddCompany(Companies m_Company);
        void UpdateCompany(Companies m_Company);
        void DeleteCompany();
        void SearchCompanies();
        Companies RetrieveOne(int id);
        List<Companies> RetrieveAll();
        List<CompanyType> GetCompanyTypes();
        List<GlobalDataTypes.ContactSelect> GetContacts(int id);
        List<Companies> Pagination(int pageNum, string filter, int mode);
        int GetNumCompanies(string filter, int mode);

    }
}
