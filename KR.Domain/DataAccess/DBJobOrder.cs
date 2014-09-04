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

        public static JobOrder RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_JobOrders WHERE id = @id";
            SqlCommand getJob = new SqlCommand(queryString, conn);
            getJob.Parameters.AddWithValue("id", id);
            SqlDataReader jobsReader = getJob.ExecuteReader();
            
            JobOrder m_Job = new JobOrder();

            if(jobsReader.Read())
            {
                m_Job.Id = jobsReader.GetInt32(0);
                m_Job.DateAquired = jobsReader.GetDateTime(1);
                m_Job.CompanyId = jobsReader.GetInt32(2);
                m_Job.PositionTitle = jobsReader.GetString(3);
                m_Job.Salary = jobsReader.GetInt32(4);
                m_Job.Education = jobsReader.GetString(5);
                m_Job.RecruitmentFee = jobsReader.GetInt32(6);
                //m_Job.DatePlaced = jobsReader.GetDateTime(7);
                m_Job.KeyWords = jobsReader.GetString(9);
                m_Job.Requirements = jobsReader.GetString(10);
                m_Job.Duties = jobsReader.GetString(11);
                m_Job.ContactId = jobsReader.GetInt32(12);
                m_Job.ContactType = jobsReader.GetString(13);
            }

            conn.Close();

            return m_Job;
        }

        public static List<JobOrder> RetrieveAll()
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_JobOrders ORDER BY dateAquired DESC";
            SqlCommand getJobs = new SqlCommand(queryString, conn);
            SqlDataReader jobsReader = getJobs.ExecuteReader();

            List<JobOrder> m_Jobs = new List<JobOrder>();

            while (jobsReader.Read())
            {
                JobOrder m_Job = new JobOrder();

                m_Job.Id = jobsReader.GetInt32(0);
                m_Job.DateAquired = jobsReader.GetDateTime(1);
                m_Job.CompanyId = jobsReader.GetInt32(2);
                m_Job.PositionTitle = jobsReader.GetString(3);
                m_Job.Salary = jobsReader.GetInt32(4);
                m_Job.Education = jobsReader.GetString(5);
                m_Job.RecruitmentFee = jobsReader.GetInt32(6);
                //m_Job.DatePlaced = jobsReader.GetDateTime(7);
                m_Job.KeyWords = jobsReader.GetString(9);
                m_Job.Requirements = jobsReader.GetString(10);
                m_Job.Duties = jobsReader.GetString(11);
                m_Job.ContactId = jobsReader.GetInt32(12);
                m_Job.ContactType = jobsReader.GetString(13);

                m_Jobs.Add(m_Job);
            }



            conn.Close();

            return m_Jobs;
        }

        public static void UpdateJobOrder(JobOrder m_JobOrder)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_JobOrders SET dateAquired = @dateAquired, companyId = @companyId, positionTitle = @positionTitle, salary = @salary, education = @education, recruitmentFee = @recruitmentFee, keywords = @keywords, requirements = @requirements, duties = @duties, contactId = @contactId, contactType = @contactType WHERE id = @id";
            SqlCommand updJob = new SqlCommand(queryString, conn);
            updJob.Parameters.AddWithValue("dateAquired", m_JobOrder.DateAquired);
            updJob.Parameters.AddWithValue("companyId", m_JobOrder.CompanyId);
            updJob.Parameters.AddWithValue("positionTitle", m_JobOrder.PositionTitle);
            updJob.Parameters.AddWithValue("salary", m_JobOrder.Salary);
            updJob.Parameters.AddWithValue("education", m_JobOrder.Education);
            updJob.Parameters.AddWithValue("recruitmentFee", m_JobOrder.RecruitmentFee);
            updJob.Parameters.AddWithValue("keywords", m_JobOrder.KeyWords);
            updJob.Parameters.AddWithValue("requirements", m_JobOrder.Requirements);
            updJob.Parameters.AddWithValue("duties", m_JobOrder.Duties);
            updJob.Parameters.AddWithValue("contactId", m_JobOrder.ContactId);
            updJob.Parameters.AddWithValue("contactType", m_JobOrder.ContactType);
            updJob.Parameters.AddWithValue("id", m_JobOrder.Id);

            updJob.ExecuteNonQuery();

            conn.Close();
        }

        public static List<JobOrder> GetJobsByCompanyId(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_JobOrders WHERE companyId = @companyId";
            SqlCommand getJobs = new SqlCommand(queryString, conn);
            getJobs.Parameters.AddWithValue("companyId", id);
            SqlDataReader jobsReader = getJobs.ExecuteReader();

            List<JobOrder> m_Jobs = new List<JobOrder>();

            while (jobsReader.Read())
            {
                JobOrder m_Job = new JobOrder();
                m_Job.Id = jobsReader.GetInt32(0);
                m_Job.DateAquired = jobsReader.GetDateTime(1);
                m_Job.CompanyId = jobsReader.GetInt32(2);
                m_Job.PositionTitle = jobsReader.GetString(3);
                m_Job.Salary = jobsReader.GetInt32(4);
                m_Job.Education = jobsReader.GetString(5);
                m_Job.RecruitmentFee = jobsReader.GetInt32(6);
                //m_Job.DatePlaced = jobsReader.GetDateTime(7);
                m_Job.KeyWords = jobsReader.GetString(9);
                m_Job.Requirements = jobsReader.GetString(10);
                m_Job.Duties = jobsReader.GetString(11);
                m_Job.ContactId = jobsReader.GetInt32(12);
                m_Job.ContactType = jobsReader.GetString(13);

                m_Jobs.Add(m_Job);

            }

            conn.Close();

            return m_Jobs;
        }

        public static List<Resume> ResumesSent(int jobId)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_ResumesSent WHERE jobId = @jobId ORDER BY sentDate DESC";
            SqlCommand getRS = new SqlCommand(queryString, conn);
            getRS.Parameters.AddWithValue("jobId", jobId);
            SqlDataReader rsReader = getRS.ExecuteReader();

            List<Resume> m_Resumes = new List<Resume>();

            while (rsReader.Read())
            {
                Resume m_Resume = new Resume();
                m_Resume.Id = rsReader.GetInt32(0);
                m_Resume.CandidateId = rsReader.GetInt32(1);
                m_Resume.JobId = rsReader.GetInt32(2);
                m_Resume.DateSent = rsReader.GetDateTime(3);

                Candidate m_Cand = DBCandidate.RetrieveOne(m_Resume.CandidateId);

                m_Resume.CandidateName = m_Cand.LastName + ", " + m_Cand.FirstName;

                m_Resumes.Add(m_Resume);
            }

            conn.Close();

            return m_Resumes;
        }

        public static List<Note> GetNotes(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Notes WHERE noteType = 'Job' AND typeRelatedId = @id ORDER BY noteDate DESC";
            SqlCommand getNotes = new SqlCommand(queryString, conn);
            getNotes.Parameters.AddWithValue("id", id);
            SqlDataReader notesReader = getNotes.ExecuteReader();

            List<Note> m_Notes = new List<Note>();

            while (notesReader.Read())
            {
                Note m_Note = new Note();

                m_Note.Id = notesReader.GetInt32(0);
                m_Note.NoteDate = notesReader.GetDateTime(1);
                m_Note.NoteText = notesReader.GetString(2);
                m_Note.NoteType = notesReader.GetString(3);
                m_Note.RelatedTypeId = notesReader.GetInt32(4);

                m_Notes.Add(m_Note);
            }


            conn.Close();

            return m_Notes;
        }

        public static List<Interview> GetInterviews(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CandidateInterviews WHERE jobId = @id ORDER BY interviewDate";
            SqlCommand getInterviews = new SqlCommand(queryString, conn);
            getInterviews.Parameters.AddWithValue("id", id);
            SqlDataReader interviewReader = getInterviews.ExecuteReader();

            List<Interview> m_Interviews = new List<Interview>();

            while (interviewReader.Read())
            {
                Interview m_Interview = new Interview();

                m_Interview.Id = interviewReader.GetInt32(0);
                m_Interview.InterviewDate = interviewReader.GetDateTime(1);
                m_Interview.JobId = interviewReader.GetInt32(2);
                m_Interview.CandidateId = interviewReader.GetInt32(3);
                m_Interview.InterviewType = interviewReader.GetString(4);

                Candidate m_Cand = DBCandidate.RetrieveOne(m_Interview.CandidateId);
                m_Interview.CandidateName = m_Cand.LastName + ", " + m_Cand.FirstName;

                m_Interviews.Add(m_Interview);

            }

            conn.Close();

            return m_Interviews;
        }

        public static void SendResume(Resume m_Resume)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_ResumesSent(candidateId, jobId, dateSent) VALUES(@candidateId, @jobId, @dateSent)";
            SqlCommand insRS = new SqlCommand(queryString, conn);
            insRS.Parameters.AddWithValue("candidateId", m_Resume.CandidateId);
            insRS.Parameters.AddWithValue("jobId", m_Resume.JobId);
            insRS.Parameters.AddWithValue("dateSent", m_Resume.DateSent);
            insRS.ExecuteNonQuery();

            conn.Close();
        }
        
    }
}