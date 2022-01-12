using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using Model;
using System;
using System.Linq;

namespace BusinessLayer
{
    public class IntervencaoServices
    {

        Session session;
        IIntervencaoMapper intervencaoMapper;

        public IntervencaoServices()
        {
            this.session = new Session();
            this.intervencaoMapper = new IntervencaoMapper(session);
        }

        

        public void insertIntervencaoWithProcedure(Intervencao i)
        {
            try
            {
                intervencaoMapper.Create(i);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
