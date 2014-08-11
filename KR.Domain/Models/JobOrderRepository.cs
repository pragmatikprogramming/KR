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
        }

        public JobOrder RetrieveOne(int id)
        {
            JobOrder m_JobOrder = new JobOrder();
            return m_JobOrder;
        }

        public List<JobOrder> RetrieveAll()
        {
            List<JobOrder> m_JobOrders = new List<JobOrder>();
            return m_JobOrders;
        }

        public void DeleteJobOrder(int id)
        {
        }
    }
}