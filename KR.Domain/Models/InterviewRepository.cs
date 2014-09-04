using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;

namespace KR.Domain.Models
{
    public class InterviewRepository : IInterviewRepository
    {
        public void AddInterview(Interview m_Interview)
        {
            DBInterview.AddInterview(m_Interview);
        }

        public void EditInterview(Interview m_Interview)
        {
            DBInterview.EditInterview(m_Interview);
        }

        public Interview RetrieveOne(int id)
        {
            Interview m_Interview = DBInterview.RetrieveOne(id);
            return m_Interview;
        }

        public List<Interview> RetrieveAllCand(int id)
        {
            List<Interview> m_Interviews = DBInterview.RetrieveAllCand(id);
            return m_Interviews;
        }

        public List<Interview> RetrieveAllJob(int id)
        {
            List<Interview> m_Interviews = DBInterview.RetrieveAllJob(id);
            return m_Interviews;
        }

        public void DeleteInterview(int id)
        {
            DBInterview.DeleteInterview(id);
        }
    }
}