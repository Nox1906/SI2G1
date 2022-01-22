using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Intervencao : IIntervencao
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
        public DateTime dtInicio { get; set; }
        public DateTime dtFim { get; set; }
        public decimal valor { get; set; }
        public IAtivo Ativo { get; set; }

        public int meses { get; set; }
        public EquipaIntervencao EquipaIntervencao { get; set; }
        public IntervencaoPeriodica IntervencaoPeriodica{ get; set; }

        public override string ToString()
        {
            return $"Intervencao -> id: {id} ; descricao: {descricao} ; estado: {estado} ; " +
                $"dtInicio: {dtInicio} ; dtFim: {dtFim} ; valor: {valor} ; activoId: {Ativo.id} " +
                $"; meses: {meses}";
        }

        public string shortToString()
        {
            return $"Intervencao -> id: {id} ; descricao: {descricao} ";
        }
        


        
            
    }
}
