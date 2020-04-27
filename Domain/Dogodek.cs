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
    
    public partial class Dogodek
    {
        public Dogodek()
        {
            this.Sporocila = new HashSet<Sporocila>();
            this.DogodekSestanek = new HashSet<DogodekSestanek>();
            this.Avtomatika = new HashSet<Avtomatika>();
        }
    
        public int idDogodek { get; set; }
        public Nullable<int> idStranka { get; set; }
        public Nullable<int> idKategorija { get; set; }
        public Nullable<int> Skrbnik { get; set; }
        public Nullable<int> Izvajalec { get; set; }
        public Nullable<int> idStatus { get; set; }
        public string Opis { get; set; }
        public Nullable<System.DateTime> DatumOtvoritve { get; set; }
        public Nullable<System.DateTime> Rok { get; set; }
        public string DatumZadZaprtja { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public string Tip { get; set; }
        public Nullable<System.DateTime> RokIzvedbe { get; set; }
    
        public virtual Kategorija Kategorija { get; set; }
        public virtual Osebe Osebe { get; set; }
        public virtual Osebe Osebe1 { get; set; }
        public virtual StatusDogodek StatusDogodek { get; set; }
        public virtual Stranka Stranka { get; set; }
        public virtual ICollection<Sporocila> Sporocila { get; set; }
        public virtual ICollection<DogodekSestanek> DogodekSestanek { get; set; }
        public virtual ICollection<Avtomatika> Avtomatika { get; set; }
    }
}
