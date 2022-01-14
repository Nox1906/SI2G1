using System;
using System.Collections.Generic;
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
            Equipa eq = servicesContext.getEquipaLivre(competencia);
            if (eq != null)
                Console.WriteLine(eq.ToString());
            else
                Console.WriteLine("Não há equipas disponiveis");
        }
        public void insertIntervencaoWithProcedure()
        {
            Intervencao intervencao = insertIntervencaoValues();
            servicesContext.insertIntervencaoWithProcedure(intervencao);
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
            servicesContext.insertEquipa(equipa);
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
            servicesContext.insertOrDeleteEquipaFunc(equipaFunc, option);

        }

        public void getIntervencoesAno()
        {
            Console.WriteLine("Inserir ano");
            int ano;
            while (!Int32.TryParse(Console.ReadLine(), out ano))
                Console.WriteLine("Valor tem de ser inteiro entre 1990 e 2100\n");
            List<Intervencao> intervencoes = servicesContext.getIntervencoesAno(ano);
            if (intervencoes != null && intervencoes.Any())
            {
                foreach (Intervencao i in intervencoes)
                {
                    Console.WriteLine($"id: {i.id} ; descrição: {i.descricao}");
                }
                intervencoes.Clear();
            }
            else
                Console.WriteLine("Não existem intervenções nesse ano");
        }
        public void insertIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            servicesContext.insertIntervencao(intervencao);
        }

        public void insertEquipaIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            try
            {
                Equipa e = servicesContext.getEquipaLivre(intervencao.descricao);
                Console.WriteLine($"Equipa que será atribuída à intervençao: \n {e} ");
                servicesContext.insertIntervencaoWithProcedure(intervencao);
                intervencao.estado = insertEstado();
                servicesContext.insertEquipaIntervencao(intervencao, e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void changeCompetenciaFunc()
        {
            int id;
            int newCompt;
            int oldCompt;
            Console.WriteLine("Inserir id do funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            Console.WriteLine("Inserir id da competencia a substituir");
            while (!Int32.TryParse(Console.ReadLine(), out oldCompt))
                Console.WriteLine("Valor tem de ser inteiro\n");
            Console.WriteLine("Inserir id da nova competencia");
            while (!Int32.TryParse(Console.ReadLine(), out newCompt))
                Console.WriteLine("Valor tem de ser inteiro\n");
            servicesContext.changeCompetenciaFunc(id, newCompt, oldCompt);

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
