using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Transactions;
using Model;

namespace BusinessLayer
{
    public class DbServices
    {
        IServices servicesContext;
        TransactionOptions options;
        protected TransactionScope ts;
        public DbServices(bool fm)
        {
            options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            if (fm)
                servicesContext = new ADONetServices();
            else
                servicesContext = new EntityFrameworkServices();
        }

        public void openTransactionScope()
        {
            ts = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public void getEquipaLivre()
        {
            Console.WriteLine("Inserir competencia : ");
            string competencia = Console.ReadLine();
            openTransactionScope();
            using (ts)
            {
                try
                {
                    Equipa eq = servicesContext.getEquipaLivre(competencia);
                    Console.WriteLine(eq.ToString());
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Não existem equipas disponiveis");
                }
            }
        }
        public void insertIntervencaoWithProcedure()
        {
            openTransactionScope();
            using (ts)
            {
                try
                {
                    Intervencao intervencao = insertIntervencaoValues();
                    servicesContext.insertIntervencaoWithProcedure(intervencao);
                    Console.WriteLine("Intervenção inserida com sucesso \n");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
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
            openTransactionScope();
            using (ts)
            {
                try
                {
                    servicesContext.insertEquipa(equipa);
                    Console.WriteLine("Equipa inserida com sucesso \n");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
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
            equipaFunc.Equipa = new Equipa { Id = id };

            Console.WriteLine("Inserir id do Supervisor");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            equipaFunc.supervisor = id;

            openTransactionScope();
            using (ts)
            {
                try
                {
                    servicesContext.insertOrDeleteEquipaFunc(equipaFunc, option);
                    ts.Complete();
                    Console.WriteLine("Operação realizada com sucesso \n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }
        }

        public void getIntervencoesAno()
        {
            Console.WriteLine("Inserir ano");
            int ano;
            while (!Int32.TryParse(Console.ReadLine(), out ano))
                Console.WriteLine("Valor tem de ser inteiro entre 1990 e 2100\n");
            openTransactionScope();
            using (ts)
            {
                try
                {
                    List<Intervencao> intervencoes = servicesContext.getIntervencoesAno(ano);
                    foreach (Intervencao i in intervencoes)
                    {
                        Console.WriteLine($"id: {i.id} ; descrição: {i.descricao}");
                    }
                    intervencoes.Clear();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                    Console.WriteLine("Não existem intervenções nesse ano");
                }
            }
        }

        public void insertIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            openTransactionScope();
            using (ts)
            {
                try
                {
                    servicesContext.insertIntervencao(intervencao);
                    Console.WriteLine("Intervenção inserida com sucesso \n");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }
        }

        public void getIntervencao()
        {
            Console.WriteLine("Insira Id da Intervencao \n");
            openTransactionScope();
            using (ts)
            {
                try
                {
                    int id;
                    while (!Int32.TryParse(Console.ReadLine(), out id))
                        Console.WriteLine("Valor tem de ser inteiro\n");
                    servicesContext.getIntervencao(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }
        }
        public void insertEquipaIntervencao()
        {
            Intervencao intervencao = insertIntervencaoValues();
            openTransactionScope();
            using (ts)
            {
                try
                {
                    servicesContext.insertIntervencaoWithProcedure(intervencao);
                    Equipa equipa = servicesContext.getEquipaLivre(intervencao.descricao);
                    Console.WriteLine($"Equipa que será atribuída à intervençao: \n {equipa} ");
                    intervencao.estado = insertEstado();
                    servicesContext.insertEquipaIntervencao(intervencao, equipa);
                    Console.WriteLine("Equipa com id = " + equipa.Id + " atribuida à intervenção com id = " + intervencao.id + "\n");
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }
        }
        public void testes()
        {
            Intervencao intervencao = new Intervencao
            {
                id = 1234,
                descricao = "inspecção",
                estado = "por atribuir",
                dtInicio = DateTime.Parse("28-10-2021"),
                dtFim = DateTime.Parse("02-12-2021"),
                valor = 50,
                Ativo = new Ativo { id = 2 },
                meses = 2
            };
            string tipo;
            if (servicesContext is ADONetServices) tipo = "ADONetServices"; else tipo = "EFServices";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            openTransactionScope();
            try
            {
                using (ts)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        servicesContext.insertIntervencao(intervencao);
                        Equipa equipa = servicesContext.getEquipaLivre(intervencao.descricao);
                        servicesContext.insertEquipaIntervencao(intervencao, equipa);
                        servicesContext.clearTest(intervencao.id);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
            finally
            {
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
            openTransactionScope();
            using (ts)
            {
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
        }

        private Intervencao insertIntervencaoValues()
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
            intervencao.Ativo = new Ativo { id = id };
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
