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

        ISession session;

        public ADONetServices()
        {
            this.session = new Session();
        }

        public Equipa getEquipaLivre(string competencia)
        {
            Equipa resultado;
            using (session)
            {
                IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
                resultado = ((EquipaMapper)equipaMapper).getFreeTeam(competencia);
                return resultado;
            }
        }


        public void insertIntervencaoWithProcedure(Intervencao intervencao)
        {
            using (session)
            {

                IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                intervencaoMapper.CreateWithSP(intervencao);
            }
        }
        public void insertEquipa(Equipa equipa)
        {
            using (session)
            {

                IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
                equipaMapper.CreateWithSP(equipa);
            }
        }

        public void insertOrDeleteEquipaFunc(EquipaFunc equipaFunc, string option)
        {
            using (session)
            {
                IMapper<EquipaFunc, int> equipaFuncMapper = new EquipaFuncMapper(session);
                ((EquipaFuncMapper)equipaFuncMapper).insertOrDeleteEquipaFunc(equipaFunc, option);
            }
        }

        public List<Intervencao> getIntervencoesAno(int ano)
        {
            List<Intervencao> intervencoes;
            using (session)
            {
                IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                intervencoes = ((IntervencaoMapper)intervencaoMapper).getIntervencoesAno(ano);
            }
            return intervencoes;
        }

        public void insertIntervencao(Intervencao intervencao)
        {
            using (session)
            {
                IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                intervencaoMapper.Create(intervencao);
            }
        }
        public void insertEquipaIntervencao(Intervencao intervencao, Equipa equipa)
        {
            using (session)
            {
                IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                IMapper<EquipaIntervencao, int> equipaIntervencaoMapper = new EquipaIntervencaoMapper(session);
                EquipaIntervencao equipaIntervecao = new EquipaIntervencao
                {
                    Equipa = equipa,
                    Intervencao = intervencao
                };
                equipaIntervencaoMapper.Update(equipaIntervecao);
                ((IntervencaoMapper)intervencaoMapper).UpdateState(intervencao);
            }
        }

        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            throw new NotImplementedException();
        }

        public void clearTest(int id)
        {
            using (session)
            {

                EquipaIntervencao equipa = new EquipaIntervencao { Intervencao = new Intervencao { id = id } };
                IMapper<EquipaIntervencao, int> equipaIntervencaoMapper = new EquipaIntervencaoMapper(session);
                equipaIntervencaoMapper.Delete(equipa);

            }
        }
    }
}
