//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseWebService.DomainNOZ
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusNarocilaOptimalnihZalog
    {
        public StatusNarocilaOptimalnihZalog()
        {
            this.NarociloOptimalnihZalog = new HashSet<NarociloOptimalnihZalog>();
        }
    
        public int StatusNarocilaOptimalnihZalogID { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public Nullable<int> tsIDOseba { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<System.DateTime> tsUpdate { get; set; }
        public Nullable<int> tsUpdateUserID { get; set; }
    
        public virtual ICollection<NarociloOptimalnihZalog> NarociloOptimalnihZalog { get; set; }
    }
}