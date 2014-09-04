using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface IInterviewRepository
    {
        void AddInterview(Interview m_Interview);
        void EditInterview(Interview m_Interview);
        Interview RetrieveOne(int id);
        List<Interview> RetrieveAllCand(int id);
        List<Interview> RetrieveAllJob(int id);
        void DeleteInterview(int id);
    }
}
