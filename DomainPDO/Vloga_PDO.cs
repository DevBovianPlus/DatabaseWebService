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
    
    public partial class Vloga_PDO
    {
        public Vloga_PDO()
        {
            this.Osebe_PDO = new HashSet<Osebe_PDO>();
        }
    
        public int VlogaID { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public Nullable<System.DateTime> tsUpdate { get; set; }
        public Nullable<int> tsUpdateUserID { get; set; }
    
        public virtual ICollection<Osebe_PDO> Osebe_PDO { get; set; }
    }
}
