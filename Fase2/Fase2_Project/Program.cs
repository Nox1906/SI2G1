using DataLayer.QueryObjects;

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
        }
    }
}
