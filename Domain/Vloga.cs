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
    
    public partial class Vloga
    {
        public Vloga()
        {
            this.Osebe = new HashSet<Osebe>();
        }
    
        public int idVloga { get; set; }
        public string Koda { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
    
        public virtual ICollection<Osebe> Osebe { get; set; }
    }
}
