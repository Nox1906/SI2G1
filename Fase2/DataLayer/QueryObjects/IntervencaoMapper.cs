using DataLayer.DataMappers;
using DataLayer.Proxys;
using Model;
using ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer.QueryObjects
{
    public class IntervencaoMapper : Mapper<Intervencao, int>
    {
        public IntervencaoMapper(ISession s) : base(s)
        {
        }

        private string insertIntervencaoWithSPText
        {
            get
            {
                return "[dbo].[SP_criaInter]";
            }
        }

        private string updateIntervencionStateWithSPText
        {
            get
            {
                return "[DBO].[SP_AtualizarEstadoIntervencao]";
            }
        }

        private string GetIntervencionText
        {
            get
            {
                return "SELECT * FROM INTERVENCAO where id = @id";
            }
        }

        private string InsertIntervencaoText
        {
            get
            {
                return "INSERT INTO dbo.Intervencao VALUES(@id,@descricao,@estado,@dtInicio,@dtFim,@valor,@ativoId)";
            }
        }

        private string InsertIntervencaoPeriodicaText
        {
            get
            {
                return "INSERT INTO dbo.IntervencaoPeriodica values (@id, @meses)";
            }
        }



        private string getInterventionByYearText
        {
            get
            {
                return "SELECT * from dbo.IntervencaoAno(@year)";
            }
        }
        public override void Create(Intervencao entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    if (session.BeginTran())
                    {
                        cmd.CommandText = InsertIntervencaoText;
                        cmd.Parameters.AddWithValue("@id", entity.id);
                        cmd.Parameters.AddWithValue("@descricao", entity.descricao);
                        cmd.Parameters.AddWithValue("@estado", entity.estado);
                        cmd.Parameters.AddWithValue("@dtInicio", entity.dtInicio);
                        cmd.Parameters.AddWithValue("@dtFim", entity.dtFim);
                        cmd.Parameters.AddWithValue("@valor", entity.valor);
                        cmd.Parameters.AddWithValue("@ativoId", entity.Ativo.id);
                        cmd.ExecuteNonQuery();
                        if (entity.meses > 0)
                        {
                            IntervencaoPeriodicaMapper intervencaoPeriodicaMapper = new IntervencaoPeriodicaMapper(session);
                            IntervencaoPeriodica intervencaoPeriodica = new IntervencaoPeriodica { Intervencao = entity };
                            intervencaoPeriodicaMapper.Create(intervencaoPeriodica);
                        }
                    }
                    ts.Complete();
                }
            }
        }

        public override void Delete(Intervencao entity)
        {
            throw new NotImplementedException();
        }

        public override Intervencao ReadById(int id)
        {
            Intervencao i = new Intervencao();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = GetIntervencionText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                i.id = rd.GetInt32(0);
                                i.descricao = rd.GetString(1);
                                i.estado = rd.GetString(2);
                                i.dtInicio = rd.GetDateTime(3);
                                i.dtFim = rd.GetDateTime(4);
                                i.valor = rd.GetDecimal(5);
                                i.Ativo = new AtivoProxy (rd.GetInt32(6), ativoGetter);
                                i.meses = 0;
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return i;
        }

        public override void Update(Intervencao entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateState(Intervencao entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    cmd.CommandText = updateIntervencionStateWithSPText;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter i1 = new SqlParameter("@intervencaoID", entity.id);
                    SqlParameter i2 = new SqlParameter("@novoEstado", entity.estado);
                    cmd.Parameters.Add(i1);
                    cmd.Parameters.Add(i2);
                    cmd.ExecuteNonQuery();
                    ts.Complete();
                }
            }
        }

        public override void CreateWithSP(Intervencao entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    cmd.CommandText = insertIntervencaoWithSPText;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter i1 = new SqlParameter("@id", entity.id);
                    SqlParameter i2 = new SqlParameter("@descricao", entity.descricao);
                    SqlParameter i3 = new SqlParameter("@dtInicio", entity.dtInicio);
                    SqlParameter i4 = new SqlParameter("@dtFim", entity.dtFim);
                    SqlParameter i5 = new SqlParameter("@valor", entity.valor);
                    SqlParameter i6 = new SqlParameter("@ativoId", entity.Ativo.id);
                    SqlParameter i7 = new SqlParameter("@meses", entity.meses);
                    i3.SqlDbType = System.Data.SqlDbType.Date;
                    i4.SqlDbType = System.Data.SqlDbType.Date;
                    cmd.Parameters.Add(i1);
                    cmd.Parameters.Add(i2);
                    cmd.Parameters.Add(i3);
                    cmd.Parameters.Add(i4);
                    cmd.Parameters.Add(i5);
                    cmd.Parameters.Add(i6);
                    cmd.Parameters.Add(i7);
                    cmd.ExecuteNonQuery();
                    ts.Complete();
                }
            }
        }

        public List<Intervencao> getIntervencoesAno(int year)
        {
            List<Intervencao> interventions = new List<Intervencao>();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    cmd.CommandText = getInterventionByYearText;
                    cmd.Parameters.AddWithValue("@year", year);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Intervencao i = new Intervencao
                            {
                                id = rd.GetInt32(0),
                                descricao = rd.GetString(1),
                            };
                            interventions.Add(i);
                        }
                    }
                    ts.Complete();
                }
            }
            return interventions;
        }

        private IAtivo ativoGetter(int id)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Ativos where @id = id";
                cmd.Parameters.AddWithValue("@id", id);
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        IAtivo ativo = new Ativo()
                        {
                            id = rd.GetInt32(0),
                            nome = rd.GetString(1),
                            valor = rd.GetDecimal(2),
                            dtAquisicao = rd.GetDateTime(3),
                            estado = rd.GetBoolean(4),
                            marca = rd.GetString(5),
                            modelo = rd.GetString(6),
                            localizacao = rd.GetString(7),
                            parentId = rd.GetInt32(8),
                            tipoId = rd.GetInt32(9),
                            gestorId = rd.GetInt32(10),
                        };
                        return ativo;
                    }
                }
            }
            return null;
        }
    }
}
