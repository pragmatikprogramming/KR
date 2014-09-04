using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KR.Domain.Entities;
using KR.Domain.HelperClasses;
using System.Data.SqlClient;


namespace KR.Domain.DataAccess
{
    public class DBCompany
    {

        public static void AddCompany(Companies m_Company)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "INSERT INTO CRM_Companies(name, [type], address, city, state, country, phone, phoneSecondary, contact, website, [description], [date], crawl, zip) VALUES(@name, @type, @address, @city, @state, @country, @phone, @phoneSecondary, 0, @website, @description, @date, 1, @zip)";
            SqlCommand insCompany = new SqlCommand(queryString, conn);
            insCompany.Parameters.AddWithValue("name", m_Company.Name);
            insCompany.Parameters.AddWithValue("type", m_Company.Type);
            insCompany.Parameters.AddWithValue("address", m_Company.Address);
            insCompany.Parameters.AddWithValue("city", m_Company.City);
            insCompany.Parameters.AddWithValue("state", m_Company.State);
            insCompany.Parameters.AddWithValue("country", m_Company.Country);
            insCompany.Parameters.AddWithValue("phone", m_Company.Phone);
            insCompany.Parameters.AddWithValue("phoneSecondary", m_Company.PhoneSecondary);
            insCompany.Parameters.AddWithValue("website", m_Company.Website);
            insCompany.Parameters.AddWithValue("description", m_Company.Description);
            insCompany.Parameters.AddWithValue("date", DateTime.Now);
            insCompany.Parameters.AddWithValue("zip", m_Company.Zip);

            insCompany.ExecuteNonQuery();

            conn.Close();

        }

        public static Companies RetrieveOne(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Companies WHERE id = @id";
            SqlCommand getCompany = new SqlCommand(queryString, conn);
            getCompany.Parameters.AddWithValue("id", id);
            SqlDataReader companyReader = getCompany.ExecuteReader();

            Companies m_Company = new Companies();

            if (companyReader.Read())
            {
                m_Company.Id = companyReader.GetInt32(0);
                m_Company.Name = companyReader.GetString(1);
                m_Company.Type = companyReader.GetInt32(2);
                m_Company.Address = companyReader.GetString(3);
                m_Company.City = companyReader.GetString(4);
                m_Company.State = companyReader.GetString(5);
                m_Company.Country = companyReader.GetString(6);
                m_Company.Phone = companyReader.GetString(7);
                m_Company.PhoneSecondary = companyReader.GetString(8);
                m_Company.Contact = companyReader.GetInt32(9);
                m_Company.Website = companyReader.GetString(10);
                m_Company.Description = companyReader.GetString(11);
                m_Company.Date = companyReader.GetDateTime(12);
                m_Company.Crawl = (int)companyReader.GetByte(13);
                m_Company.Zip = companyReader.GetString(14);
            }

            conn.Close();

            return m_Company;
        }

        public static List<Companies> RetrieveAll()
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_Companies ORDER BY name";
            SqlCommand getComps = new SqlCommand(queryString, conn);
            SqlDataReader m_Comps = getComps.ExecuteReader();

            List<Companies> Companies = new List<Companies>();

            while (m_Comps.Read())
            {
                Companies m_Company = new Companies();

                m_Company.Id = m_Comps.GetInt32(0);
                m_Company.Name = m_Comps.GetString(1);
                m_Company.Type = m_Comps.GetInt32(2);
                m_Company.Address = m_Comps.GetString(3);
                m_Company.City = m_Comps.GetString(4);
                m_Company.State = m_Comps.GetString(5);
                m_Company.Country = m_Comps.GetString(6);
                m_Company.Phone = m_Comps.GetString(7);
                m_Company.PhoneSecondary = m_Comps.GetString(8);
                m_Company.Contact = m_Comps.GetInt32(9);
                m_Company.Website = m_Comps.GetString(10);
                m_Company.Description = m_Comps.GetString(11);
                m_Company.Date = m_Comps.GetDateTime(12);
                m_Company.Crawl = (int)m_Comps.GetByte(13);
                m_Company.Zip = m_Comps.GetString(14);

                Companies.Add(m_Company);
            }

            conn.Close();

