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
   public class EquipaMapper : IEquipaMapper
    {
        private ISession session;
        public EquipaMapper(ISession s)
        {
            this.session = s;
        }

        protected string GetEquipaText
        {
            get
            {
                return "SELECT id, localizacao, nfunc FROM Equipa where id = @id";
            }
        }

        public void Create(Equipa entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Equipa entity)
        {
            throw new NotImplementedException();
        }


        public Equipa ReadById(int id)
        {
            Equipa e = new Equipa();

            using (SqlCommand cmd = session.CreateCommand())
            {
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (session.beginTran())
                    {
                        cmd.CommandText = GetEquipaText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Equipa aux = new Equipa();
                                aux.Id = rd.GetInt32(0);
                                aux.Localizacao = rd.GetString(1);
                                aux.NFunc = rd.GetInt32(2);
                                e = aux;
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

        public Equipa GetEquipaLivre(String competencia)
        {
            int equipaId = 0;
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
            }
            return equipaId == 0 ? null : ReadById(equipaId);
        }
    }
}
