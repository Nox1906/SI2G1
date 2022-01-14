using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using Model;
using System;
using System.Collections.Generic;


namespace BusinessLayer
{
    public class ADONetServices : IServices
    {

        Session session;

        public ADONetServices()
        {
            this.session = new Session();
        }
        public Equipa getEquipaLivre(string competencia)
        {
            IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
            Equipa resultado = null;
            try
            {
                resultado = ((EquipaMapper)equipaMapper).getFreeTeam(competencia);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                session.closeCon();
            }
            return resultado;
        }


        public void insertIntervencaoWithProcedure(Intervencao intervencao)
        {
            IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
            try
            {
                intervencaoMapper.CreateWithSP(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                session.closeCon();
            }
        }
        public void insertEquipa(Equipa equipa)
        {
            IMapper<Equipa, int> equipaMapper = new EquipaMapper(session);
            try
            {
                equipaMapper.CreateWithSP(equipa);
                Console.WriteLine("Equipa inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                session.closeCon();
            }
        }

        public void insertOrDeleteEquipaFunc(EquipaFunc equipaFunc, string option)
        {
            IMapper<EquipaFunc, int> equipaFuncMapper = new EquipaFuncMapper(session);
            try
            {
                ((EquipaFuncMapper)equipaFuncMapper).insertOrDeleteEquipaFunc(equipaFunc, option);
                Console.WriteLine("Operação inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                session.closeCon();
            }
        }

        public List<Intervencao> getIntervencoesAno(int ano)
        {
            IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
            List<Intervencao> intervencoes = null;
            try
            {
                intervencoes = ((IntervencaoMapper)intervencaoMapper).getIntervencoesAno(ano);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                session.closeCon();
            }
            return intervencoes;
        }

        public void insertIntervencao(Intervencao intervencao)
        {
            IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
            try
            {
                intervencaoMapper.Create(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.session.closeCon();
            }
        }
        public void insertEquipaIntervencao(Intervencao intervencao, Equipa equipa)
        {
            IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
            IMapper<EquipaIntervencao, int> equipaIntervencaoMapper = new EquipaIntervencaoMapper(session);
            EquipaIntervencao equipaIntervecao = new EquipaIntervencao { equipaId = equipa.Id, idIntervencao = intervencao.id };
            try
            {
                equipaIntervencaoMapper.Update(equipaIntervecao);
                ((IntervencaoMapper)intervencaoMapper).UpdateState(intervencao);
                Console.WriteLine("Equipa com id = " + equipa.Id + " atribuida à intervenção com id = " + intervencao.id + "\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.session.closeCon();
            }
        }

        public void changeCompetenciaFunc(int id,int newCompt, int oldCompt)
        {
            throw new NotImplementedException();
        }
    }
}
