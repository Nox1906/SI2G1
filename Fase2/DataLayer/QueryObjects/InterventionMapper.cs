using DataLayer.DataMappers;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class InterventionMapper : Mapper, IMapper<Intervencao, int>
    {
        public InterventionMapper(ISession s) : base (s)
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
                return "SELECT i.id, i.descricao, i.estado, i.dtInicio, i.dtFim, i.valor, i.ativoId," +
                    " ip.meses  FROM Intervencao i inner join IntervencaoPeriodica ip on ip.id = i.id where i.id = @id";
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



        private string getClanByYearText
        {
            get
            {
                return "SELECT * from dbo.IntervencaoAno(@year)";
            }
        }
        public void Create(Intervencao entity)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (session.beginTran())
                    {
                        cmd.CommandText = InsertIntervencaoText;
                        cmd.Parameters.AddWithValue("@id", entity.id);
                        cmd.Parameters.AddWithValue("@descricao", entity.descricao);
                        cmd.Parameters.AddWithValue("@estado", entity.estado);
                        cmd.Parameters.AddWithValue("@dtInicio", entity.dtInicio);
                        cmd.Parameters.AddWithValue("@dtFim", entity.dtFim);
                        cmd.Parameters.AddWithValue("@valor", entity.valor);
                        cmd.Parameters.AddWithValue("@ativoId", entity.ativoId);
                        cmd.ExecuteNonQuery();
                        if (entity.meses > 0)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = InsertIntervencaoPeriodicaText;
                            cmd.Parameters.AddWithValue("@id", entity.id);
                            cmd.Parameters.AddWithValue("@meses", entity.meses);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    ts.Complete();
                }
            }
        }

        public void Delete(Intervencao entity)
        {
            throw new NotImplementedException();
        }

        public Intervencao ReadById(int id)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                Intervencao i = new Intervencao();
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (session.beginTran())
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
                                i.ativoId = rd.GetInt32(6);
                                i.meses = rd.GetInt32(7);
                            }
                        }
                    }
                    ts.Complete();
                }
                return i;
            }
        }

        public void Update(Intervencao entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateState(Intervencao entity)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                cmd.CommandText = updateIntervencionStateWithSPText;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter i1 = new SqlParameter("@intervencaoID", entity.id);
                SqlParameter i2 = new SqlParameter("@novoEstado", entity.estado);
                cmd.Parameters.Add(i1);
                cmd.Parameters.Add(i2);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void CreateWithSP(Intervencao entity)
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
                SqlParameter i6 = new SqlParameter("@ativoId", entity.ativoId);
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
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<Intervencao> GetInterventionsByYear(int year) 
        {
            List<Intervencao> interventions = new List<Intervencao>();
            using (SqlCommand cmd = session.CreateCommand()) 
            {
                cmd.CommandText = getClanByYearText;
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
            }
            return interventions;
        }
    }
}
