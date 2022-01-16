using DataLayer.DataMappers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class Session : ISession
    {
        private string connection;
        private SqlConnection con = null;

        public Session()
        {
            connection = ConfigurationManager.ConnectionStrings["L51NG1"].ConnectionString; ;

        }

        public bool BeginTran()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
                return true;
            }
            return false;
        }

        public void closeCon()
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        public SqlCommand CreateCommand()
        {
            openCon();
            SqlCommand cmd = con.CreateCommand();
            return cmd;
        }

        public void Dispose()
        {
            if (con != null && con.State != ConnectionState.Closed)
            {
                closeCon();
            }
        }

        public SqlConnection getCurrCon()
        {
            return con;
        }

        public bool openCon()
        {
            bool sc = false;
            if (con == null)
            {
                con = new SqlConnection(connection);
                con.Open();
                sc = true;
            }
            return sc;
        }
    }
}
