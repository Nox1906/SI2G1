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
            this.session = new Session();
        }

        public void insertInterventionWithProcedure()
        {
            IMapper<Intervencao, int> intervencaoMapper = new InterventionMapper(session);
            Intervencao intervencao = new Intervencao();
            intervencao.insertValues();
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
                this.session.closeCon();
            }
        }

        public void showFreeTeam()
        {
            IMapper<Equipa, int> equipaMapper = new TeamMapper(session);
            Console.WriteLine("Inserir competencia : ");
            string competencia = Console.ReadLine();
            try
            {
                Equipa resultado  = ((TeamMapper)equipaMapper).GetEquipaLivre(competencia);
                Console.WriteLine(resultado.ToString());
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
        public void insertTeam()
        {
            IMapper<Equipa, int> equipaMapper = new TeamMapper(session);
            Equipa equipa = new Equipa();
            equipa.insertValues();
            try
            {
                equipaMapper.CreateWithSP(equipa);
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
        public void insertOrDeleteEquipaFunc()
        {
            IMapper<EquipaFunc, int> equipaFuncMapper = new EquipaFuncMapper(session);
            EquipaFunc equipaFunc = new EquipaFunc();
            Console.WriteLine("Escolher opção: insert ; delete");
            string option;
            while (true)
            {
                string[] values = { "insert", "delete" };
                option = Console.ReadLine();
                if (values.Any(option.Contains))
                {
                    break;
                }
                else
                    Console.WriteLine("descricao nao é valida");
            }
            equipaFunc.insertValues();
            try
            {
                ((EquipaFuncMapper)equipaFuncMapper).insertOrDeleteEquipaFunc(equipaFunc, option);
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

        public void showIntervencionsByYear()
        {
            Console.WriteLine("Inserir ano");
            int ano;
            while (!Int32.TryParse(Console.ReadLine(), out ano))
                Console.WriteLine("Valor tem de ser inteiro entre 1990 e 2100\n");
            IMapper<Intervencao, int> intervencaoMapper = new InterventionMapper(session);
            try
            {
                List<Intervencao> intervencions = ((InterventionMapper)intervencaoMapper).GetInterventionsByYear(ano);
                foreach (Intervencao i in intervencions)
                {
                    Intervencao newI = intervencaoMapper.ReadById(i.id);
                    Console.WriteLine($"id: {newI.id} ; descrição: {newI.descricao}");
                }
                intervencions.Clear();
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

        public void insertIntervention()
        {
            IMapper<Intervencao, int> intervencaoMapper = new InterventionMapper(session);
            Intervencao intervencao = new Intervencao();
            intervencao.insertValues();
            intervencao.insertDiferentState();
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

        public void putTeamInIntervencion()
        {
            Intervencao intervencao = new Intervencao();
            IMapper<Intervencao, int> intervencaoMapper = new InterventionMapper(session);
            IMapper<Equipa, int> equipaMapper = new TeamMapper(session);
            intervencao.insertValues();
            try
            {
                Equipa e = ((TeamMapper)equipaMapper).GetEquipaLivre(intervencao.descricao);
                Console.WriteLine($"Equipa que será atribuída à intervençao: \n {e} ");
                intervencaoMapper.CreateWithSP(intervencao);
                intervencao.insertDiferentState();
                ((InterventionMapper)intervencaoMapper).UpdateState(intervencao);
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
