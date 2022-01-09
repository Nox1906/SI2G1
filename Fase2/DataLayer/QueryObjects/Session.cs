using DataLayer.DataMappers;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class Session : ISession
    {
        private string connection;
        private SqlConnection con = null;
        private SqlTransaction tran = null;
        private bool TranVotes;
        public Session()
        {
            connection = ConfigurationManager.ConnectionStrings["L51NG1"].ConnectionString; ;
        }
        public bool beginTran()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
                return true;
            }
            return false;
        }

        public void closeCon(bool IsMyCon)
        {
            if (IsMyCon && con != null)
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
            if (con != null) con.Dispose();
        }

        public void endTran(bool myVote, bool IsMyTran)
        {
            TranVotes &= myVote;
            if (IsMyTran)
            {
                if (TranVotes) tran.Commit();
                else tran.Rollback();
            }
            tran = null;
        }

        public SqlConnection getCurrCon()
        {
            return con;
        }

        public SqlTransaction getCurrTran()
        {

            return tran;
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
