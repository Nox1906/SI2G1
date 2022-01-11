using DataLayer.DataMappers;
using Model;
using System;
using System.Data.SqlClient;

namespace DataLayer.QueryObjects
{
    public class IntervencaoMapper : IIntervencaoMapper
    {
        private ISession session;

        public IntervencaoMapper(ISession s)
        {
            this.session = s;
        }

        private string insertIntervencaoText
        {
            get
            {
                return "[dbo].[SP_criaInter]";
            }
        }
        public void Create(Intervencao entity)
        {
            using (SqlCommand cmd = session.CreateCommand())
            {
                cmd.CommandText = insertIntervencaoText;
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

        public void Delete(Intervencao entity)
        {
            throw new NotImplementedException();
        }

        public Intervencao ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Intervencao entity)
        {
            throw new NotImplementedException();
        }
    }
}
