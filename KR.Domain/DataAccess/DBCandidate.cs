using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using KR.Domain.HelperClasses;
using KR.Domain.Entities;

namespace KR.Domain.DataAccess
{
    public class DBCandidate
    {
        public static int AddCandidate(Candidate m_Cand)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Candidates([date], lastName, firstName, email, [resume], noncompete,optout, resumeLastUpdate, resumeBinary, companyId, fileType) VALUES(@date, @lastName, @firstName, @email, @resume, @noncompete, @optout, @resumeLastUpdate, @resumeBinary, @companyId, @fileType)";
            SqlCommand insCand = new SqlCommand(queryString, conn);
            insCand.Parameters.AddWithValue("date", DateTime.Now);
            insCand.Parameters.AddWithValue("lastName", m_Cand.LastName);
            insCand.Parameters.AddWithValue("firstName", m_Cand.FirstName);
            insCand.Parameters.AddWithValue("email", m_Cand.Email);
            insCand.Parameters.AddWithValue("resume", m_Cand.Resume ?? "");
            insCand.Parameters.AddWithValue("noncompete", m_Cand.Noncompete);
            insCand.Parameters.AddWithValue("optout", m_Cand.OptOut);
            insCand.Parameters.AddWithValue("resumeLastUpdate", DateTime.Now);
            insCand.Parameters.AddWithValue("resumeBinary", m_Cand.ResumeBinary);
            insCand.Parameters.AddWithValue("companyId", m_Cand.CompanyId);
            insCand.Parameters.AddWithValue("fileType", m_Cand.FileType);

            insCand.ExecuteNonQuery();

            queryString = "SELECT IDENT_CURRENT('CRM_Candidates') AS m_Id";
            SqlCommand getIdent = new SqlCommand(queryString, conn);
            SqlDataReader idReader = getIdent.ExecuteReader();

            idReader.Read();
            int m_Id = (int)idReader.GetDecimal(0);

            conn.Close();

            return m_Id;
        }

        public static Candidate RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Candidates WHERE id = @id";
            SqlCommand getCand = new SqlCommand(queryString, conn);
            getCand.Parameters.AddWithValue("id", id);
            SqlDataReader candReader = getCand.ExecuteReader();

            Candidate m_Cand = new Candidate();

            if (candReader.Read())
            {
                m_Cand.Id = candReader.GetInt32(0);
                m_Cand.Date = candReader.GetDateTime(1);
                m_Cand.LastName = candReader.GetString(2);
                m_Cand.FirstName = candReader.GetString(3);
                m_Cand.Email = candReader.GetString(4);
                m_Cand.Resume = candReader.GetString(5);
                m_Cand.Noncompete = (int)candReader.GetByte(6);
                m_Cand.OptOut = (int)candReader.GetByte(7);
                m_Cand.ResumeLastUpdate = candReader.GetDateTime(8);
                m_Cand.ResumeBinary = candReader.GetSqlBytes(9).Buffer;
                m_Cand.CompanyId = candReader.GetInt32(10);
                m_Cand.FileType = candReader.GetString(11);
            }

            conn.Close();

