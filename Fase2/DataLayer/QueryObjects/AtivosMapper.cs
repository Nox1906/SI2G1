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
    class AtivosMapper : Mapper<Ativo, int>
    {

        public AtivosMapper(ISession s) : base(s)
        {
        }

        private string GetAtivoText
        {
            get
            {
                return "SELECT * FROM Ativos where id = @id";
            }
        }

        public override void Create(Ativo entity)
        {
            throw new NotImplementedException();
        }

        public override void CreateWithSP(Ativo entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Ativo entity)
        {
            throw new NotImplementedException();
        }

        public override Ativo ReadById(int id)
        {
            Ativo a = new Ativo();
            openTransactionScope();
            using (ts)
            {
                using (SqlCommand cmd = session.CreateCommand())
                {
                    if (session.BeginTran())
                    {
                        cmd.CommandText = GetAtivoText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                a.id = rd.GetInt32(0);
                                a.nome = rd.GetString(1);
                                a.valor = rd.GetDecimal(2);
                                a.dtAquisicao = rd.GetDateTime(3);
                                a.estado = rd.GetBoolean(4);
                                a.marca = rd.GetString(5);
                                a.modelo = rd.GetString(6);
                                a.localizacao = rd.GetString(7);
                                a.parentId = rd.GetInt32(8);
                                a.tipoId = rd.GetInt32(9);
                                a.gestorId = rd.GetInt32(10);
                            }
                        }
                    }
                }
                ts.Complete();
            }
            return a;
        }

        public override void Update(Ativo entity)
        {
            throw new NotImplementedException();
        }
    }
}
