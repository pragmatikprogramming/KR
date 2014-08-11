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
        void DeleteStaff();
        void SearchStaff();
        Staff RetrieveOne(int id);
    }
}
