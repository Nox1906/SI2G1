using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataMappers
{
    public interface ISession :IDisposable
    {
        bool beginTran();
        bool openCon();
        void endTran(bool myVote,bool IsMyTran);
        void closeCon();
        SqlConnection getCurrCon();
        SqlTransaction getCurrTran();
        SqlCommand CreateCommand();
    }
}