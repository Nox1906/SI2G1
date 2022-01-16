using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataMappers
{
    public interface ISession : IDisposable
    {
        bool BeginTran();
        bool openCon();
        void closeCon();
        SqlConnection getCurrCon();
        SqlCommand CreateCommand();
    }
}