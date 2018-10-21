using System;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataAccess
{
    public class DB
    {
        string ConnectionString = null;
        protected SQLiteConnection m_dbConnection = null;

        public DB()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            }
            catch { }
        }

        protected void Open()
        {
            m_dbConnection = new SQLiteConnection(ConnectionString);
            m_dbConnection.Open();
        }

        protected void Close()
        {
            m_dbConnection.Close();

            m_dbConnection = null;
        }
    }
}
