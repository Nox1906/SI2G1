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

        public Model.Equipa getEquipaLivre(string competencia)
        {
            Equipa e;
            int r;
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    string sqlQuery = "SELECT [dbo].[F_ObterEquipaLivre] ({0})";
                    Object[] parameters = { competencia };
                    r = ctx.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                    e = (from i in ctx.Equipas
                         where i.id == r
                         select i).SingleOrDefault();
                }
                ts.Complete();
            }

            return e != null ? new Model.Equipa { Id = e.id, Localizacao = e.localizacao, NFunc = e.nFunc } : null;

        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_criaInter(i.id, i.descricao, i.dtInicio, i.dtFim, i.valor, i.ativoId, i.meses);
                }
                ts.Complete();
            }
        }
        public void insertEquipa(Model.Equipa e)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_criaEquipa(e.Id, e.Localizacao);
                }
                ts.Complete();
            }
        }

        public List<IntervencaoAno_Result> getIntervencoesAno(int ano)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    return ctx.IntervencaoAno(ano).ToList();
                }
            }
        }

        public void insertOrDeleteEquipaFunc(Model.EquipaFunc equipaFunc, string option)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_ActualizarElementosEquipa(equipaFunc.equipaId, equipaFunc.funcId, option, equipaFunc.supervisor);
                }
                ts.Complete();
            }
        }
    }
}

