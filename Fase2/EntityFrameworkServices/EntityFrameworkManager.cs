using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EntityFrameworkServices
{

    public class EntityFrameworkManager
    {
        TransactionOptions options;
        TransactionScope ts;
        L51NG1Entities ctx;
        public EntityFrameworkManager()
        {
            options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            ts = new TransactionScope(TransactionScopeOption.Required, options);

            
        }
        public Funcionario getFuncionario(int id)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    Funcionario f = (from i in ctx.Funcionarios where i.id == id select i).SingleOrDefault();
                    return f;
                }
            }
        }
        public List<IntervencaoAno_Result> getIntervencaoAno(int ano)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    var intervencoes = (from i in ctx.IntervencaoAno(ano) select i).ToList();
                    return intervencoes;
                }
            }
        }

    }
}
