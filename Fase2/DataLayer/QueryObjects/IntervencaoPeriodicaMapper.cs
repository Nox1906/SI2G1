using DataLayer.DataMappers;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.QueryObjects
{
    public class IntervencaoPeriodicaMapper : Mapper<IntervencaoPeriodica, int>
    {
        public IntervencaoPeriodicaMapper(ISession s) : base(s)
        {

        }

        private string InsertIntervencaoPeriodicaText
        {
            get
            {
                return "INSERT INTO dbo.IntervencaoPeriodica values (@id, @meses)";
            }
        }
        private string getIntervencaoPeriodicaText
        {
            get
            {
                return "select * from dbo.IntervencaoPeriodica where id = @id ";
            }
        }

        public override void Create(IntervencaoPeriodica entity)
        {
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {

                    if (session.BeginTran())
                    {
                        cmd.CommandText = InsertIntervencaoPeriodicaText;
                        cmd.Parameters.AddWithValue("@id", entity.Intervencao.id);
                        cmd.Parameters.AddWithValue("@meses", entity.Intervencao.meses);
                        cmd.ExecuteNonQuery();
                    }
                    
                }
                ts.Complete();
            }
        }

        public override void CreateWithSP(IntervencaoPeriodica entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(IntervencaoPeriodica entity)
        {
            throw new NotImplementedException();
        }

        public override IntervencaoPeriodica ReadById(int id)
        {
            IntervencaoPeriodica i = new IntervencaoPeriodica();
            
            openTransactionScope();
            using (ts)
            {
                IMapper<Intervencao, int> intervencaoMapper = new IntervencaoMapper(session);
                i.Intervencao = intervencaoMapper.ReadById(id);
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = getIntervencaoPeriodicaText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                i.Intervencao.meses = rd.GetInt32(1);
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return i;
        }



        public override void Update(IntervencaoPeriodica entity)
        {
            throw new NotImplementedException();
        }
    }
}
