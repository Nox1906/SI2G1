﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class L51NG1Entities : DbContext
    {
        public L51NG1Entities()
            : base("name=L51NG1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ativo> Ativos { get; set; }
        public virtual DbSet<Competencia> Competencias { get; set; }
        public virtual DbSet<Equipa> Equipas { get; set; }
        public virtual DbSet<EquipaFunc> EquipaFuncs { get; set; }
        public virtual DbSet<EquipaIntervencao> EquipaIntervencaos { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<HistAlteracaoEqInterv> HistAlteracaoEqIntervs { get; set; }
        public virtual DbSet<Historico> Historicoes { get; set; }
        public virtual DbSet<Intervencao> Intervencaos { get; set; }
        public virtual DbSet<IntervencaoPeriodica> IntervencaoPeriodicas { get; set; }
        public virtual DbSet<Tipos> Tipos { get; set; }
        public virtual DbSet<TV_ResumoIntervencoes> TV_ResumoIntervencoes { get; set; }
    
        [DbFunction("L51NG1Entities", "IntervencaoAno")]
        public virtual IQueryable<IntervencaoAno_Result> IntervencaoAno(Nullable<int> year)
        {
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<IntervencaoAno_Result>("[L51NG1Entities].[IntervencaoAno](@year)", yearParameter);
        }
    
        public virtual int SP_ActualizarElementosEquipa(Nullable<int> equipaId, Nullable<int> funcId, string operationType, Nullable<int> supervisor)
        {
            var equipaIdParameter = equipaId.HasValue ?
                new ObjectParameter("equipaId", equipaId) :
                new ObjectParameter("equipaId", typeof(int));
    
            var funcIdParameter = funcId.HasValue ?
                new ObjectParameter("FuncId", funcId) :
                new ObjectParameter("FuncId", typeof(int));
    
            var operationTypeParameter = operationType != null ?
                new ObjectParameter("operationType", operationType) :
                new ObjectParameter("operationType", typeof(string));
    
            var supervisorParameter = supervisor.HasValue ?
                new ObjectParameter("supervisor", supervisor) :
                new ObjectParameter("supervisor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_ActualizarElementosEquipa", equipaIdParameter, funcIdParameter, operationTypeParameter, supervisorParameter);
        }
    
        public virtual int SP_AtualizarEstadoIntervencao(Nullable<int> intervencaoID, string novoEstado)
        {
            var intervencaoIDParameter = intervencaoID.HasValue ?
                new ObjectParameter("intervencaoID", intervencaoID) :
                new ObjectParameter("intervencaoID", typeof(int));
    
            var novoEstadoParameter = novoEstado != null ?
                new ObjectParameter("novoEstado", novoEstado) :
                new ObjectParameter("novoEstado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AtualizarEstadoIntervencao", intervencaoIDParameter, novoEstadoParameter);
        }
    
        public virtual int SP_criaEquipa(Nullable<int> idEquipa, string localizacao)
        {
            var idEquipaParameter = idEquipa.HasValue ?
                new ObjectParameter("idEquipa", idEquipa) :
                new ObjectParameter("idEquipa", typeof(int));
    
            var localizacaoParameter = localizacao != null ?
                new ObjectParameter("localizacao", localizacao) :
                new ObjectParameter("localizacao", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_criaEquipa", idEquipaParameter, localizacaoParameter);
        }
    
        public virtual int SP_criaInter(Nullable<int> id, string descricao, Nullable<System.DateTime> dtInicio, Nullable<System.DateTime> dtFim, Nullable<decimal> valor, Nullable<int> ativoId, Nullable<int> meses)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var descricaoParameter = descricao != null ?
                new ObjectParameter("descricao", descricao) :
                new ObjectParameter("descricao", typeof(string));
    
            var dtInicioParameter = dtInicio.HasValue ?
                new ObjectParameter("dtInicio", dtInicio) :
                new ObjectParameter("dtInicio", typeof(System.DateTime));
    
            var dtFimParameter = dtFim.HasValue ?
                new ObjectParameter("dtFim", dtFim) :
                new ObjectParameter("dtFim", typeof(System.DateTime));
    
            var valorParameter = valor.HasValue ?
                new ObjectParameter("valor", valor) :
                new ObjectParameter("valor", typeof(decimal));
    
            var ativoIdParameter = ativoId.HasValue ?
                new ObjectParameter("ativoId", ativoId) :
                new ObjectParameter("ativoId", typeof(int));
    
            var mesesParameter = meses.HasValue ?
                new ObjectParameter("meses", meses) :
                new ObjectParameter("meses", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_criaInter", idParameter, descricaoParameter, dtInicioParameter, dtFimParameter, valorParameter, ativoIdParameter, mesesParameter);
        }
    
        public virtual int SP_Funcionarios(Nullable<int> id, string nome, Nullable<int> cc, Nullable<int> nif, Nullable<System.DateTime> dtNasc, string endereco, string email, string ntelefone, string profissao, string operationType)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var nomeParameter = nome != null ?
                new ObjectParameter("nome", nome) :
                new ObjectParameter("nome", typeof(string));
    
            var ccParameter = cc.HasValue ?
                new ObjectParameter("cc", cc) :
                new ObjectParameter("cc", typeof(int));
    
            var nifParameter = nif.HasValue ?
                new ObjectParameter("nif", nif) :
                new ObjectParameter("nif", typeof(int));
    
            var dtNascParameter = dtNasc.HasValue ?
                new ObjectParameter("dtNasc", dtNasc) :
                new ObjectParameter("dtNasc", typeof(System.DateTime));
    
            var enderecoParameter = endereco != null ?
                new ObjectParameter("endereco", endereco) :
                new ObjectParameter("endereco", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var ntelefoneParameter = ntelefone != null ?
                new ObjectParameter("ntelefone", ntelefone) :
                new ObjectParameter("ntelefone", typeof(string));
    
            var profissaoParameter = profissao != null ?
                new ObjectParameter("profissao", profissao) :
                new ObjectParameter("profissao", typeof(string));
    
            var operationTypeParameter = operationType != null ?
                new ObjectParameter("operationType", operationType) :
                new ObjectParameter("operationType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Funcionarios", idParameter, nomeParameter, ccParameter, nifParameter, dtNascParameter, enderecoParameter, emailParameter, ntelefoneParameter, profissaoParameter, operationTypeParameter);
        }
    }
}