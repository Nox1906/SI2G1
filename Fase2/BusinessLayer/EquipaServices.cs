#define ADONET
//#define EF

using DataLayer;
using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using System;
using System.Reflection;

namespace BusinessLayer
{

    public class EquipaServices
    {
#if ADONET

        Session session;
        IEquipaMapper equipaMapper;

        public EquipaServices()
        {
            this.session = new Session();
            equipaMapper = new EquipaMapper(session);
        }

        public void showResults(Object obj)
        {
            Type t = obj.GetType();
            Console.WriteLine("Type is: {0}", t.Name);
            PropertyInfo[] props = t.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine("   {0} : {1}", prop.Name,
                  prop.GetValue(obj));
            }
        }

        public void showEquipaLivre()
        {
            Console.WriteLine("Inserir competencia : \n");
            string competencia = Console.ReadLine();
            Object resultado = ((EquipaMapper)equipaMapper).GetEquipaLivre(competencia);
            showResults(resultado);
            this.session.closeCon();
        }

#endif
#if EF
#endif
    }
}
