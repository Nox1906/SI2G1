using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Model;

namespace BusinessLayer
{
    public class DbServices
    {
        IServices servicesContext;
        public DbServices(bool fm)
        {
            if (fm)
                servicesContext = new ADONetServices();
            else
                servicesContext = new EntityFrameworkServices();
        }

        public void getEquipaLivre()
        {
            Console.WriteLine("Inserir competencia : ");
            string competencia = Console.ReadLine();
            try
            {
                Equipa eq = servicesContext.getEquipaLivre(competencia);
                Console.WriteLine(eq.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não há equipas disponiveis");
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }
        public void insertIntervencaoWithProcedure()
        {
            try
            {
                Intervencao intervencao = insertIntervencaoValues();
                servicesContext.insertIntervencaoWithProcedure(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        public void insertEquipa()
        {
            Equipa equipa = new Equipa();
            int id;
            Console.WriteLine("Inserir id da Equipa");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            equipa.Id = id;
            Console.WriteLine("Inserir localização");
            equipa.Localizacao = Console.ReadLine();
            try
            {
                servicesContext.insertEquipa(equipa);
                Console.WriteLine("Equipa inserida com sucesso \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        public void insertOrDeleteEquipaFunc()
        {
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
            int id;
            Console.WriteLine("Inserir id do Funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            equipaFunc.funcId = id;

            Console.WriteLine("Inserir id da Equipa");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            equipaFunc.equipaId = id;

            Console.WriteLine("Inserir id do Supervisor");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            equipaFunc.supervisor = id;
            try
            {
                servicesContext.insertOrDeleteEquipaFunc(equipaFunc, option);
                Console.WriteLine("Operação realizada com sucesso \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        public void getIntervencoesAno()
        {
            Console.WriteLine("Inserir ano");
            int ano;
            while (!Int32.TryParse(Console.ReadLine(), out ano))
                Console.WriteLine("Valor tem de ser inteiro entre 1990 e 2100\n");
            try
            {
                List<Intervencao> intervencoes = servicesContext.getIntervencoesAno(ano);
                foreach (Intervencao i in intervencoes)
                {
                    Console.WriteLine($"id: {i.id} ; descrição: {i.descricao}");
                }
                intervencoes.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                Console.WriteLine("Não existem intervenções nesse ano");
            }
        }

        public void insertIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            try
            {
                servicesContext.insertIntervencao(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }
        public void insertEquipaIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            try
            {
                Equipa equipa = servicesContext.getEquipaLivre(intervencao.descricao);
                Console.WriteLine($"Equipa que será atribuída à intervençao: \n {equipa} ");
                servicesContext.insertIntervencaoWithProcedure(intervencao);
                intervencao.estado = insertEstado();
                servicesContext.insertEquipaIntervencao(intervencao, equipa);
                Console.WriteLine("Equipa com id = " + equipa.Id + " atribuida à intervenção com id = " + intervencao.id + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        public void testes()
        {
            Intervencao intervencao = new Intervencao
            {
                id = 1234,
                descricao = "avaria",
                estado = "em análise",
                dtInicio = DateTime.Parse("28-10-2021"),
                dtFim = DateTime.Parse("02-12-2021"),
                valor = 50,
                ativoId = 1,
                meses = 2
            };
            string tipo;
            if (servicesContext is ADONetServices) tipo = "ADONetServices"; else tipo = "EFServices";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                servicesContext.insertIntervencao(intervencao);
                Equipa equipa = servicesContext.getEquipaLivre(intervencao.descricao);
                servicesContext.insertEquipaIntervencao(intervencao, equipa);
                servicesContext.clearTest(intervencao.id);
            }
            sw.Stop();
            Console.WriteLine("tempo medido usando {0} = {1}", tipo, sw.Elapsed);
        }
        public void changeCompetenciaFunc()
        {
            int idFunc1;
            int idFunc2;
            int idComptFunc1;
            int idComptFunc2;
            Console.WriteLine("Inserir id do primeiro funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out idFunc1))
                Console.WriteLine("Valor tem de ser inteiro\n");
            Console.WriteLine("Inserir id do segundo funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out idFunc2))
                Console.WriteLine("Valor tem de ser inteiro\n");
            Console.WriteLine("Inserir id da competencia do primeiro funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out idComptFunc1))
                Console.WriteLine("Valor tem de ser inteiro\n");
            Console.WriteLine("Inserir id da competencia do segundo funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out idComptFunc2))
                Console.WriteLine("Valor tem de ser inteiro\n");
            try
            {
                servicesContext.changeCompetenciaFunc(idFunc1, idFunc2, idComptFunc1, idComptFunc2);
                Console.WriteLine("Competencias trocadas com sucesso entre funcionario " + idFunc1 + " e " + idFunc2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Intervencao insertIntervencaoValues()
        {
            Intervencao intervencao = new Intervencao();
            Console.WriteLine("Inserir id da Intervencao");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            intervencao.id = id;
            Console.WriteLine("Inserir opção da descricao da Intervencao : inspecção; rutura ; avaria");
            while (true)
            {
                string[] values = { "inspecção", "rutura", "avaria" };
                string result = Console.ReadLine();
                if (values.Any(result.Contains))
                {
                    intervencao.descricao = result;
                    break;
                }
                else
                    Console.WriteLine("descricao nao é valida");
            }
            intervencao.estado = "por atribuir";
            Console.WriteLine("Inserir dtInicio da Intervencao no formato : YYYY-MM-DD");
            DateTime dtIni;
            while (!DateTime.TryParse(Console.ReadLine(), out dtIni))
            {
                Console.WriteLine("Data nao é valida\n");
            }
            intervencao.dtInicio = dtIni;
            DateTime dtfim;
            while (true)
            {
                Console.WriteLine("Inserir dtFim da Intervencao no formato : YYYY-MM-DD e maior que a data de inicio");
                if (DateTime.TryParse(Console.ReadLine(), out dtfim))
                {
                    if (DateTime.Compare(dtIni, dtfim) < 0)
                        break;
                }
                Console.WriteLine("Data nao é valida\n");
            }
            intervencao.dtFim = dtfim;
            Console.WriteLine("Inserir valor da Intervencao");
            decimal valor;
            while (!decimal.TryParse(Console.ReadLine(), out valor))
                Console.WriteLine("Valor tem de ser decimal\n");
            intervencao.valor = valor;
            Console.WriteLine("Inserir Ativo da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            intervencao.ativoId = id;
            Console.WriteLine("Inserir meses da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            intervencao.meses = id;
            return intervencao;
        }
        public string insertEstado()
        {
            Console.WriteLine("Inserir opção de estado: concluído; em execução ; em análise ; por atribuir");
            while (true)
            {
                string[] values = { "concluído", "em execução", "em análise", "por atribuir" };
                string result = Console.ReadLine();
                if (values.Any(result.Contains))
                {
                    return result;

                }
                else
                    Console.WriteLine("estado nao é valido");
            }
        }
    }
}
