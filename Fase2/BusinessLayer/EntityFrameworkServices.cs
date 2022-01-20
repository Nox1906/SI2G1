using Model;
using System;
using System.Collections.Generic;
using EntityFrameworkModel;
using System.Transactions;

namespace BusinessLayer
{
    public class EntityFrameworkServices : IServices
    {

        EntityFrameworkManager efm;
        public EntityFrameworkServices()
        {
            efm = new EntityFrameworkManager();
        }

        public Model.Equipa getEquipaLivre(string competencia)
        {
            Model.Equipa resultado = null;
            resultado = efm.getEquipaLivre(competencia);
            return resultado;
        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            efm.insertIntervencaoWithProcedure(i);
        }
        public void insertEquipa(Model.Equipa equipa)
        {

            efm.insertEquipa(equipa);

        }
        public void insertOrDeleteEquipaFunc(Model.EquipaFunc equipaFunc, string option)
        {

            efm.insertOrDeleteEquipaFunc(equipaFunc, option);

        }
        public List<Model.Intervencao> getIntervencoesAno(int ano)
        {
            List<Model.Intervencao> intervencoes = new List<Model.Intervencao>();
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
                        Ativo = null,
                        meses = 0
                    });
                }
            }
            return intervencoes;
        }
        public void insertIntervencao(Model.Intervencao intervencao)
        {

            efm.insertIntervencao(intervencao);

        }
        public void insertEquipaIntervencao(Model.Intervencao intervencao, Model.Equipa equipa)
        {

            efm.insertEquipaIntervencao(intervencao, equipa);

        }
        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {

            efm.changeCompetenciaFunc(idFunc1, idFunc2, idCompFunc1, idCompFunc2);

        }

        public void clearTest(int id)
        {

            efm.DeleteEquipaIntervencao(id);

        }
    }
}
