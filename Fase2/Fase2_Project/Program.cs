//#define EF
#define ADONET

using System;
using BusinessLayer;


namespace Fase2_Project
{
    class Program
    {
        static void Main(string[] args)
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

        private static void showOptions()
        {
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine("1 -> Obter equipa livre para uma intervenção");
            Console.WriteLine("2 -> Inserir Intervencão");
            Console.WriteLine("10 -> Sair");
        }
#if ADONET
        private static void execOption(string userInput)
        {
            int option = int.Parse(userInput.Substring(0, 1));

            switch (option)
            {
                case 1:
                    EquipaServices _equipaService = new EquipaServices();
                    _equipaService.showEquipaLivre();
                    break;
                case 2:
                    IntervencaoServices _intervencaoService = new IntervencaoServices();
                    _intervencaoService.insertIntervencaoWithProcedure();
                    break;
            }
        }
#endif
#if EF
        private static void execOption(string userInput)
        {
            int option = int.Parse(userInput.Substring(0, 1));

            switch (option)
            {
                case 1:
                    EquipaServices _equipaService = new EquipaServices();
                    _equipaService.showEquipaLivreEF();
                    break;

            }
        }
#endif
    }
}
