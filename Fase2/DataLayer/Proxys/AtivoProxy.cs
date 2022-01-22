using ModelInterfaces;
using System;
using System.Collections.Generic;

namespace DataLayer.Proxys
{
    public class AtivoProxy : IAtivo
    {

        private int _id;
        Func<int, IAtivo> _AtivoGetter = null;
        private IAtivo _ativo;

        public AtivoProxy(int id, Func<int, IAtivo> AtivoGetter)
        {
            _id = id;
            _AtivoGetter = AtivoGetter;
        }
        public int id { get => _id; set => _id = value; }
        public string nome { get => getAtivo().nome; set => getAtivo().nome = value; }
        public decimal? valor { get => getAtivo().valor; set => getAtivo().valor = value; }
        public DateTime dtAquisicao { get => getAtivo().dtAquisicao; set => getAtivo().dtAquisicao = value; }
        public bool? estado { get => getAtivo().estado; set => getAtivo().estado = value; }
        public string marca { get => getAtivo().marca; set => getAtivo().marca = value; }
        public string modelo { get => getAtivo().modelo; set => getAtivo().modelo = value; }
        public string localizacao { get => getAtivo().localizacao; set => getAtivo().localizacao = value; }
        public int? parentId { get => getAtivo().parentId; set => getAtivo().parentId = value; }
        public int tipoId { get => getAtivo().tipoId; set => getAtivo().tipoId = value; }
        public int gestorId { get => getAtivo().gestorId; set => getAtivo().gestorId = value; }
        //NOT IMPLEMENTED
        public List<IIntervencao> _intervencaos { get => null; set => _intervencaos = null; }

        private IAtivo getAtivo()
        {
            if (_ativo == null)
            {
                _ativo = _AtivoGetter(_id);
            }
            return _ativo;
        }
    }
}
