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
    
    public partial class Narocilo_PDO
    {
        public Narocilo_PDO()
        {
            this.NarociloPozicija_PDO = new HashSet<NarociloPozicija_PDO>();
        }
    
        public int NarociloID { get; set; }
        public string NarociloStevilka_P { get; set; }
        public Nullable<System.DateTime> DatumDobave { get; set; }
        public string Opombe { get; set; }
        public Nullable<System.DateTime> ts { get; set; }
        public Nullable<int> tsIDOsebe { get; set; }
        public Nullable<System.DateTime> tsUpdate { get; set; }
        public Nullable<int> tsUpdateUserID { get; set; }
        public Nullable<System.DateTime> P_CreateOrder { get; set; }
        public Nullable<int> P_UnsuccCountCreatePDFPantheon { get; set; }
        public Nullable<System.DateTime> P_LastTSCreatePDFPantheon { get; set; }
        public string P_TransportOrderPDFName { get; set; }
        public string P_TransportOrderPDFDocPath { get; set; }
        public Nullable<System.DateTime> P_GetPDFOrderFile { get; set; }
        public Nullable<int> P_SendWarningToAmin { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> DobaviteljID { get; set; }
        public Nullable<int> OddelekID { get; set; }
        public int PovprasevanjeID { get; set; }
        public string PovprasevanjeStevilka { get; set; }
    
        public virtual Stranka_PDO Stranka_PDO { get; set; }
        public virtual ICollection<NarociloPozicija_PDO> NarociloPozicija_PDO { get; set; }
        public virtual StatusPovprasevanja StatusPovprasevanja { get; set; }
        public virtual Oddelek Oddelek { get; set; }
    }
}
