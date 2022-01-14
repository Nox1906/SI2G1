using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkModel;

namespace BusinessLayer
{
    public class EntityFrameworkServices : IServices

    {
        EntityFrameworkManager efm;
        public EntityFrameworkServices()
        {

        }

        public Model.Equipa getEquipaLivre(string competencia)
        {
            efm = new EntityFrameworkManager();
            Model.Equipa resultado = null;
            try
            {
                resultado = efm.getEquipaLivre(competencia);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            efm = new EntityFrameworkManager();
            try
            {
                efm.insertIntervencaoWithProcedure(i);
                Console.WriteLine("Intervenção inserida com sucesso \n");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void insertEquipa(Model.Equipa equipa)
        {

            efm = new EntityFrameworkManager();
            try
            {
                efm.insertEquipa(equipa);
                Console.WriteLine("Equipa inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void insertOrDeleteEquipaFunc(Model.EquipaFunc equipaFunc, string option)
        {
            efm = new EntityFrameworkManager();
            try
            {
                efm.insertOrDeleteEquipaFunc(equipaFunc, option);
                Console.WriteLine("Operação realizada com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public List<Model.Intervencao> getIntervencoesAno(int ano)
        {
            List<Model.Intervencao> intervencoes = new List<Model.Intervencao>();
            efm = new EntityFrameworkManager();

            try
            {
                List<IntervencaoAno_Result> result = efm.getIntervencoesAno(ano);
                if (result != null)
                {
                    foreach (IntervencaoAno_Result e in result)
                    {
                        intervencoes.Add(new Model.Intervencao
                        {
                            id = e.id,
                            descricao = e.descricao,
                            estado = null,
                            dtInicio = new DateTime(),
                            dtFim = new DateTime(),
                            valor = 0,
                            ativoId = 0,
                            meses = 0
                        });
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return intervencoes;

        }








        public void insertIntervencao(Model.Intervencao intervencao)
        {
            throw new NotImplementedException();
        }

        public void insertEquipaIntervencao(Model.Intervencao intervencao)
        {
            throw new NotImplementedException();
        }
    }
}
