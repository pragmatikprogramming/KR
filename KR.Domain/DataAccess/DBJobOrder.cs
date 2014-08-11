using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;

namespace KR.Domain.DataAccess
{
    public class DBJobOrder
    {
        public static void AddJobOrder(JobOrder m_JobOrder)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_JobOrders(dateAquired, companyId, positionTitle, salary, education, recruitmentFee, displayOnWebsite, keywords, requirements, duties, contactId, contactType) VALUES(@dateAquired, @companyId, @positionTitle, @salary, @education, @recruitmentFee, @displayOnWebsite, @keywords, @requirements, @duties, @contactId, @contactType)";
            SqlCommand insJob = new SqlCommand(queryString, conn);
            insJob.Parameters.AddWithValue("dateAquired", m_JobOrder.DateAquired.ToString("yyyy-MM-dd"));
            insJob.Parameters.AddWithValue("companyId", m_JobOrder.CompanyId);
            insJob.Parameters.AddWithValue("positionTitle", m_JobOrder.PositionTitle);
            insJob.Parameters.AddWithValue("salary", m_JobOrder.Salary);
            insJob.Parameters.AddWithValue("education", m_JobOrder.Education);
            insJob.Parameters.AddWithValue("recruitmentFee", m_JobOrder.RecruitmentFee);
            insJob.Parameters.AddWithValue("displayOnWebSite", m_JobOrder.DisplayOnWebsite);
            insJob.Parameters.AddWithValue("keywords", m_JobOrder.KeyWords);
            insJob.Parameters.AddWithValue("requirements", m_JobOrder.Requirements);
            insJob.Parameters.AddWithValue("duties", m_JobOrder.Duties);
            insJob.Parameters.AddWithValue("contactId", m_JobOrder.ContactId);
            insJob.Parameters.AddWithValue("contactType", m_JobOrder.ContactType);

            insJob.ExecuteNonQuery();

            conn.Close();
        }

    }
}