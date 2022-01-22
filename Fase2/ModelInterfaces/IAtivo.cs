using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelInterfaces
{
    public interface IAtivo
    {
        int id { get; set; }
        string nome { get; set; }
        Nullable<decimal> valor { get; set; }
        System.DateTime dtAquisicao { get; set; }
        Nullable<bool> estado { get; set; }
        string marca { get; set; }
        string modelo { get; set; }
        string localizacao { get; set; }
        Nullable<int> parentId { get; set; }
        int tipoId { get; set; }
        int gestorId { get; set; }

        List<IIntervencao> _intervencaos { get; set; }
    }
}
