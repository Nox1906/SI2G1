using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class ADONetServices : IServices
    {

        Session session;

        public ADONetServices()
        {
            
        }
        public Equipa getEquipaLivre(string competencia)
        {
            session = new Session();
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
            session = new Session();
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
            session = new Session();
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
            session = new Session();
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
            session = new Session();
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
            session = new Session();
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
        public void insertEquipaIntervencao(Intervencao intervencao)
        {
            session = new Session();
            IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
            try
            {
                
                ((IntervencaoMapper)intervencaoMapper).UpdateState(intervencao);
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
    }
}
