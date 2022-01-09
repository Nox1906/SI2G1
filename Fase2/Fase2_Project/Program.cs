using System;
using System.Data.Entity;
using DataLayer.QueryObjects;
using EntityFrameworkServices;

namespace Fase2_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Session session = new Session())
            {
                FuncionarioMapper pmapper = new FuncionarioMapper(session);
                Model.Funcionario f = pmapper.ReadById(1);
                session.closeCon(false);
            }
           
            var efm = new EntityFrameworkManager();
            Funcionario func = efm.getFuncionario(1);
            Console.WriteLine(func.nome);


            var intervencoes = efm.getIntervencaoAno(2021);
            foreach(var i in intervencoes)
            {
                Console.WriteLine(i.id);
                Console.WriteLine(i.descricao);
            }
        }
    }
}
