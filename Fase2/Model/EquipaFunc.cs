using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EquipaFunc : Models
    {
        public int funcId { get; set; }
        public Nullable<int> equipaId { get; set; }
        public Nullable<int> supervisor { get; set; }

        public override string ToString()
        {
            return $"EquipaFunc -> funcId: {funcId} ; equipaId: {equipaId} ; supervisor: {supervisor}";
        }

        public override void insertValues()
        {
            int id;
            Console.WriteLine("Inserir id do Funcionario");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.funcId = id;

            Console.WriteLine("Inserir id da Equipa");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.equipaId = id;

            Console.WriteLine("Inserir id do Supervisor");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.supervisor = id;
        }
    }
}
