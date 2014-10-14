using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;

namespace KR.Domain.DataAccess
{
    public class DBPlacement
    {
        public static void AddPlacement(Placement m_Placement)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Placements(jobId, candidateId, startDate, invoiceDate, paidDate, salary) VALUES(@jobId, @candidateId, @startDate, @invoiceDate, @paidDate, @salary)";
            SqlCommand insPlacement = new SqlCommand(queryString, conn);
            insPlacement.Parameters.AddWithValue("jobId", m_Placement.JobId);
            insPlacement.Parameters.AddWithValue("candidateId", m_Placement.CandidateId);
            insPlacement.Parameters.AddWithValue("startDate", m_Placement.StartDate);
            insPlacement.Parameters.AddWithValue("invoiceDate", m_Placement.InvoiceDate);
            insPlacement.Parameters.AddWithValue("paidDate", m_Placement.PaidDate);
            insPlacement.Parameters.AddWithValue("salary", m_Placement.Salary);

            insPlacement.ExecuteNonQuery();

            conn.Close();
        }

        public static void EditPlacement(Placement m_Placement)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Placements SET jobid = @jobId, candidateId, @candidateId, startDate = @startDate, invoiceDate = @invoiceDate, paidDate = @paidDate, salary = @salary WHERE id = @id";
            SqlCommand updPlacement = new SqlCommand(queryString, conn);
            updPlacement.Parameters.AddWithValue("jobId", m_Placement.JobId);
            updPlacement.Parameters.AddWithValue("candidateId", m_Placement.CandidateId);
            updPlacement.Parameters.AddWithValue("startDate", m_Placement.StartDate);
            updPlacement.Parameters.AddWithValue("invoiceDate", m_Placement.InvoiceDate);
            updPlacement.Parameters.AddWithValue("paidDate", m_Placement.PaidDate);
            updPlacement.Parameters.AddWithValue("salary", m_Placement.Salary);
            updPlacement.Parameters.AddWithValue("id", m_Placement.Id);

            updPlacement.ExecuteNonQuery();

            conn.Close();
        }

        public static void DeletePlacement(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_Placements WHERE id = @id";
            SqlCommand delPlacement = new SqlCommand(queryString ,conn);
            delPlacement.ExecuteNonQuery();

            conn.Close();
        }
    }
}