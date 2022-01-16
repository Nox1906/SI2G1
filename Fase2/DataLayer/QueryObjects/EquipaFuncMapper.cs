using DataLayer.DataMappers;
using Model;
using System;
using System.Data.SqlClient;

namespace DataLayer.QueryObjects
{
    public class EquipaFuncMapper : Mapper, IMapper<EquipaFunc, int>
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

        public void Create(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EquipaFunc ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }

        public void insertOrDeleteEquipaFunc(EquipaFunc entity, string option)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                cmd.CommandText = insertorDeleteEquipaFuncText;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter i1 = new SqlParameter("@equipaId", entity.equipaId);
                SqlParameter i2 = new SqlParameter("@FuncId", entity.funcId);
                SqlParameter i3 = new SqlParameter("@operationType", option);
                SqlParameter i4 = new SqlParameter("@supervisor", entity.supervisor);
                cmd.Parameters.Add(i1);
                cmd.Parameters.Add(i2);
                cmd.Parameters.Add(i3);
                cmd.Parameters.Add(i4);

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

        public void CreateWithSP(EquipaFunc entity)
        {
            throw new NotImplementedException();
        }
    }
}
