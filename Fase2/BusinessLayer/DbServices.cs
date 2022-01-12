using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLayer
{
    public class DbServices
    {
        IServices servicesContext;
        public DbServices(int fm)
        {
            if (fm == (int)FrameworkEnum.ADONET)
                servicesContext = new AdoNetServices();
            else
                servicesContext = new EntityFrameworkServices();
        }

        public void showEquipalivre()
        {
            servicesContext.showEquipaLivre();
        }
        public void showIntervencaoAno()

        {
            Console.WriteLine("Inserir um ano : ");
            int ano = Int32.Parse(Console.ReadLine());
            servicesContext.showIntervencaoAno(ano);
        }
        public void insertIntervencaoWithProcedure()
        {
            Intervencao i = new Intervencao();
            Console.WriteLine("Inserir intervencao da Intervencao");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            i.id = id;
            Console.WriteLine("Inserir opção da descricao da Intervencao : inspecção; descricao ; avaria");
            while (true)
            {
                string[] values = { "inspecção", "descricao", "avaria" };
                string result = Console.ReadLine();
                if (values.Any(result.Contains))
                {
                    i.descricao = result;
                    break;
                }
                else
                    Console.WriteLine("descricao nao é valida");
            }
            i.estado = "por atribuir";

            Console.WriteLine("Inserir dtInicio da Intervencao no formato : YYYY-MM-DD");
            DateTime dtIni;
            while (!DateTime.TryParse(Console.ReadLine(), out dtIni))
            {
                Console.WriteLine("Data nao é valida\n");
            }
            i.dtInicio = dtIni;
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
            i.dtFim = dtfim;
            Console.WriteLine("Inserir valor da Intervencao");
            decimal valor;
            while (!decimal.TryParse(Console.ReadLine(), out valor))
                Console.WriteLine("Valor tem de ser decimal\n");
            i.valor = valor;
            Console.WriteLine("Inserir Ativo da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            i.ativoId = id;
            Console.WriteLine("Inserir meses da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            i.meses = id;
            servicesContext.insertIntervencaoWithProcedure(i);
        }
    }
}
