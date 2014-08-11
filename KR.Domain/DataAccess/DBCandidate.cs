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
        public static void AddCandidate(Candidate m_Cand)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Candidates([date], lastName, firstName, email, [resume], noncompete,optout, resumeLastUpdate, resumeBinary, companyId) VALUES(@date, @lastName, @firstName, @email, @resume, @noncompete, @optout, @resumeLastUpdate, @resumeBinary, @companyId)";
            SqlCommand insCand = new SqlCommand(queryString, conn);
            insCand.Parameters.AddWithValue("date", DateTime.Now);
            insCand.Parameters.AddWithValue("lastName", m_Cand.LastName);
            insCand.Parameters.AddWithValue("firstName", m_Cand.FirstName);
            insCand.Parameters.AddWithValue("email", m_Cand.Email);
            insCand.Parameters.AddWithValue("resume", m_Cand.Resume);
            insCand.Parameters.AddWithValue("noncompete", m_Cand.Noncompete);
            insCand.Parameters.AddWithValue("optout", m_Cand.OptOut);
            insCand.Parameters.AddWithValue("resumeLastUpdate", DateTime.Now);
            insCand.Parameters.AddWithValue("resumeBinary", m_Cand.ResumeBinary);
            insCand.Parameters.AddWithValue("companyId", m_Cand.CompanyId);

            insCand.ExecuteNonQuery();

            conn.Close();
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
    }
}