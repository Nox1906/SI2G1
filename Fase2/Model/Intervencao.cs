using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Intervencao : Models
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public decimal valor { get; set; }
        public int ativoId { get; set; }
        public int meses { get; set; }

        public override string ToString()
        {
            return $"Intervencao -> id: {id} ; descricao: {descricao} ; estado: {estado} ; " +
                $"dtInicio: {dtInicio} ; dtFim: {dtFim} ; valor: {valor} ; activoId: {ativoId} " +
                $"; meses: {meses}";
        }

        public string shortToString()
        {
            return $"Intervencao -> id: {id} ; descricao: {descricao} ";
        }
        public void insertDiferentState()
        {
            Console.WriteLine("Inserir opção de estado: concluído; em execução ; em análise ; por atribuir");
            while (true)
            {
                string[] values = { "concluído", "em execução", "em análise" , "por atribuir" };
                string result = Console.ReadLine();
                if (values.Any(result.Contains))
                {
                    this.estado = result;
                    break;
                }
                else
                    Console.WriteLine("estado nao é valido");
            }
        }


        public override void insertValues()
        {
            Console.WriteLine("Inserir id da Intervencao");
            int id;
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.id = id;
            Console.WriteLine("Inserir opção da descricao da Intervencao : inspecção; descricao ; avaria");
            while (true)
            {
                string[] values = { "inspecção", "descricao", "avaria" };
                string result = Console.ReadLine();
                if (values.Any(result.Contains))
                {
                    this.descricao = result;
                    break;
                }
                else
                    Console.WriteLine("descricao nao é valida");
            }
            this.estado = "por atribuir";

            Console.WriteLine("Inserir dtInicio da Intervencao no formato : YYYY-MM-DD");
            DateTime dtIni;
            while (!DateTime.TryParse(Console.ReadLine(), out dtIni))
            {
                Console.WriteLine("Data nao é valida\n");
            }
            this.dtInicio = dtIni;
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
            this.dtFim = dtfim;
            Console.WriteLine("Inserir valor da Intervencao");
            decimal valor;
            while (!decimal.TryParse(Console.ReadLine(), out valor))
                Console.WriteLine("Valor tem de ser decimal\n");
            this.valor = valor;
            Console.WriteLine("Inserir Ativo da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.ativoId = id;
            Console.WriteLine("Inserir meses da Intervencao");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.meses = id;
        }
    }
}
