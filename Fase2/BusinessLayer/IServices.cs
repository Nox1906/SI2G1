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
        void insertInterventionWithProcedure();
        void showFreeTeam();
        void insertTeam();
        void insertOrDeleteEquipaFunc();
        void showIntervencionsByYear();
        void insertIntervention();
        void putTeamInIntervencion();
    }
}
