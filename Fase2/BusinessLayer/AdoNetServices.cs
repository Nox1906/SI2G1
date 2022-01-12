using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.QueryObjects;
using Model;

namespace BusinessLayer
{
    public class AdoNetServices : IServices
    {
        
        public AdoNetServices()
        {
           
        }

        public void showIntervencaoAno(int ano)
        {
            Console.WriteLine("using ado net getintervencaoAno");
        }

        public void showEquipaLivre()
        {
            EquipaServices eqs = new EquipaServices();
            eqs.showEquipaLivre();
        }

        public void insertIntervencaoWithProcedure(Intervencao i)
        {
            IntervencaoServices ins = new IntervencaoServices();
            ins.insertIntervencaoWithProcedure(i);

          
        }
    }
}
