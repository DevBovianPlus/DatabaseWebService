//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseWebService.DomainPDO
{
    using System;
    using System.Collections.Generic;
    
    public partial class OsebeNadrejeni_PDO
    {
        public int OsebeNadrejeniID { get; set; }
        public int OsebaID { get; set; }
        public int NadrejeniID { get; set; }
        public string Opomba { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDosebe { get; set; }
        public Nullable<System.DateTime> tsUpdate { get; set; }
        public Nullable<int> tsUpdateUserID { get; set; }
    
        public virtual Osebe_PDO Osebe_PDO { get; set; }
        public virtual Osebe_PDO Osebe_PDO1 { get; set; }
    }
}
