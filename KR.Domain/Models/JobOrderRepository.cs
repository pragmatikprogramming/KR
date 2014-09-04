using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Abstract;
using KR.Domain.Entities;
using KR.Domain.DataAccess;

namespace KR.Domain.Models
{
    public class JobOrderRepository : IJobOrderRepository
    {
        public void AddJobOrder(JobOrder m_JobOrder)
        {
            DBJobOrder.AddJobOrder(m_JobOrder);
        }

        public void UpdateJobOrder(JobOrder m_JobOrder)
        {
            DBJobOrder.UpdateJobOrder(m_JobOrder);
        }

        public JobOrder RetrieveOne(int id)
        {
            JobOrder m_JobOrder = DBJobOrder.RetrieveOne(id);
            return m_JobOrder;
        }

        public List<JobOrder> RetrieveAll()
        {
            List<JobOrder> m_JobOrders = DBJobOrder.RetrieveAll();
            return m_JobOrders;
        }

        public void DeleteJobOrder(int id)
        {
        }

        public List<JobOrder> GetJobsByCompanyId(int id)
        {
            List<JobOrder> m_Jobs = DBJobOrder.GetJobsByCompanyId(id);
            return m_Jobs;
        }

        public List<Resume> ResumesSent(int id)
        {
            List<Resume> m_Resumes = DBJobOrder.ResumesSent(id);
            return m_Resumes;
        }

        public List<Note> GetNotes(int id)
        {
            List<Note> m_Notes = DBJobOrder.GetNotes(id);
            return m_Notes;
        }

        public List<Interview> GetInterviews(int id)
        {
            List<Interview> m_Interviews = DBJobOrder.GetInterviews(id);
            return m_Interviews;
        }

        public void SendResume(Resume m_Resume)
        {
            DBJobOrder.SendResume(m_Resume);
        }
    }
}