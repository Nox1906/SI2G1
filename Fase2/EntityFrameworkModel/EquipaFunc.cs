//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class EquipaFunc
    {
        public int funcId { get; set; }
        public Nullable<int> equipaId { get; set; }
        public Nullable<int> supervisor { get; set; }
    
        public virtual Equipa Equipa { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Funcionario Funcionario1 { get; set; }
    }
}