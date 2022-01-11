using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using Model;
using System;
using System.Linq;

namespace BusinessLayer
{
    public class IntervencaoServices
    {

        Session session;
        IIntervencaoMapper intervencaoMapper;

        public IntervencaoServices()
        {
            this.session = new Session();
            this.intervencaoMapper = new IntervencaoMapper(session);
        }

        private Intervencao createIntervencao()
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
            return i;
        }

        public void insertIntervencaoWithProcedure()
        {
            try
            {
                intervencaoMapper.Create(createIntervencao());
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void insertIntervencaoWithProcedureEF()
        {
            throw new NotImplementedException();
        }
    }
}
