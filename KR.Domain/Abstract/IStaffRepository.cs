using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface IStaffRepository
    {
        void AddStaff(Staff m_Staff);
        void EditStaff(Staff m_Staff);
        void DeleteStaff(int id);
        void SearchStaff();
        Staff RetrieveOne(int id);
        List<Staff> Pagination(int pageNum, string filter, int mode);
        int GetNumStaff(string filter, int mode);
        List<Staff> GetStaffByCompanyId(int id);
    }
}
