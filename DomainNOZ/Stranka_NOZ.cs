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
    
    public partial class Stranka_NOZ
    {
        public Stranka_NOZ()
        {
            this.StrankaZaposleni_NOZ = new HashSet<StrankaZaposleni_NOZ>();
            this.NarociloOptimalnihZalog = new HashSet<NarociloOptimalnihZalog>();
        }
    
        public int StrankaID { get; set; }
        public string KodaStranke { get; set; }
        public int TipStrankaID { get; set; }
        public string NazivPrvi { get; set; }
        public string NazivDrugi { get; set; }
        public string Naslov { get; set; }
        public string StevPoste { get; set; }
        public string NazivPoste { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string FAX { get; set; }
        public string InternetniNalov { get; set; }
        public string KontaktnaOseba { get; set; }
        public string RokPlacila { get; set; }
        public string TRR { get; set; }
        public string DavcnaStev { get; set; }
        public string MaticnaStev { get; set; }
        public Nullable<bool> StatusDomacTuji { get; set; }
        public Nullable<bool> Zavezanec_DA_NE { get; set; }
        public string IdentifikacijskaStev { get; set; }
        public Nullable<bool> Clan_EU { get; set; }
        public string BIC { get; set; }
        public string KodaPlacila { get; set; }
        public string DrzavaStranke { get; set; }
        public string Neaktivna { get; set; }
        public Nullable<int> GenerirajERacun { get; set; }
        public Nullable<int> JavniZavod { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public Nullable<System.DateTime> tsUpdate { get; set; }
        public Nullable<int> tsUpdateUserID { get; set; }
    
        public virtual ICollection<StrankaZaposleni_NOZ> StrankaZaposleni_NOZ { get; set; }
        public virtual TipStranka_NOZ TipStranka_NOZ { get; set; }
        public virtual ICollection<NarociloOptimalnihZalog> NarociloOptimalnihZalog { get; set; }
    }
}