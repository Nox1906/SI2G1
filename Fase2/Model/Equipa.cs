using System;
using System.Collections.Generic;

namespace Model
{
    public class Equipa
    {

        public Equipa()
        {
            EquipaIntervencaos = new List<EquipaIntervencao>();
        }
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public Nullable<int> NFunc { get; set; }

        public List<EquipaIntervencao> EquipaIntervencaos { get; set; }
        public List<EquipaFunc> EquipaFuncs { get; set; }


        public override string ToString()
        {
            return $"Equipa -> Id: {Id} ; Localizacao: {Localizacao} ; NFunc: {NFunc}";
        }

       
    }
}
