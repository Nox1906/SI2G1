using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ativo :IAtivo 
    {
        public Ativo()
        {
        
        }
        public int id { get; set; }
        public string nome { get; set; }
        public Nullable<decimal> valor { get; set; }
        public System.DateTime dtAquisicao { get; set; }
        public Nullable<bool> estado { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string localizacao { get; set; }
        public Nullable<int> parentId { get; set; }
        public int tipoId { get; set; }
        public int gestorId { get; set; }
        public List<IIntervencao> _intervencaos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
