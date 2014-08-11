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
    }
}