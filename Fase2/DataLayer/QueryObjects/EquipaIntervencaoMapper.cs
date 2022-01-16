using DataLayer.DataMappers;
using Model;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class EquipaIntervencaoMapper : Mapper, IMapper<EquipaIntervencao, int>
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
        public void Create(EquipaIntervencao entity)
        {
            throw new NotImplementedException();
        }

        public void CreateWithSP(EquipaIntervencao entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = deleteEquipaIntervencaoText;
                        cmd.Parameters.AddWithValue("@idIntervencao", id);
                        cmd.ExecuteNonQuery();
                    }
                    ts.Complete();
                }
            }
        }

        public EquipaIntervencao ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EquipaIntervencao entity)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = updateEquipaText;
                        cmd.Parameters.AddWithValue("@idIntervencao", entity.idIntervencao);
                        cmd.Parameters.AddWithValue("@equipaId", entity.equipaId);
                        cmd.ExecuteNonQuery();
                    }
                    ts.Complete();
                }
            }
        }
    }
}
