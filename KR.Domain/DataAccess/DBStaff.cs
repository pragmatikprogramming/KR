using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;
using System.Data.SqlClient;

namespace KR.Domain.DataAccess
{
    public class DBStaff
    {
        public static void AddStaff(Staff m_Staff)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Staff(firstName, lastName, email, companyId, title, optOut, dateEntered) VALUES(@firstName, @lastName, @email, @companyId, @title, 0, @dateEntered)";
            SqlCommand insStaff = new SqlCommand(queryString, conn);
            insStaff.Parameters.AddWithValue("firstName", m_Staff.FirstName);
            insStaff.Parameters.AddWithValue("lastName", m_Staff.LastName);
            insStaff.Parameters.AddWithValue("email", m_Staff.Email);
            insStaff.Parameters.AddWithValue("companyId", m_Staff.CompanyId);
            insStaff.Parameters.AddWithValue("title", m_Staff.Title);
            insStaff.Parameters.AddWithValue("dateEntered", DateTime.Now);

            insStaff.ExecuteNonQuery();

            conn.Close();
        }

        public static void UpdateStaff(Staff m_Staff)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Staff SET firstName = @firstName, lastName = @lastName, email = @email, companyId = @companyId, title = @title, optOut = @optOut WHERE id = @id";
            SqlCommand updateStaff = new SqlCommand(queryString, conn);
            updateStaff.Parameters.AddWithValue("firstName", m_Staff.FirstName);
            updateStaff.Parameters.AddWithValue("lastName", m_Staff.LastName);
            updateStaff.Parameters.AddWithValue("email", m_Staff.Email);
            updateStaff.Parameters.AddWithValue("companyId", m_Staff.CompanyId);
            updateStaff.Parameters.AddWithValue("title", m_Staff.Title);
            updateStaff.Parameters.AddWithValue("optOut", m_Staff.OptOut);
            updateStaff.Parameters.AddWithValue("id", m_Staff.Id);

            updateStaff.ExecuteNonQuery();

            conn.Close();
        }

        public static Staff RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Staff WHERE id = @id";
            SqlCommand getStaff = new SqlCommand(queryString, conn);
            getStaff.Parameters.AddWithValue("id", id);
            SqlDataReader staffReader = getStaff.ExecuteReader();

            Staff m_Staff = new Staff();

            if (staffReader.Read())
            {
                m_Staff.Id = staffReader.GetInt32(0);
                m_Staff.FirstName = staffReader.GetString(1);
                m_Staff.LastName = staffReader.GetString(2);
                m_Staff.Email = staffReader.GetString(3);
                m_Staff.CompanyId = staffReader.GetInt32(4);
                m_Staff.Title = staffReader.GetString(5);
                m_Staff.OptOut = (int)staffReader.GetByte(6);
                m_Staff.DateEntered = staffReader.GetDateTime(7);
            }

            conn.Close();

            return m_Staff;

        }

        public static void DeleteStaff(int id)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "DELETE FROM CRM_Staff WHERE id = @id";
            SqlCommand delStaff = new SqlCommand(queryString, conn);
            delStaff.Parameters.AddWithValue("id", id);
            delStaff.ExecuteNonQuery();

            conn.Close();
        }

        public static List<Staff> Pagination(int pageNum, int mode, string filter)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            int total = pageNum * 50;

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Staff WHERE lastName like @filter + '%' ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " s.id, s.firstName, s.lastName, s.email, s.companyId, s.title, s.optOut, s.dateEntered FROM CRM_Staff s, CRM_Companies c WHERE s.companyId = c.id AND c.description like '%' + @filter + '%' ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }
            else
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Staff ORDER BY lastName ASC) AS COMP_TABLE ORDER BY lastName DESC) AS COMP_TABLE2 ORDER BY lastName ASC";
            }

            SqlCommand getStaff = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                getStaff.Parameters.AddWithValue("filter", filter);
            }
            SqlDataReader staffReader = getStaff.ExecuteReader();

            List<Staff> m_Staffs = new List<Staff>();

            while (staffReader.Read())
            {
                Staff m_Staff = new Staff();

                m_Staff.Id = staffReader.GetInt32(0);
                m_Staff.FirstName = staffReader.GetString(1);
                m_Staff.LastName = staffReader.GetString(2);
                m_Staff.Email = staffReader.GetString(3);
                m_Staff.CompanyId = staffReader.GetInt32(4);
                m_Staff.Title = staffReader.GetString(5);
                m_Staff.OptOut = (int)staffReader.GetByte(6);
                m_Staff.DateEntered = staffReader.GetDateTime(7);

                m_Staffs.Add(m_Staff);
            }


            conn.Close();

            return m_Staffs;
        }

        public static int GetNumStaff(string filter, int mode)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Staff WHERE lastName like @filter + '%'";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Staff s, CRM_Companies c WHERE s.companyId = c.id AND c.description like '%' + @filter + '%'";
            }
            else
            {
                queryString = "SELECT COUNT(*) FROM CRM_Staff";
            }

            SqlCommand m_Staff = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                m_Staff.Parameters.AddWithValue("filter", filter);
            }
            int numStaff = (int)m_Staff.ExecuteScalar();

            conn.Close();

            return numStaff;
        }

        public static List<Staff> GetStaffByCompanyId(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Staff WHERE companyId = @companyId";
            SqlCommand getStaff = new SqlCommand(queryString, conn);
            getStaff.Parameters.AddWithValue("companyId", id);

            SqlDataReader staffReader = getStaff.ExecuteReader();

            List<Staff> m_Staffs = new List<Staff>();

            while (staffReader.Read())
            {
                Staff m_Staff = new Staff();

                m_Staff.Id = staffReader.GetInt32(0);
                m_Staff.FirstName = staffReader.GetString(1);
                m_Staff.LastName = staffReader.GetString(2);
                m_Staff.Email = staffReader.GetString(3);
                m_Staff.CompanyId = staffReader.GetInt32(4);
                m_Staff.Title = staffReader.GetString(5);
                m_Staff.OptOut = (int)staffReader.GetByte(6);
                m_Staff.DateEntered = staffReader.GetDateTime(7);

                m_Staffs.Add(m_Staff);
            }


            conn.Close();
            return m_Staffs;
        }
    }
}