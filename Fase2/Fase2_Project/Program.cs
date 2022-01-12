//#define EF
#define ADONET

using System;
using BusinessLayer;


namespace Fase2_Project
{
    class Program
    {
        static DbServices dbServices = null;
        static void Main(string[] args)
        {
            
            while (true)
            {

                chooseFramework();
                string userInput = Console.ReadLine();
                if (userInput == "10")
                    break;
                if (userInput == "1" || userInput == "2")
                {
                    dbServices = new DbServices(Int32.Parse(userInput));
                    break;
                }
            }

            if (dbServices != null)
            {
                menu();
            }
            Console.WriteLine("END");

        }
        public static void menu()
        {
            while (true)
            {
                showOptions();
                string userInput = Console.ReadLine();
                if (userInput == "10")
                    break;
                execOption(userInput);
            }
        }
        private static void chooseFramework()
        {
            Console.WriteLine("Escolha a framework a usar:");
            Console.WriteLine("1 -> ADO.NET");
            Console.WriteLine("2 -> Entity FrameWork");
            Console.WriteLine("10 -> Sair");
        }

        private static void showOptions()
        {
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1 -> Obter equipa livre para uma intervenção");
            Console.WriteLine("2 -> Inserir Intervencão");
            Console.WriteLine("3 -> Obter intervençoes num determinado ano");
            Console.WriteLine("10 -> Sair");
        }
        private static void execOption(string userInput)
        {
            int option = int.Parse(userInput.Substring(0, 1));

            switch (option)
            {
                case 1:
                    dbServices.showEquipalivre();
                    break;
                case 2:
                    dbServices.insertIntervencaoWithProcedure();
                    break;
                case 3:
                    dbServices.showIntervencaoAno();
                    break;
            }
        }
    }
}
