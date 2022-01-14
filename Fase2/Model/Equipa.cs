using System;


namespace Model
{
    public class Equipa
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public Nullable<int> NFunc { get; set; }

        public override string ToString()
        {
            return $"Equipa -> Id: {Id} ; Localizacao: {Localizacao} ; NFunc: {NFunc}";
        }

       
    }
}
