//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseWebService.DomainOTP
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZbirnikTon
    {
        public ZbirnikTon()
        {
            this.Odpoklic = new HashSet<Odpoklic>();
            this.RazpisPozicija = new HashSet<RazpisPozicija>();
        }
    
        public int ZbirnikTonID { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<decimal> OdTeza { get; set; }
        public Nullable<decimal> DoTeza { get; set; }
        public Nullable<decimal> SortIdx { get; set; }
    
        public virtual ICollection<Odpoklic> Odpoklic { get; set; }
        public virtual ICollection<RazpisPozicija> RazpisPozicija { get; set; }
    }
}
