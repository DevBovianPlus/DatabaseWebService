//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseWebService.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusNadzora
    {
        public StatusNadzora()
        {
            this.Nadzor = new HashSet<Nadzor>();
        }
    
        public int idStatusNadzora { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
    
        public virtual ICollection<Nadzor> Nadzor { get; set; }
    }
}
