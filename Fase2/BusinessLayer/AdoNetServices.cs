using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using Model;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BusinessLayer
{
    public class ADONetServices : IServices
    {

        Session session;
        TransactionOptions options;
        TransactionScope ts;

        public ADONetServices()
        {
            this.session = new Session();
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
        public Equipa getEquipaLivre(string competencia)
        {
            Equipa resultado;
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
                    resultado = ((EquipaMapper)equipaMapper).getFreeTeam(competencia);
                    ts.Complete();
                    return resultado;
                }
            }
        }


        public void insertIntervencaoWithProcedure(Intervencao intervencao)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                    intervencaoMapper.CreateWithSP(intervencao);
                    ts.Complete();
                }
            }
        }
        public void insertEquipa(Equipa equipa)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
                    equipaMapper.CreateWithSP(equipa);
                    ts.Complete();
                }
            }
        }

        public void insertOrDeleteEquipaFunc(EquipaFunc equipaFunc, string option)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<EquipaFunc, int> equipaFuncMapper = new EquipaFuncMapper(session);
                    ((EquipaFuncMapper)equipaFuncMapper).insertOrDeleteEquipaFunc(equipaFunc, option);
                    ts.Complete();
                }
            }
        }

        public List<Intervencao> getIntervencoesAno(int ano)
        {
            List<Intervencao> intervencoes;
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                    intervencoes = ((IntervencaoMapper)intervencaoMapper).getIntervencoesAno(ano);
                    ts.Complete();
                }
            }
            return intervencoes;
        }

        public void insertIntervencao(Intervencao intervencao)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                    intervencaoMapper.Create(intervencao);
                    ts.Complete();
                }
            }
        }
        public void insertEquipaIntervencao(Intervencao intervencao, Equipa equipa)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                    IMapper<EquipaIntervencao, int> equipaIntervencaoMapper = new EquipaIntervencaoMapper(session);
                    EquipaIntervencao equipaIntervecao = new EquipaIntervencao { equipaId = equipa.Id, idIntervencao = intervencao.id };
                    equipaIntervencaoMapper.Update(equipaIntervecao);
                    ((IntervencaoMapper)intervencaoMapper).UpdateState(intervencao);
                    ts.Complete();
                }
            }
        }

        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    ts.Complete();
                }
            }
        }

        public void clearTest(int id)
        {
            using (session)
            {
                openTransactionScope();
                using (ts)
                {
                    IMapper<EquipaIntervencao, int> equipaIntervencaoMapper = new EquipaIntervencaoMapper(session);
                    equipaIntervencaoMapper.Delete(id);
                    ts.Complete();
                }
            }
        }
    }
}
