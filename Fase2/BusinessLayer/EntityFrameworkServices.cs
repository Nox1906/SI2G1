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
            
        }

        public Model.Equipa getEquipaLivre(string competencia)
        {
            this.efm = new EntityFrameworkManager();
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
            this.efm = new EntityFrameworkManager();
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
            this.efm = new EntityFrameworkManager();
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
            this.efm = new EntityFrameworkManager();
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
            this.efm = new EntityFrameworkManager();
            try
            {
                efm.insertIntervencao(intervencao);
                Console.WriteLine("Intervenção inserida com sucesso \n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void insertEquipaIntervencao(Model.Intervencao intervencao, Model.Equipa equipa)
        {
            this.efm = new EntityFrameworkManager();
            try
            {
                efm.insertEquipaIntervencao(intervencao,equipa);
                Console.WriteLine("Equipa com id = "+ equipa.Id+" atribuida à intervenção com id = "+intervencao.id+"\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            this.efm = new EntityFrameworkManager();
            try
            {
                efm.changeCompetenciaFunc(idFunc1, idFunc2, idCompFunc1,idCompFunc2);
                Console.WriteLine("Competencias trocadas com sucesso entre funcionario "+ idFunc1+ " e "+idFunc2);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
