using DataLayer.DataMappers;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class EquipaIntervencaoMapper : Mapper<EquipaIntervencao, int>
    {
        public EquipaIntervencaoMapper(ISession s) : base(s)
        {
        }

        private string updateEquipaText
        {
            get
            {
                return "update EquipaIntervencao set equipaId = @equipaId where idIntervencao = @idIntervencao";
            }
        }

        private string deleteEquipaIntervencaoText
        {
            get
            {
                return "delete EquipaIntervencao where idIntervencao = @idIntervencao";
            }
        }

        private string getAllbyIdEquipaIntervencaoText
        {
            get
            {
                return "SELECT * FROM EquipaIntervencao where equipaId = @equipaId";
            }
        }
        public override void Create(EquipaIntervencao entity)
        {
            throw new NotImplementedException();
        }

        public override void CreateWithSP(EquipaIntervencao entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(EquipaIntervencao entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    if (session.BeginTran())
                    {
                        cmd.CommandText = deleteEquipaIntervencaoText;
                        cmd.Parameters.AddWithValue("@idIntervencao", entity.Intervencao.id);
                        cmd.ExecuteNonQuery();
                    }
                }
                ts.Complete();
            }
        }

        public List<EquipaIntervencao> ReadAllEquipas(object entity)
        {
            List<EquipaIntervencao> equipaIntervencaos = new List<EquipaIntervencao>();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = getAllbyIdEquipaIntervencaoText;
                        cmd.Parameters.AddWithValue("@equipaId", entity);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                EquipaIntervencao e = new EquipaIntervencao
                                {
                                    Intervencao = new Intervencao { id = rd.GetInt32(0) },
                                    Equipa = new Equipa { Id = rd.GetInt32(1) },
                                };
                                equipaIntervencaos.Add(e);
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return equipaIntervencaos;
        }

        public override EquipaIntervencao ReadById(int id)
        {
            EquipaIntervencao ei = new EquipaIntervencao();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = "Select * from EquipaIntervencao where @id = idIntervencao";
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                ei.Intervencao = new Intervencao { id = rd.GetInt32(0) };
                                ei.Equipa = new Equipa { Id = rd.GetInt32(1) };
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return ei;
        }

        public override void Update(EquipaIntervencao entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = updateEquipaText;
                        cmd.Parameters.AddWithValue("@idIntervencao", entity.Intervencao.id);
                        cmd.Parameters.AddWithValue("@equipaId", entity.Equipa.Id);
                        cmd.ExecuteNonQuery();
                    }
                    ts.Complete();
                }
            }
        }
    }
}
