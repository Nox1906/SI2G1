using Model;
using System;
using System.Collections.Generic;
using EntityFrameworkModel;


namespace BusinessLayer
{
    public class EntityFrameworkServices : IServices

    {
        EntityFrameworkManager efm;
        public EntityFrameworkServices()
        {
            this.efm = new EntityFrameworkManager();
        }

        public Model.Equipa getEquipaLivre(string competencia)
        {
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
            try
            {
                efm.Create(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void insertEquipaIntervencao(Model.Intervencao intervencao, Model.Equipa equipa)
        {
            throw new NotImplementedException();
        }
    }
}
