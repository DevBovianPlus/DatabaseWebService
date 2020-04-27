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
    
    public partial class Osebe_OTP
    {
        public Osebe_OTP()
        {
            this.StrankaZaposleni_OTP = new HashSet<StrankaZaposleni_OTP>();
            this.OsebeNadrejeni_OTP = new HashSet<OsebeNadrejeni_OTP>();
            this.OsebeNadrejeni_OTP1 = new HashSet<OsebeNadrejeni_OTP>();
            this.Odpoklic = new HashSet<Odpoklic>();
        }
    
        public int idOsebe { get; set; }
        public Nullable<int> idVloga { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Naslov { get; set; }
        public Nullable<System.DateTime> DatumRojstva { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DatumZaposlitve { get; set; }
        public string UporabniskoIme { get; set; }
        public string Geslo { get; set; }
        public string TelefonGSM { get; set; }
        public Nullable<int> Zunanji { get; set; }
        public string DelovnoMesto { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public Nullable<int> activity { get; set; }
    
        public virtual Vloga_OTP Vloga_OTP { get; set; }
        public virtual ICollection<StrankaZaposleni_OTP> StrankaZaposleni_OTP { get; set; }
        public virtual ICollection<OsebeNadrejeni_OTP> OsebeNadrejeni_OTP { get; set; }
        public virtual ICollection<OsebeNadrejeni_OTP> OsebeNadrejeni_OTP1 { get; set; }
        public virtual ICollection<Odpoklic> Odpoklic { get; set; }
    }
}
