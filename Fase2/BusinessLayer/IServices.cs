using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IServices
    {
        Equipa getEquipaLivre(string competencia);
        void insertIntervencaoWithProcedure(Intervencao intervencao);
        void insertEquipa(Equipa equipa);
        void insertOrDeleteEquipaFunc(EquipaFunc equipaFunc, string option);
        List<Intervencao> getIntervencoesAno(int ano);
        void insertIntervencao(Intervencao intervencao);
        void insertEquipaIntervencao(Intervencao intervencao, Equipa equipa);
    }
}
