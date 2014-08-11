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

        public void DeleteStaff()
        {
        }

        public void SearchStaff()
        {
        }

        public Staff RetrieveOne(int id)
        {
            Staff m_Staff = DBStaff.RetrieveOne(id);

            return m_Staff;
        }
    }
}