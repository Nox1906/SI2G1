using DataLayer.DataMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public abstract class Mapper
    {
        protected ISession session;
        TransactionOptions options;
        protected TransactionScope ts;



        protected Mapper(ISession s)
        {
            this.session = s;
            options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
        }

        public void openTransactionScope()
        {
            ts = new TransactionScope(TransactionScopeOption.Required, options);
        }
    }
}
