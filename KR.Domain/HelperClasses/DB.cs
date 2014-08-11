using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace KR.Domain.HelperClasses
{
    public class DB
    {
        public static SqlConnection DbReadOnlyConnect()
        {
            SqlConnection conn = new SqlConnection("Data Source=" + ConfigurationManager.AppSettings["DBServer"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["DBName"] + ";User ID=" + ConfigurationManager.AppSettings["DBROUser"] + ";Password=" + ConfigurationManager.AppSettings["DBROPass"]);

            return conn;
        }

        public static SqlConnection DbWriteOnlyConnect()
        {
            SqlConnection conn = new SqlConnection("Data Source=" + ConfigurationManager.AppSettings["DBServer"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["DBName"] + ";User ID=" + ConfigurationManager.AppSettings["DBRWUser"] + ";Password=" + ConfigurationManager.AppSettings["DBRWPass"]);

            return conn;
        }
    }
}