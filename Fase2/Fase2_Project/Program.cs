using System;
using BusinessLayer;


namespace Fase2_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                showConOptions();
                int userInput;
                while (!Int32.TryParse(Console.ReadLine(), out userInput))
                    Console.WriteLine("Valor tem de ser inteiro\n");
                DbServices services;
                switch (userInput) {
                    case 1:
                        services = new DbServices(true);
                        secondMenu(services);
                        break;
                    case 2:
                        services = new DbServices(false);
                        secondMenu(services);
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
        }

        private static void secondMenu(DbServices services)
        {
            while (true)
            {
                showOptions();
                int userInput;
                while (!Int32.TryParse(Console.ReadLine(), out userInput))
                    Console.WriteLine("Valor tem de ser inteiro\n");
                if (userInput == 0) return;
                execOption(services, userInput);
            }
        }

        private static void showConOptions()
        {
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1 -> Usar ADONET");
            Console.WriteLine("2 -> Usar Entity Framework");
            Console.WriteLine("0 -> Sair");
        }

        private static void showOptions()
        {
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1 -> Obter equipa livre para uma intervenção");
            Console.WriteLine("2 -> Inserir Intervencão com procedure");
            Console.WriteLine("3 -> Inserir Equipa");
            Console.WriteLine("4 -> Atualizar Elementos a uma Equipa");
            Console.WriteLine("5 -> Intervenções num ano");
            Console.WriteLine("6 -> Inserir Intervencão");
            Console.WriteLine("7 -> Atribuir intervenção a uma equipa livre");
            Console.WriteLine("8 -> Trocar competencias entre 2 funcionarios");
            Console.WriteLine("0 -> Sair para o menu anterior");
        }

        private static void execOption(DbServices services,  int userInput)
        {
            switch (userInput)
            {
                case 1:
                    services.getEquipaLivre();
                    break;
                case 2:
                    services.insertIntervencaoWithProcedure();
                    break;
                case 3:
                    services.insertEquipa();
                    break;
                case 4:
                    services.insertOrDeleteEquipaFunc();
                    break;
                case 5:
                    services.getIntervencoesAno();
                    break;
                case 6:
                    services.insertIntervencao();
                    break;
                case 7:
                    services.insertEquipaIntervencao();
                    break;
                case 8:
                    services.changeCompetenciaFunc();
                    break;
            }
        }
    }
}
