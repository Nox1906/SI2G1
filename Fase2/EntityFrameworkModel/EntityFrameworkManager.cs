using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace EntityFrameworkModel
{
    public class EntityFrameworkManager
    {
        TransactionOptions options;
        TransactionScope ts;
        L51NG1Entities ctx;
        public EntityFrameworkManager()
        {
            options = new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(2)
            };
            ts = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public Model.Equipa getEquipaLivre(string competencia)
        {
            Equipa e;
            int r;
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    string sqlQuery = "SELECT [dbo].[F_ObterEquipaLivre] ({0})";
                    Object[] parameters = { competencia };

                    r = ctx.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                    Console.WriteLine();
                    e = (from i in ctx.Equipas
                         where i.id == r
                         select i).SingleOrDefault();
                }
                ts.Complete();
            }
            return e != null ? new Model.Equipa { Id = e.id, Localizacao = e.localizacao, NFunc = e.nFunc } : null;

        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_criaInter(i.id, i.descricao, i.dtInicio, i.dtFim, i.valor, i.ativoId, i.meses);
                }
                ts.Complete();
            }
        }
        public void insertEquipa(Model.Equipa e)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_criaEquipa(e.Id, e.Localizacao);
                }
                ts.Complete();
            }
        }

        public List<IntervencaoAno_Result> getIntervencoesAno(int ano)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    return ctx.IntervencaoAno(ano).ToList();
                }
            }
        }

        public void insertOrDeleteEquipaFunc(Model.EquipaFunc equipaFunc, string option)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_ActualizarElementosEquipa(equipaFunc.equipaId, equipaFunc.funcId, option, equipaFunc.supervisor);
                }
                ts.Complete();
            }
        }

        public void insertIntervencao(Model.Intervencao i)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    var intervencion = ctx.Set<Intervencao>();
                    intervencion.Add(new Intervencao
                    {
                        id = i.id,
                        descricao = i.descricao,
                        estado = i.estado,
                        dtInicio = i.dtInicio,
                        dtFim = i.dtFim,
                        valor = i.valor,
                        ativoId = i.ativoId,
                    });
                    if (i.meses > 0)
                    {
                        var periodic = ctx.Set<IntervencaoPeriodica>();
                        periodic.Add(new IntervencaoPeriodica
                        {
                            id = i.id,
                            meses = i.meses
                        }); ;
                    }
                    ctx.SaveChanges();
                }
                ts.Complete();
            }
        }
        public void insertEquipaIntervencao(Model.Intervencao intervencao, Model.Equipa equipa)
        {
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    var e = (from i in ctx.EquipaIntervencaos where i.idIntervencao == intervencao.id select i).SingleOrDefault();

                    e.equipaId = equipa.Id;

                    ctx.SP_AtualizarEstadoIntervencao(intervencao.id, intervencao.estado);
                }
                ts.Complete();
            }

        }
        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            bool fail;
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    do
                    {
                        fail = false;
                        try
                        {
                            var funcionario1 = ctx.Funcionarios.Where(f => f.id == idFunc1).FirstOrDefault();
                            var funcionario2 = ctx.Funcionarios.Where(f => f.id == idFunc2).FirstOrDefault();

                            var comptFunc1 = funcionario1.Competencias.Where(c => c.id == idCompFunc1).SingleOrDefault();
                            var comptFunc2 = funcionario2.Competencias.Where(c => c.id == idCompFunc2).SingleOrDefault();

                            funcionario1.Competencias.Remove(comptFunc1);
                            funcionario2.Competencias.Remove(comptFunc2);

                            funcionario1.Competencias.Add(comptFunc2);
                            funcionario2.Competencias.Add(comptFunc1);
                            ctx.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException e)
                        {

                            fail = true;
                            // esmagar as alterações na BD
                            var entry = e.Entries.Single();
                            var dbValues = entry.GetDatabaseValues();
                            entry.OriginalValues.SetValues(dbValues);
                        }
                    } while (fail);
                }
                ts.Complete();
            }
        }
    }
}

