using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;

namespace KR.Domain.Models
{
    public class StaffRepository : IStaffRepository
    {
        public void AddStaff(Staff m_Staff)
        {
            DBStaff.AddStaff(m_Staff);
        }

        public void EditStaff(Staff m_Staff)
        {
            DBStaff.UpdateStaff(m_Staff);
        }

        public void DeleteStaff(int id)
        {
            DBStaff.DeleteStaff(id);
        }

        public void SearchStaff()
        {
        }

        public Staff RetrieveOne(int id)
        {
            Staff m_Staff = DBStaff.RetrieveOne(id);

            return m_Staff;
        }

        public List<Staff> Pagination(int pageNum, string filter, int mode)
        {
            List<Staff> m_Staffs = DBStaff.Pagination(pageNum, mode, filter);

            return m_Staffs;
        }

        public int GetNumStaff(string filter, int mode)
        {
            int numStaff = DBStaff.GetNumStaff(filter, mode);

            return numStaff;
        }

        public List<Staff> GetStaffByCompanyId(int id)
        {
            List<Staff> m_Staff = DBStaff.GetStaffByCompanyId(id);
            return m_Staff;
        }
    }
}