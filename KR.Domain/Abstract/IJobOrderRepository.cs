using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KR.Domain.Entities;

namespace KR.Domain.Abstract
{
    public interface IJobOrderRepository
    {
        void AddJobOrder(JobOrder m_JobOrder);
        void UpdateJobOrder(JobOrder m_JobOrder);
        JobOrder RetrieveOne(int id);
        List<JobOrder> RetrieveAll();
        void DeleteJobOrder(int id);
        List<JobOrder> GetJobsByCompanyId(int id);
        List<Resume> ResumesSent(int id);
        List<Note> GetNotes(int id);
        List<Interview> GetInterviews(int id);
        void SendResume(Resume m_Resume);
        List<JobOrder> GetPlacements();
    }
}
