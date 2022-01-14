using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EquipaIntervencao
    {
        public int idIntervencao { get; set; }
        public Nullable<int> equipaId { get; set; }

        public override string ToString()
        {
            return $"EquipaIntervencao -> idIntervencao: {idIntervencao} ; equipaId: {equipaId} ";
        }
    }
}
