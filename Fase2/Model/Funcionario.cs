using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Funcionario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int cc { get; set; }
        public int nif { get; set; }
        public System.DateTime dtNasc { get; set; }
        public string endereco { get; set; }
        public string email { get; set; }
        public string ntelefone { get; set; }
        public string profissao { get; set; }

    }
}
