using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;

namespace KR.Domain.DataAccess
{
    public class DBInterview
    {
        public static void AddInterview(Interview m_Interview)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_CandidateInterviews(interviewDate, jobId, candidateId, interviewType) VALUES(@interviewDate, @jobId, @candidateId, @interviewType)";
            SqlCommand insInt = new SqlCommand(queryString, conn);
            insInt.Parameters.AddWithValue("interviewDate", m_Interview.InterviewDate);
            insInt.Parameters.AddWithValue("jobId", m_Interview.JobId);
            insInt.Parameters.AddWithValue("candidateId", m_Interview.CandidateId);
            insInt.Parameters.AddWithValue("interviewType", m_Interview.InterviewType);

            insInt.ExecuteNonQuery();

            conn.Close();
        }

        public static void EditInterview(Interview m_Interview)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_CandidateInterviews SET interviewDate = @interviewDate, jobId = @jobId, candidateId = @candidateId, interviewType = @interviewType WHERE id = @id";
            SqlCommand updInt = new SqlCommand(queryString, conn);
            updInt.Parameters.AddWithValue("interviewDate", m_Interview.InterviewDate);
            updInt.Parameters.AddWithValue("jobId", m_Interview.JobId);
            updInt.Parameters.AddWithValue("candidateId", m_Interview.CandidateId);
            updInt.Parameters.AddWithValue("interviewType ", m_Interview.InterviewType);
            updInt.Parameters.AddWithValue("id", m_Interview.Id);

            updInt.ExecuteNonQuery();

            conn.Close();
        }

        public static Interview RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CandidateInterviews WHERE id = @id";
            SqlCommand getInt = new SqlCommand(queryString, conn);
            getInt.Parameters.AddWithValue("id", id);
            SqlDataReader intReader = getInt.ExecuteReader();

            Interview m_Interview = new Interview();

            if (intReader.Read())
            {
                m_Interview.Id = intReader.GetInt32(0);
                m_Interview.InterviewDate = intReader.GetDateTime(1);
                m_Interview.JobId = intReader.GetInt32(2);
                m_Interview.CandidateId = intReader.GetInt32(3);
                m_Interview.InterviewType = intReader.GetString(4);

                JobOrder m_JobOrder = DBJobOrder.RetrieveOne(m_Interview.JobId);
                m_Interview.JobName = m_JobOrder.PositionTitle;

                Candidate m_Candidate = DBCandidate.RetrieveOne(m_Interview.CandidateId);
                m_Interview.CandidateName = m_Candidate.LastName + ", " + m_Candidate.FirstName;
            }

            conn.Close();

            return m_Interview;
        }

        public static List<Interview> RetrieveAllCand(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CandidateInterviews WHERE candidateId = @id ORDER BY interviewDate DESC";
            SqlCommand getInt = new SqlCommand(queryString, conn);
            getInt.Parameters.AddWithValue("id", id);
            SqlDataReader intReader = getInt.ExecuteReader();

            List<Interview> m_Interviews = new List<Interview>();

            while (intReader.Read())
            {
                Interview m_Interview = new Interview();

                m_Interview.Id = intReader.GetInt32(0);
                m_Interview.InterviewDate = intReader.GetDateTime(1);
                m_Interview.JobId = intReader.GetInt32(2);
                m_Interview.CandidateId = intReader.GetInt32(3);
                m_Interview.InterviewType = intReader.GetString(4);

                JobOrder m_JobOrder = DBJobOrder.RetrieveOne(m_Interview.JobId);
                m_Interview.JobName = m_JobOrder.PositionTitle;

                Candidate m_Candidate = DBCandidate.RetrieveOne(m_Interview.CandidateId);
                m_Interview.CandidateName = m_Candidate.LastName + ", " + m_Candidate.FirstName;

                m_Interviews.Add(m_Interview);
            }

            conn.Close();

            return m_Interviews;
        }

        public static List<Interview> RetrieveAllJob(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CandidateInterviews WHERE jobId = @id ORDER BY interviewDate DESC";
            SqlCommand getInt = new SqlCommand(queryString, conn);
            getInt.Parameters.AddWithValue("id", id);
            SqlDataReader intReader = getInt.ExecuteReader();

            List<Interview> m_Interviews = new List<Interview>();

            while (intReader.Read())
            {
                Interview m_Interview = new Interview();

                m_Interview.Id = intReader.GetInt32(0);
                m_Interview.InterviewDate = intReader.GetDateTime(1);
                m_Interview.JobId = intReader.GetInt32(2);
                m_Interview.CandidateId = intReader.GetInt32(3);
                m_Interview.InterviewType = intReader.GetString(4);

                m_Interviews.Add(m_Interview);
            }

            conn.Close();

            return m_Interviews;
        }

        public static void DeleteInterview(int id)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_CandidateInterviews WHERE id = @id";
            SqlCommand delInt = new SqlCommand(queryString, conn);
            delInt.Parameters.AddWithValue("id", id);

            delInt.ExecuteNonQuery();

            conn.Close();
        }
    }
}