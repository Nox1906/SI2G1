using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLayer
{
    public interface IServices
    {
        void showIntervencaoAno(int ano);
        void showEquipaLivre();
        void insertIntervencaoWithProcedure(Intervencao i);

    }
}
