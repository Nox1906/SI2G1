using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace EntityFrameworkModel
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
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    
                    ctx.SP_criaInter(i.id, i.descricao, i.dtInicio, i.dtFim, i.valor, i.ativoId, i.meses);
                }
            }
        }

    }
}
