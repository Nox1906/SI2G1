using Model;
using System;
using DataLayer.DataMappers;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.QueryObjects
{
    public class FuncionarioMapper : IFuncionarioMapper
    {
        private ISession session;

        public FuncionarioMapper(ISession s)
        {
            this.session = s;
        }

        protected string GetFuncionarioText
        {
            get
            {
                return "SELECT id, nome, cc, nif, dtNasc, endereco, email, ntelefone, profissao FROM Funcionarios where id = @id";
            }
        }

        public void Create(Funcionario entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Funcionario entity)
        {
            throw new NotImplementedException();
        }

        public Funcionario ReadById(int id)
        {
            Funcionario f = new Funcionario();

            using (SqlCommand cmd = session.CreateCommand())
            {
                TransactionOptions topt = new TransactionOptions();
                topt.IsolationLevel = IsolationLevel.ReadCommitted;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required)) 
                {
                    if (session.beginTran())
                    {
                        cmd.CommandText = GetFuncionarioText;
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Funcionario aux = new Funcionario();
                                aux.id = rd.GetInt32(0);
                                aux.nome = rd.GetString(1);
                                aux.cc = rd.GetInt32(2);
                                aux.nif = rd.GetInt32(3);
                                aux.dtNasc = rd.GetDateTime(4);
                                aux.endereco = rd.GetString(5);
                                aux.email = rd.GetString(6);
                                aux.ntelefone = rd.GetString(7);
                                aux.profissao = rd.GetString(8);
                                f = aux;
                            }
                        }

                    }
                    ts.Complete();
                }
                return f;
            }
        }

            public void Update(Funcionario entity)
            {
                throw new NotImplementedException();
            }
        }
    }
