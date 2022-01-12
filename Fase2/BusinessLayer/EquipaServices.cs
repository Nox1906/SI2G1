using DataLayer;
using DataLayer.DataMappers;
using DataLayer.QueryObjects;
using System;
using System.Reflection;

namespace BusinessLayer
{

    public class EquipaServices 
    {

        IEquipaMapper equipaMapper;
        Session session;
        public EquipaServices()
        {
            this.session = new Session();
            equipaMapper = new EquipaMapper(session);
        }

        public void showEquipaLivre()
        {
            Console.WriteLine("Inserir competencia : ");
            string competencia = Console.ReadLine();
            try
            {
                Object resultado = equipaMapper.GetEquipaLivre(competencia);
                showResults(resultado);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
      
        private void showResults(Object obj)
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
    }
}
