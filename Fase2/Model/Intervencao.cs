using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Intervencao
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
        


        
            
    }
}
