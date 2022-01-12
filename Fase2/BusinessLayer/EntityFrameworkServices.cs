using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using EntityFrameworkModel;
using Model;
namespace BusinessLayer
{

    public class EntityFrameworkServices : IServices

    {
        EntityFrameworkManager efm;
        public EntityFrameworkServices()
        {

        }
        //public Funcionario getFuncionario(int id)
        //{
        //    using (ts)
        //    {
        //        using (ctx = new L51NG1Entities())
        //        {
        //            Funcionario f = (from i in ctx.Funcionarios where i.id == id select i).SingleOrDefault();
        //            return f;
        //        }
        //    }
        //}
        public void showIntervencaoAno(int ano)
        {
            
            efm = new EntityFrameworkManager();
            List<IntervencaoAno_Result> intervencoes = efm.getIntervencaoAno(ano);
            foreach (IntervencaoAno_Result r in intervencoes)
            {
                Console.WriteLine(r.id);
                Console.WriteLine(r.descricao);
            }

        }

        public void showEquipaLivre()
        {
            Console.WriteLine("show equipa livre using EF");
        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            efm = new EntityFrameworkManager();
            efm.insertIntervencaoWithProcedure(i);
        }

    }
}