            return Companies;
        }

        public static void UpdateCompany(Companies m_Company)
        {
            SqlConnection conn = DB.DbWriteOnlyConnect();
            conn.Open();

            string queryString = "UPDATE CRM_Companies SET name = @name, [type] = @type, address = @address, city = @city, state = @state, country = @country, phone = @phone, phoneSecondary = @phoneSecondary, website = @website, [description] = @description, crawl = @crawl, zip = @zip WHERE id = @id";
            SqlCommand updComp = new SqlCommand(queryString, conn);
            updComp.Parameters.AddWithValue("name", m_Company.Name);
            updComp.Parameters.AddWithValue("type", m_Company.Type);
            updComp.Parameters.AddWithValue("address", m_Company.Address);
            updComp.Parameters.AddWithValue("city", m_Company.City);
            updComp.Parameters.AddWithValue("state", m_Company.State);
            updComp.Parameters.AddWithValue("country", m_Company.Country);
            updComp.Parameters.AddWithValue("phone", m_Company.Phone);
            updComp.Parameters.AddWithValue("phoneSecondary", m_Company.PhoneSecondary);
            updComp.Parameters.AddWithValue("website", m_Company.Website);
            updComp.Parameters.AddWithValue("description", m_Company.Description);
            updComp.Parameters.AddWithValue("crawl", m_Company.Crawl);
            updComp.Parameters.AddWithValue("zip", m_Company.Zip);
            updComp.Parameters.AddWithValue("id", m_Company.Id);

            updComp.ExecuteNonQuery();

            conn.Close();
        }

        public static List<CompanyType> GetCompanyTypes()
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "SELECT * FROM CRM_CompanyTypes ORDER BY type";
            SqlCommand getCompTypes = new SqlCommand(queryString, conn);
            SqlDataReader compTypesReader = getCompTypes.ExecuteReader();

            List<CompanyType> m_CompanyTypes = new List<CompanyType>();

            while (compTypesReader.Read())
            {
                CompanyType tempCompType = new CompanyType();

                tempCompType.Id = compTypesReader.GetInt32(0);
                tempCompType.Type = compTypesReader.GetString(1);

                m_CompanyTypes.Add(tempCompType);
            }

            conn.Close();

            return m_CompanyTypes;
        }

        public static List<GlobalDataTypes.ContactSelect> GetContacts(int id)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "select id, firstName, lastName, 'Staff' as 'type' from CRM_Staff where companyId = @id UNION ALL select id, firstName, lastName, 'Candidate' as 'type' from CRM_Candidates where companyId = @id";
            SqlCommand getContacts = new SqlCommand(queryString, conn);
            getContacts.Parameters.AddWithValue("id", id);
            SqlDataReader contactsReader = getContacts.ExecuteReader();

            List<GlobalDataTypes.ContactSelect> m_Contacts = new List<GlobalDataTypes.ContactSelect>();

            while (contactsReader.Read())
            {
                GlobalDataTypes.ContactSelect m_Contact = new GlobalDataTypes.ContactSelect();

                m_Contact.id = contactsReader.GetInt32(0);
                m_Contact.firstName = contactsReader.GetString(1);
                m_Contact.lastName = contactsReader.GetString(2);
                m_Contact.type = contactsReader.GetString(3);

                m_Contacts.Add(m_Contact);
            }

            conn.Close();

            return m_Contacts;
        }

        public static List<Companies> Pagination(int pageNum, string filter, int mode)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            int total = pageNum * 50;

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Companies WHERE name like @filter + '%' ORDER BY name ASC) AS COMP_TABLE ORDER BY name DESC) AS COMP_TABLE2 ORDER BY name ASC";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Companies WHERE description like '%' + @filter + '%' ORDER BY name ASC) AS COMP_TABLE ORDER BY name DESC) AS COMP_TABLE2 ORDER BY name ASC";
            }
            else
            {
                queryString = "SELECT * FROM (SELECT TOP 50 * FROM (SELECT TOP " + total + " * FROM CRM_Companies ORDER BY name ASC) AS COMP_TABLE ORDER BY name DESC) AS COMP_TABLE2 ORDER BY name ASC";
            }

            SqlCommand getComps = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                getComps.Parameters.AddWithValue("filter", filter);
            }
            SqlDataReader m_Comps = getComps.ExecuteReader();

            List<Companies> m_Companies = new List<Companies>();

            while (m_Comps.Read())
            {
                Companies m_Company = new Companies();

                m_Company.Id = m_Comps.GetInt32(0);
                m_Company.Name = m_Comps.GetString(1);
                m_Company.Type = m_Comps.GetInt32(2);
                m_Company.Address = m_Comps.GetString(3);
                m_Company.City = m_Comps.GetString(4);
                m_Company.State = m_Comps.GetString(5);
                m_Company.Country = m_Comps.GetString(6);
                m_Company.Phone = m_Comps.GetString(7);
                m_Company.PhoneSecondary = m_Comps.GetString(8);
                m_Company.Contact = m_Comps.GetInt32(9);
                m_Company.Website = m_Comps.GetString(10);
                m_Company.Date = m_Comps.GetDateTime(12);
                m_Company.Crawl = (int)m_Comps.GetByte(13);
                m_Company.Zip = m_Comps.GetString(14);

                m_Companies.Add(m_Company);
            }
            
            conn.Close();

            return m_Companies;
        }

        public static int GetNumCompanies(string filter, int mode)
        {
            SqlConnection conn = DB.DbReadOnlyConnect();
            conn.Open();

            string queryString = "";

            if (filter != "" && mode == 0)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Companies WHERE name like @filter + '%'";
            }
            else if (filter != "" && mode == 1)
            {
                queryString = "SELECT COUNT(*) FROM CRM_Companies WHERE description like '%' + @filter + '%'";
            }
            else
            {
                queryString = "SELECT COUNT(*) FROM CRM_Companies";
            }

            SqlCommand numComps = new SqlCommand(queryString, conn);
            if (filter != "")
            {
                numComps.Parameters.AddWithValue("filter", filter);
            }
            int numCompanies = (int)numComps.ExecuteScalar();

            conn.Close();

            return numCompanies;
        }
    }
}