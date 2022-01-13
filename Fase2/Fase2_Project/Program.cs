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
                IServices services;
                switch (userInput) {
                    case 1:
                        services = new ADONetServices();
                        secondMenu(services);
                        break;
                    case 2:
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
        }

        private static void secondMenu(IServices services)
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
            Console.WriteLine("4 -> Inserir Elementos a uma Equipa");
            Console.WriteLine("5 -> Intervenções num ano");
            Console.WriteLine("6 -> Inserir Intervencão");
            Console.WriteLine("7 -> Atribuir intervenção a uma equipa livre");
            Console.WriteLine("0 -> Sair para o menu anterior");
        }

        private static void execOption(IServices services,  int userInput)
        {
            switch (userInput)
            {
                case 1:
                    services.showFreeTeam();
                    break;
                case 2:
                    services.insertInterventionWithProcedure();
                    break;
                case 3:
                    services.insertTeam();
                    break;
                case 4:
                    services.insertOrDeleteEquipaFunc();
                    break;
                case 5:
                    services.showIntervencionsByYear();
                    break;
                case 6:
                    services.insertIntervention();
                    break;
                case 7:
                    services.putTeamInIntervencion();
                    break;
            }
        }
    }
}
