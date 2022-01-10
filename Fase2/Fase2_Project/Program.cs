using System;
using System.Data.Entity;
using BusinessLayer;
using DataLayer.QueryObjects;
using EntityFrameworkServices;


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
                getAndExecOption(userInput);
            }       
        }

        private static void showOptions()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 -> Get free team");
            Console.WriteLine("10 -> Exit");
        }
  
        private static void getAndExecOption(string userInput)
        {
            int option = int.Parse(userInput.Substring(0, 1));

            switch (option)
            {
                case 1:
                    EquipaServices _equipaService = new EquipaServices();
                    _equipaService.showEquipaLivre();
                    break;

            }
        }
    }
}
