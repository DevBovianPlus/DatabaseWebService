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
    
    public partial class TipStranka
    {
        public TipStranka()
        {
            this.Stranka_OTP = new HashSet<Stranka_OTP>();
        }
    
        public int TipStrankaID { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public Nullable<int> tsIDOseba { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
    
        public virtual ICollection<Stranka_OTP> Stranka_OTP { get; set; }
    }
}