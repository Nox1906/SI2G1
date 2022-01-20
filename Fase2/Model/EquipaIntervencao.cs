using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EquipaIntervencao
    {
        public Equipa Equipa { get; set; }
        public Intervencao Intervencao { get; set; }

        public override string ToString()
        {
            return $"EquipaIntervencao -> idIntervencao: {Intervencao.id} ; equipaId: {Equipa.Id} ";
        }
    }
}