            return m_Cand;
        }

        public static void UpdateCandidate(Candidate m_Cand)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Candidates SET lastName = @lastName, firstName = @firstName, email = @email, [resume] = @resume, noncompete = @noncompete, optout = @optout, resumeLastUpdate = @resumeLastUpdate, companyId = @companyId WHERE id = @id";
            SqlCommand updCand = new SqlCommand(queryString, conn);
            updCand.Parameters.AddWithValue("lastName", m_Cand.LastName);
            updCand.Parameters.AddWithValue("firstName", m_Cand.FirstName);
            updCand.Parameters.AddWithValue("email", m_Cand.Email);
            updCand.Parameters.AddWithValue("resume", m_Cand.Resume ?? "");
            updCand.Parameters.AddWithValue("noncompete", m_Cand.Noncompete);
            updCand.Parameters.AddWithValue("optout", m_Cand.OptOut);
            updCand.Parameters.AddWithValue("resumeLastUpdate", m_Cand.ResumeLastUpdate.ToString("yyyy-MM-dd"));
            updCand.Parameters.AddWithValue("id", m_Cand.Id);
            updCand.Parameters.AddWithValue("companyId", m_Cand.CompanyId);

            updCand.ExecuteNonQuery();

            conn.Close();
        }

        public static List<Candidate> RetrieveAll()
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT  * FROM CRM_Candidates ORDER BY lastName, firstName ASC";
            SqlCommand getCands = new SqlCommand(queryString, conn);
            SqlDataReader m_Cands = getCands.ExecuteReader();

            List<Candidate> m_Candidates = new List<Candidate>();

            while (m_Cands.Read())
            {
                Candidate m_Cand = new Candidate();
                m_Cand.Id = m_Cands.GetInt32(0);
                m_Cand.Date = m_Cands.GetDateTime(1);
                m_Cand.LastName = m_Cands.GetString(2);
                m_Cand.FirstName = m_Cands.GetString(3);
                m_Cand.Email = m_Cands.GetString(4);
                m_Cand.Noncompete = (int)m_Cands.GetByte(6);
                m_Cand.OptOut = (int)m_Cands.GetByte(7);
                m_Cand.ResumeLastUpdate = m_Cands.GetDateTime(8);
                m_Cand.CompanyId = m_Cands.GetInt32(10);
                m_Cand.FileType = m_Cands.GetString(11);

                m_Candidates.Add(m_Cand);
            }


            conn.Close();

            return m_Candidates;
        }

        public static List<Candidate> Pagination(int pageNum, int mode, string filter)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            int total = pageNum * 50;

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Candidates WHERE lastName like @filter + '%' ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Candidates WHERE [resume] like '%' + @filter + '%' ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }
            else
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Candidates ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }

            SqlCommand getCands = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                getCands.Parameters.AddWithValue("filter", filter);
            }
            SqlDataReader m_Cands = getCands.ExecuteReader();

            List<Candidate> m_Candidates = new List<Candidate>();

            while(m_Cands.Read())
            {
                Candidate m_Cand = new Candidate();
                m_Cand.Id = m_Cands.GetInt32(0);
                m_Cand.Date = m_Cands.GetDateTime(1);
                m_Cand.LastName = m_Cands.GetString(2);
                m_Cand.FirstName = m_Cands.GetString(3);
                m_Cand.Email = m_Cands.GetString(4);
                m_Cand.Noncompete = (int)m_Cands.GetByte(6);
                m_Cand.OptOut = (int)m_Cands.GetByte(7);
                m_Cand.ResumeLastUpdate = m_Cands.GetDateTime(8);
                m_Cand.CompanyId = m_Cands.GetInt32(10);
                m_Cand.FileType = m_Cands.GetString(11);

                m_Candidates.Add(m_Cand);
            }


            conn.Close();

            return m_Candidates;
        }

        public static int GetNumCandidates(string filter, int mode)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Candidates WHERE lastName like @filter + '%'";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Candidates WHERE [resume] like '%' + @filter + '%'";
            }
            else
            {
                queryString = "SELECT COUNT(*) FROM CRM_Candidates";
            }

            SqlCommand numCands = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                numCands.Parameters.AddWithValue("filter", filter);
            }
            int numCompanies = (int)numCands.ExecuteScalar();

            conn.Close();

            return numCompanies;
        }

        public static List<Candidate> GetCandidatesByCompanyId(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Candidates WHERE companyId = @companyId";
            SqlCommand getCandidates = new SqlCommand(queryString, conn);
            getCandidates.Parameters.AddWithValue("companyId", id);

            SqlDataReader candReader = getCandidates.ExecuteReader();

            List<Candidate> m_Candidates = new List<Candidate>();

            while (candReader.Read())
            {
                Candidate m_Cand = new Candidate();
                m_Cand.Id = candReader.GetInt32(0);
                m_Cand.Date = candReader.GetDateTime(1);
                m_Cand.LastName = candReader.GetString(2);
                m_Cand.FirstName = candReader.GetString(3);
                m_Cand.Email = candReader.GetString(4);
                m_Cand.Noncompete = (int)candReader.GetByte(6);
                m_Cand.OptOut = (int)candReader.GetByte(7);
                m_Cand.ResumeLastUpdate = candReader.GetDateTime(8);
                m_Cand.CompanyId = candReader.GetInt32(10);
                m_Cand.FileType = candReader.GetString(11);

                m_Candidates.Add(m_Cand);
            }


            conn.Close();

            return m_Candidates;
        }

        public static List<Resume> GetResumesSent(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_ResumesSent WHERE candidateId = @id ORDER BY sentDate DESC";
            SqlCommand getResumesSent = new SqlCommand(queryString, conn);
            getResumesSent.Parameters.AddWithValue("id", id);
            SqlDataReader resumeReader = getResumesSent.ExecuteReader();

            List<Resume> m_Resumes = new List<Resume>();

            while (resumeReader.Read())
            {
                Resume m_Resume = new Resume();

                m_Resume.Id = resumeReader.GetInt32(0);
                m_Resume.CandidateId = resumeReader.GetInt32(1);
                m_Resume.JobId = resumeReader.GetInt32(2);
                m_Resume.DateSent = resumeReader.GetDateTime(3);

                JobOrder m_Job = DBJobOrder.RetrieveOne(m_Resume.JobId);
                m_Resume.JobName = m_Job.PositionTitle;

                m_Resumes.Add(m_Resume);
            }

            conn.Close();

            return m_Resumes;
        }

        public static List<Interview> GetInterviews(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CandidateInterviews WHERE candidateId = @id ORDER BY interviewDate DESC";
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

                JobOrder m_Job = DBJobOrder.RetrieveOne(m_Interview.JobId);
                m_Interview.JobName = m_Job.PositionTitle;

                m_Interviews.Add(m_Interview);
            }

            conn.Close();

            return m_Interviews;
        }

        public static List<Note> GetNotes(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Notes WHERE noteType = 'Candidate' AND typeRelatedId = @id ORDER BY noteDate DESC";
            SqlCommand getNotes = new SqlCommand(queryString, conn);
            getNotes.Parameters.AddWithValue("id", id);
            SqlDataReader noteReader = getNotes.ExecuteReader();

            List<Note> m_Notes = new List<Note>();

            while (noteReader.Read())
            {
                Note m_Note = new Note();
                m_Note.Id = noteReader.GetInt32(0);
                m_Note.NoteDate = noteReader.GetDateTime(1);
                m_Note.NoteText = noteReader.GetString(2);
                m_Note.NoteType = noteReader.GetString(3);
                m_Note.RelatedTypeId = noteReader.GetInt32(4);

                m_Notes.Add(m_Note);
            }

            conn.Close();

            return m_Notes;
        }

        public static void UploadResume(int id, byte[] resume, string fileType, string fileText)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Candidates SET resumeBinary = @resumeBinary, resumeLastUpdate = @resumeLastUpdate, fileType = @fileType, [resume] = @resume WHERE id = @id";
            SqlCommand updCand = new SqlCommand(queryString, conn);
            updCand.Parameters.AddWithValue("resumeBinary", resume);
            updCand.Parameters.AddWithValue("resumeLastUpdate", DateTime.Now);
            updCand.Parameters.AddWithValue("fileType", fileType);
            updCand.Parameters.AddWithValue("resume", fileText);
            updCand.Parameters.AddWithValue("id", id);

            updCand.ExecuteNonQuery();

            conn.Close();
        }

        public static byte[] ViewResume(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT resumeBinary FROM CRM_Candidates WHERE id = @id";
            SqlCommand getRes = new SqlCommand(queryString, conn);
            getRes.Parameters.AddWithValue("id", id);

            byte[] m_Resume = getRes.ExecuteScalar() as byte[];

            conn.Close();

            return m_Resume;
        }

        public static void SendResume(int id, int jobId)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_ResumesSent(candidateId, jobId, sentDate) VALUES(@candidateId, @jobId, @dateSent)";
            SqlCommand insRS = new SqlCommand(queryString, conn);
            insRS.Parameters.AddWithValue("candidateId", id);
            insRS.Parameters.AddWithValue("jobId", jobId);
            insRS.Parameters.AddWithValue("dateSent", DateTime.Now.ToString("MM-dd-yyyy"));
            insRS.ExecuteNonQuery();

            conn.Close();
        }

        public static void DeleteCandidate(int id)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_Candidates WHERE id = @id";
            SqlCommand delCand = new SqlCommand(queryString, conn);
            delCand.Parameters.AddWithValue("id", id);
            delCand.ExecuteNonQuery();

            conn.Close();
        }
    }
}