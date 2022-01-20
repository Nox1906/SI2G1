using DataLayer.DataMappers;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer.QueryObjects
{
    public class EquipaFuncMapper : Mapper<EquipaFunc, int>
    {
        public EquipaFuncMapper(ISession s) : base(s)
        {
        }

        private string insertorDeleteEquipaFuncText
        {
            get
            {
                return "[dbo].[SP_ActualizarElementosEquipa]";
            }
        }

        public override void Create(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(EquipaFunc id)
        {
            throw new NotImplementedException();
        }

        public override EquipaFunc ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }

        public void insertOrDeleteEquipaFunc(EquipaFunc entity, string option)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    cmd.CommandText = insertorDeleteEquipaFuncText;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter i1 = new SqlParameter("@equipaId", entity.Equipa.Id);
                    SqlParameter i2 = new SqlParameter("@FuncId", entity.funcId);
                    SqlParameter i3 = new SqlParameter("@operationType", option);
                    SqlParameter i4 = new SqlParameter("@supervisor", entity.supervisor);
                    cmd.Parameters.Add(i1);
                    cmd.Parameters.Add(i2);
                    cmd.Parameters.Add(i3);
                    cmd.Parameters.Add(i4);
                    cmd.ExecuteNonQuery();
                    ts.Complete();
                }
            }
        }

        public override void CreateWithSP(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }

    }
}
