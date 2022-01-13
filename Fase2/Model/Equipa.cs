using System;


namespace Model
{
    public class Equipa : Models
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        public Nullable<int> NFunc { get; set; }

        public override string ToString()
        {
            return $"Equipa -> Id: {Id} ; Localizacao: {Localizacao} ; NFunc: {NFunc}";
        }

        public override void insertValues()
        {
            int id;
            Console.WriteLine("Inserir id da Equipa");
            while (!Int32.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Valor tem de ser inteiro\n");
            this.Id = id;
            Console.WriteLine("Inserir localização");
            this.Localizacao = Console.ReadLine();
        }
    }
}
