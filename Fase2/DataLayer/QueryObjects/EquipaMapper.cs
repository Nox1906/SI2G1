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
    public class EquipaMapper : Mapper, IMapper<Equipa, int>
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

        public void Create(Equipa entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Equipa ReadById(int id)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                Equipa e = new Equipa();
                openTransactionScope();
                using (ts)
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
                    ts.Complete();
                }
                return e;
            }
        }

        public void Update(Equipa entity)
        {
            throw new NotImplementedException();
        }

        public Equipa getFreeTeam(string competencia)
        {
            int equipaId = 0;
            using (SqlCommand cmd = session.CreateCommand())
            {
                openTransactionScope();
                using (ts)
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

        public void CreateWithSP(Equipa entity)
        {

            using (SqlCommand cmd = session.CreateCommand())
            {
                openTransactionScope();
                using (ts)
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
