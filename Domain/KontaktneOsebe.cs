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
    
    public partial class KontaktneOsebe
    {
        public int idKontaktneOsebe { get; set; }
        public Nullable<int> idStranka { get; set; }
        public string NazivKontaktneOsebe { get; set; }
        public string Telefon { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
        public string DelovnoMesto { get; set; }
        public Nullable<int> ZaporednaStevika { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public string Fax { get; set; }
        public string Opombe { get; set; }
    
        public virtual Stranka Stranka { get; set; }
    }
}
