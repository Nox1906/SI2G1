using DataLayer.DataMappers;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class EquipaMapper : Mapper<Equipa, int>
    {
        public EquipaMapper(ISession s) : base(s)
        {
        }

        protected string GetEquipaText
        {
            get
            {
                return "SELECT * FROM Equipa where id = @id";
            }
        }
        private string insertEquipaText
        {
            get
            {
                return "[dbo].[SP_criaEquipa]";
            }
        }

        public override void Create(Equipa entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Equipa entity)
        {
            throw new NotImplementedException();
        }


        public override Equipa ReadById(int id)
        {
            Equipa e = new Equipa();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = GetEquipaText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                e.Id = rd.GetInt32(0);
                                e.Localizacao = rd.GetString(1);
                                e.NFunc = rd.GetInt32(2);
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return e;
        }

        public override void Update(Equipa entity)
        {
            throw new NotImplementedException();
        }

        public Equipa getFreeTeam(string competencia)
        {
            int equipaId = 0;
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    cmd.CommandText = $"SELECT dbo.F_ObterEquipaLivre('{competencia}')";
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            equipaId = rd.GetInt32(0);
                        }
                    }
                    ts.Complete();
                }
            }
            return equipaId == 0 ? null : ReadById(equipaId);
        }

        public override void CreateWithSP(Equipa entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    cmd.CommandText = insertEquipaText;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter i1 = new SqlParameter("@idEquipa", entity.Id);
                    SqlParameter i2 = new SqlParameter("@localizacao", entity.Localizacao);
                    cmd.Parameters.Add(i1);
                    cmd.Parameters.Add(i2);
                    cmd.ExecuteNonQuery();
                    ts.Complete();
                }
            }
        }

    }
}
