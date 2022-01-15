using Model;
using System;
using System.Collections.Generic;
using EntityFrameworkModel;
using System.Transactions;

namespace BusinessLayer
{
    public class EntityFrameworkServices : IServices
    {
        TransactionOptions options;
        TransactionScope ts;
        EntityFrameworkManager efm;
        public EntityFrameworkServices()
        {
            efm = new EntityFrameworkManager();
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
        public Model.Equipa getEquipaLivre(string competencia)
        {
            Model.Equipa resultado = null;
            openTransactionScope();
            using (ts)
            {
                resultado = efm.getEquipaLivre(competencia);
                ts.Complete();
            }
            return resultado;
        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            openTransactionScope();
            using (ts)
            {
                efm.insertIntervencaoWithProcedure(i);
                ts.Complete();
            }
        }
        public void insertEquipa(Model.Equipa equipa)
        {
            openTransactionScope();
            using (ts)
            {
                efm.insertEquipa(equipa);
                ts.Complete();
            }
        }
        public void insertOrDeleteEquipaFunc(Model.EquipaFunc equipaFunc, string option)
        {
            openTransactionScope();
            using (ts)
            {
                efm.insertOrDeleteEquipaFunc(equipaFunc, option);
                ts.Complete();               
            }
        }
        public List<Model.Intervencao> getIntervencoesAno(int ano)
        {
            List<Model.Intervencao> intervencoes = new List<Model.Intervencao>();
            openTransactionScope();
            using (ts)
            {
                List<IntervencaoAno_Result> result = efm.getIntervencoesAno(ano);
                ts.Complete();
                if (result != null)
                {
                    foreach (IntervencaoAno_Result e in result)
                    {
                        intervencoes.Add(new Model.Intervencao
                        {
                            id = e.id,
                            descricao = e.descricao,
                            estado = null,
                            dtInicio = new DateTime(),
                            dtFim = new DateTime(),
                            valor = 0,
                            ativoId = 0,
                            meses = 0
                        });
                    }
                }
            }
            return intervencoes;
        }
        public void insertIntervencao(Model.Intervencao intervencao)
        {
            openTransactionScope();
            using (ts)
            {
                efm.insertIntervencao(intervencao);
                ts.Complete();              
            }
        }
        public void insertEquipaIntervencao(Model.Intervencao intervencao, Model.Equipa equipa)
        {
            openTransactionScope();
            using (ts)
            {
                efm.insertEquipaIntervencao(intervencao, equipa);
                ts.Complete();               
            }
        }
        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            openTransactionScope();
            using (ts)
            {
                efm.changeCompetenciaFunc(idFunc1, idFunc2, idCompFunc1, idCompFunc2);
                ts.Complete();               
            }
        }
    }
}
