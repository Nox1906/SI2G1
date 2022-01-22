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
        }
        public void openTransactionScope()
        {
            ts = new TransactionScope(TransactionScopeOption.Required, options);
        }
        public Model.Equipa getEquipaLivre(string competencia)
        {
            Equipa e = null; ;
            int r;

            openTransactionScope();
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    string sqlQuery = "SELECT [dbo].[F_ObterEquipaLivre] ({0})";
                    Object[] parameters = { competencia };
                    r = ctx.Database.SqlQuery<int>(sqlQuery, parameters).FirstOrDefault();
                    e = ctx.Equipas.Where(eq => eq.id == r).FirstOrDefault();
                }
                ts.Complete();
            }
            return e != null ? new Model.Equipa { Id = e.id, Localizacao = e.localizacao, NFunc = e.nFunc } : null;
        }
        public void insertIntervencaoWithProcedure(Model.Intervencao i)
        {
            openTransactionScope();
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_criaInter(i.id, i.descricao, i.dtInicio, i.dtFim, i.valor, i.Ativo.id, i.meses);
                    ctx.SaveChanges();
                }
                ts.Complete();
            }
        }
        public void insertEquipa(Model.Equipa e)
        {
            openTransactionScope();
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
            openTransactionScope();
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
            openTransactionScope();
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    ctx.SP_ActualizarElementosEquipa(equipaFunc.Equipa.Id, equipaFunc.funcId, option, equipaFunc.supervisor);
                }
                ts.Complete();
            }
        }

        public void insertIntervencao(Model.Intervencao i)
        {
            openTransactionScope();
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
                        ativoId = i.Ativo.id,
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
            openTransactionScope();
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    var equipaInter = ctx.EquipaIntervencaos.Where(eq => eq.idIntervencao == intervencao.id).SingleOrDefault();
                    equipaInter.equipaId = equipa.Id;
                    ctx.SaveChanges();
                    ctx.SP_AtualizarEstadoIntervencao(intervencao.id, intervencao.estado);
                }
                ts.Complete();
            }
        }
        public void changeCompetenciaFunc(int idFunc1, int idFunc2, int idCompFunc1, int idCompFunc2)
        {
            openTransactionScope();
            ctx = new L51NG1Entities();
            L51NG1Entities ctx2 = new L51NG1Entities();
            using (ts)
            {

                var funcionario1 = ctx.Funcionarios.Where(f => f.id == idFunc1).SingleOrDefault();
                var comptFunc1 = funcionario1.Competencias.Where(c => c.id == idCompFunc1).SingleOrDefault();
                var funcionario2 = ctx.Funcionarios.Where(f => f.id == idFunc2).SingleOrDefault();
                var comptFunc2 = funcionario2.Competencias.Where(c => c.id == idCompFunc2).SingleOrDefault();

                var funcionario_1 = ctx2.Funcionarios.Where(f => f.id == idFunc1).SingleOrDefault();
                var comptFunc_1 = funcionario_1.Competencias.Where(c => c.id == idCompFunc1).SingleOrDefault();

                var funcionario_2 = ctx2.Funcionarios.Where(f => f.id == idFunc2).SingleOrDefault();
                var comptFunc_2 = funcionario_2.Competencias.Where(c => c.id == idCompFunc2).SingleOrDefault();

                funcionario_1.Competencias.Remove(comptFunc_1);
                funcionario_1.Competencias.Add(comptFunc_2);
                ctx2.SaveChanges();


                funcionario1.Competencias.Remove(comptFunc1);
                funcionario1.Competencias.Add(comptFunc2);
                funcionario2.Competencias.Remove(comptFunc2);
                funcionario2.Competencias.Add(comptFunc1);
                ctx.SaveChanges();
                ts.Complete();
            }
        }

        public void DeleteEquipaIntervencao(int id)
        {
            openTransactionScope();
            using (ts)
            {
                using (ctx = new L51NG1Entities())
                {
                    var equipInterv = ctx.EquipaIntervencaos.SingleOrDefault(x => x.idIntervencao == id);
                    if (equipInterv != null)
                    {
                        ctx.EquipaIntervencaos.Remove(equipInterv);
                        ctx.SaveChanges();
                    }
                }
                ts.Complete();
            }
        }
    }
}

