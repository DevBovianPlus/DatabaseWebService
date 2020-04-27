﻿using DatabaseWebService.Common;
using DatabaseWebService.Common.Enums;
using DatabaseWebService.DomainOTP.Abstract;
using DatabaseWebService.ModelsOTP.Client;
using DatabaseWebService.ModelsOTP.Order;
using DatabaseWebService.ModelsOTP.Recall;
using DatabaseWebService.ModelsOTP.Tender;
using DatabaseWebService.Resources;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace DatabaseWebService.DomainOTP.Concrete
{
    public class MSSQLFunctionsRepository : IMSSQLFunctionsRepository
    {
        GrafolitOTPEntities context;

        public MSSQLFunctionsRepository(GrafolitOTPEntities _context)
        {
            context = _context;
        }

        public List<SupplierModel> GetListOfSupplier()
        {//TODO: pridobimo seznam tudi tistih dobaviteljev ki jih imamo v tabelo stranke_otp in so tipa SKLADISCE
            try
            {
                var query = from dob in context.SeznamDobaviteljev()
                            select new SupplierModel
                            {
                                Dobavitelj = dob.Dobavitelj,
                                Kraj = dob.Kraj,
                                Naslov = dob.Naslov,
                                Posta = dob.Posta
                            };

                //Seznam dobaviteljev, ki imajo v Tabeli Stranka_OTP polje TipStranke SKLADISCE
                string skladisceCode = Enums.TypeOfClient.SKLADISCE.ToString();
                var listOfClientsTypeSKLADISCE = from client in context.Stranka_OTP
                                                 where client.TipStranka.Koda == skladisceCode
                                                 select new SupplierModel
                                                 {
                                                     Dobavitelj = client.NazivPrvi,
                                                     Kraj = client.NazivPoste,
                                                     Naslov = client.Naslov,
                                                     Posta = client.StevPoste,
                                                     StrankaSkladisceID = client.idStranka
                                                 };

                List<SupplierModel> model = query.ToList();
                model.AddRange(listOfClientsTypeSKLADISCE.ToList());

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ValidationExceptionError.res_06, ex);
            }
        }

        public List<OrderPositionModelNew> GetListOfOpenedOrderPositions(string supplier, int clientID = 0)
        {//TODO: pridobimo še vse pozicije iz tabele lastna zaloga če je izbran dobavitelj iz naše tabele stranka_otp i da je tipa SKLADISCE
            try
            {
                supplier = supplier.Replace("|", "&");

                var query = from np in context.SeznamPozicijOdprtihNarocilnicGledeNaDobavitelja(supplier.Trim())
                            select new OrderPositionModelNew
                            {
                                Artikel = np.Artikel,
                                Datum_Dobave = np.Datum_Dobave,
                                Datum_narocila = np.Datum_narocila.HasValue ? np.Datum_narocila.Value : DateTime.MinValue,
                                Dobavitelj = np.Dobavitelj,
                                Kupec = np.Kupec,
                                Naroceno = np.Naroceno.HasValue ? np.Naroceno.Value : 0,
                                Narocilnica = np.Narocilnica,
                                Order_Confirm = np.Order_Confirm,
                                Prevzeto = np.Prevzeto.HasValue ? np.Prevzeto.Value : 0,
                                Razlika = np.Razlika.HasValue ? np.Razlika.Value : 0,
                                St_Pozicija = np.St_Pozicija,
                                Tovarna = np.Tovarna,
                                Tip = np.Tip,
                                Zaloga = np.Zaloga.HasValue ? np.Zaloga.Value : 0,
                                Interno = np.Interno,
                                Dovoljeno_Odpoklicati = np.Dovoljeno_odpoklicati,
                                Proizvedeno = np.Proizvedeno.HasValue ? np.Proizvedeno.Value : 0,
                                Ident = np.Ident,
                                Kupec_Kraj = np.Kupec_Kraj,
                                Kupec_Naslov = np.Kupec_Naslov,
                                Kupec_Posta = np.Kupec_Posta,
                                EnotaMere = np.Enota_Mere
                            };

                var pos = query.Where(o => o.Order_Confirm == "15-4988/30").FirstOrDefault();
                List<OrderPositionModelNew> list = query.ToList();
                int count = 1;
                foreach (var item in list)
                {
                    item.tempID = count;
                    count++;
                }

                string kodaPotrjen = Enums.StatusOfRecall.POTRJEN.ToString();
                int statusPotrjen = context.StatusOdpoklica.Where(so => so.Koda == kodaPotrjen).FirstOrDefault().StatusOdpoklicaID;

                string kodaDelnoPrevzet = Enums.StatusOfRecall.DELNO_PREVZET.ToString();
                int statusDelnoPrevzet = context.StatusOdpoklica.Where(so => so.Koda == kodaDelnoPrevzet).FirstOrDefault().StatusOdpoklicaID;

                CheckPositionQuantity(list, statusPotrjen, statusDelnoPrevzet);

                #region
                /*for (int i = list.Count - 1; i >= 0; i--)
                {
                    OrderPositionModelNew item = list[i];
                    //OdpoklicPozicija rPos = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident && op.Odpoklic.StatusID == statusPotrjen).OrderByDescending(op => op.DatumVnosa).FirstOrDefault();
                    List<OdpoklicPozicija> currentKolicinaOTPList = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident &&
                        !op.StatusPrevzeto.Value &&
                        (op.Odpoklic.StatusID == statusPotrjen || op.Odpoklic.StatusID == statusDelnoPrevzet)).ToList();

                    decimal? currentRecallQuantity = 0;
                    if (currentKolicinaOTPList.Count > 0)
                        currentRecallQuantity = currentKolicinaOTPList.Sum(op => op.Kolicina);

                    //pridobimo vse pozicije odpoklicev za posamezno pozicijo naročilnice
                    List<OdpoklicPozicija> posByOrderPosNum = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident &&
                        op.NarociloID == item.Narocilnica &&
                        op.NarociloPozicijaID == item.St_Pozicija &&
                        !op.StatusPrevzeto.Value &&
                        (op.Odpoklic.StatusID == statusPotrjen || op.Odpoklic.StatusID == statusDelnoPrevzet)).ToList();

                    //seštejemo odpoklicano količino za pozicijo naročilnice
                    item.OdpoklicKolicinaOTP = posByOrderPosNum.Sum(op => op.Kolicina);

                    if (item.OdpoklicKolicinaOTP == item.Naroceno)
                        list.RemoveAt(i);
                    else if (currentRecallQuantity.HasValue && item.Naroceno == (currentRecallQuantity + item.Zaloga))
                    {
                        item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);//rPos.KolicinaOTP.Value;
                    }
                    else if (currentRecallQuantity.HasValue && currentRecallQuantity.Value < item.Naroceno)
                    {
                        //list[i].Naroceno -= rPos.KolicinaOTP.HasValue ? rPos.KolicinaOTP.Value : 0;
                        //list[i].Razlika = list[i].Naroceno - list[i].Prevzeto;//TODO: Kaj če bo večja količina prevzeta kot naročena? (negativna razlika????)
                        item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);
                    }
                    else if (currentRecallQuantity.HasValue && currentRecallQuantity.Value > item.Naroceno)
                    {
                        item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);
                        //TODO: na večih pozicijah naročilnic potrebno odšteti in po potrebi le-to odstraniti iz seznama
                        /**decimal tempQuantity = rPos.KolicinaOTP.Value;
                        for (int j = list.Count - 1; j >= 0; j--)
                        {
                            OrderPositionModelNew obj = list[j];
                            if (obj.Ident == item.Ident)
                            {
                                if (tempQuantity > obj.Naroceno)
                                {
                                    tempQuantity -= obj.Naroceno;
                                    list.RemoveAt(j);
                                }
                                else if (tempQuantity == obj.Naroceno)
                                {
                                    tempQuantity -= obj.Naroceno;
                                    list.RemoveAt(j);
                                }
                                else if (tempQuantity < obj.Naroceno)
                                {
                                    obj.Naroceno -= tempQuantity;
                                    obj.Razlika = obj.Naroceno - obj.Prevzeto;//TODO: Kaj če bo večja količina prevzeta kot naročena? (negativna razlika????)
                                }
                            }
                        }
                    }
                }*/
                #endregion

                if (clientID > 0)
                {
                    var lastnoSkladisce = from ow in context.LastnaZaloga
                                          where ow.LastnoSkladisceID == clientID
                                          select new OrderPositionModelNew
                                          {
                                              Artikel = ow.Material,
                                              // Datum_Dobave = np.Datum_Dobave,
                                              //Datum_narocila = np.Datum_narocila.HasValue ? np.Datum_narocila.Value : DateTime.MinValue,
                                              //Dobavitelj = np.Dobavitelj,
                                              Kupec = ow.KupecNaziv,
                                              Naroceno = ow.KolicinaIzNarocila,
                                              Narocilnica = ow.NarociloID,
                                              Order_Confirm = ow.OC,
                                              Prevzeto = ow.KolicinaPrevzeta.HasValue ? ow.KolicinaPrevzeta.Value : 0,
                                              Razlika = ow.KolicinaRazlika.HasValue ? ow.KolicinaRazlika.Value : 0,
                                              St_Pozicija = ow.NarociloPozicijaID,
                                              //Tovarna = np.Tovarna,
                                              Tip = ow.TipNaziv,
                                              Zaloga = ow.TrenutnaZaloga.HasValue ? ow.TrenutnaZaloga.Value : 0,
                                              Interno = ow.Interno,
                                              Dovoljeno_Odpoklicati = ow.OptimalnaZaloga.HasValue ? (int)ow.OptimalnaZaloga.Value : 0,
                                              Proizvedeno = ow.Proizvedeno.HasValue ? ow.Proizvedeno.Value : 0,
                                              Ident = ow.MaterialIdent,
                                              Kupec_Kraj = ow.KupecKraj,
                                              Kupec_Naslov = ow.KupecNaslov,
                                              Kupec_Posta = ow.KupecPosta,
                                              OdpoklicID = ow.OdpoklicID,
                                              EnotaMere = ow.EnotaMere
                                          };

                    List<OrderPositionModelNew> newList = lastnoSkladisce.ToList();


                    foreach (var item in newList)
                    {
                        item.tempID = count;
                        count++;
                    }

                    CheckPositionQuantity(newList, statusPotrjen, statusDelnoPrevzet, true);

                    list.AddRange(newList);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ValidationExceptionError.res_06, ex);
            }
        }

        private List<OrderPositionModelNew> CheckPositionQuantity(List<OrderPositionModelNew> list, int statusPotrjen, int statusDelnoPrevzet, bool ownStockRecall = false)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                OrderPositionModelNew item = list[i];

                //OdpoklicPozicija rPos = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident && op.Odpoklic.StatusID == statusPotrjen).OrderByDescending(op => op.DatumVnosa).FirstOrDefault();
                List<OdpoklicPozicija> currentKolicinaOTPList = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident &&
                    !op.StatusPrevzeto.Value &&
                    (op.OdpoklicIzLastneZaloge.HasValue && op.OdpoklicIzLastneZaloge.Value == ownStockRecall) &&
                    (op.Odpoklic.StatusID == statusPotrjen || op.Odpoklic.StatusID == statusDelnoPrevzet)).ToList();

                decimal? currentRecallQuantity = 0;
                if (currentKolicinaOTPList.Count > 0)
                    currentRecallQuantity = currentKolicinaOTPList.Sum(op => op.Kolicina);

                //pridobimo vse pozicije odpoklicev za posamezno pozicijo naročilnice
                List<OdpoklicPozicija> posByOrderPosNum = context.OdpoklicPozicija.Where(op => op.MaterialIdent == item.Ident &&
                    op.NarociloID == item.Narocilnica &&
                    op.NarociloPozicijaID == item.St_Pozicija &&
                    !op.StatusPrevzeto.Value &&
                    op.OdpoklicIzLastneZaloge.Value == ownStockRecall &&
                    (op.Odpoklic.StatusID == statusPotrjen || op.Odpoklic.StatusID == statusDelnoPrevzet)).ToList();

                //seštejemo odpoklicano količino za pozicijo naročilnice
                item.OdpoklicKolicinaOTP = posByOrderPosNum.Sum(op => op.Kolicina);

                decimal primerjalnaKolicina = 0;

                if (item.Proizvedeno > item.Naroceno)
                    primerjalnaKolicina = item.Proizvedeno;
                else
                    primerjalnaKolicina = item.Naroceno;

                if (item.OdpoklicKolicinaOTP == primerjalnaKolicina)
                    list.RemoveAt(i);
                else if (currentRecallQuantity.HasValue && primerjalnaKolicina == (currentRecallQuantity + item.Zaloga))
                {
                    item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);//rPos.KolicinaOTP.Value;
                }
                else if (currentRecallQuantity.HasValue && currentRecallQuantity.Value < primerjalnaKolicina)
                {
                    //list[i].Naroceno -= rPos.KolicinaOTP.HasValue ? rPos.KolicinaOTP.Value : 0;
                    //list[i].Razlika = list[i].Naroceno - list[i].Prevzeto;//TODO: Kaj če bo večja količina prevzeta kot naročena? (negativna razlika????)
                    item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);
                }
                else if (currentRecallQuantity.HasValue && currentRecallQuantity.Value > primerjalnaKolicina)
                {
                    item.VsotaOdpoklicKolicinaOTP = (currentRecallQuantity.Value - item.OdpoklicKolicinaOTP);
                    //TODO: na večih pozicijah naročilnic potrebno odšteti in po potrebi le-to odstraniti iz seznama
                    /**decimal tempQuantity = rPos.KolicinaOTP.Value;
                    for (int j = list.Count - 1; j >= 0; j--)
                    {
                        OrderPositionModelNew obj = list[j];
                        if (obj.Ident == item.Ident)
                        {
                            if (tempQuantity > obj.Naroceno)
                            {
                                tempQuantity -= obj.Naroceno;
                                list.RemoveAt(j);
                            }
                            else if (tempQuantity == obj.Naroceno)
                            {
                                tempQuantity -= obj.Naroceno;
                                list.RemoveAt(j);
                            }
                            else if (tempQuantity < obj.Naroceno)
                            {
                                obj.Naroceno -= tempQuantity;
                                obj.Razlika = obj.Naroceno - obj.Prevzeto;//TODO: Kaj če bo večja količina prevzeta kot naročena? (negativna razlika????)
                            }
                        }
                    }*/
                }
            }

            return list;
        }

        public TransportCountModel GetTransportCounByTransporterAndRoute(TransportCountModel model)
        {
            try
            {

                var obj = context.GetLastYearRecallCountBySuplierAndRoute(model.RelacijaID, model.PrevoznikID).FirstOrDefault();

                if (obj != null)
                    return new TransportCountModel() { PrevoznikID = obj.DobaviteljID.Value, RelacijaID = obj.RelacijaID.Value, StPotrjenihOdpoklicev = obj.St_Relacija_Dobavitelj.Value, StVsehOdpoklicevZaRelacijo = obj.St_Relacija.Value };
                else
                    return new TransportCountModel();

            }
            catch (Exception ex)
            {
                throw new Exception(ValidationExceptionError.res_06, ex);
            }
        }

        public CreateOrderDocument GetOrderDocumentData(string OrderDocXML)
        {
            CreateOrderDocument _coData = new CreateOrderDocument();


            ObjectParameter opExportPath = new ObjectParameter("p_cExportPath", "");
            ObjectParameter opPDFFileName = new ObjectParameter("p_cKey", "");
            ObjectParameter opErrorDesc = new ObjectParameter("p_cError", "");



            var obj = context.DodajPantheonDokument(OrderDocXML, opExportPath, opPDFFileName, opErrorDesc);

            
            string sExportPath = Convert.ToString(opExportPath.Value);
            string sPDFFileName = Convert.ToString(opPDFFileName.Value);
            string sErrorDesc = Convert.ToString(opErrorDesc.Value);

            DataTypesHelper.LogThis("Številka naročilnice: " + sPDFFileName);

            if (sErrorDesc.Length>0) DataTypesHelper.LogThis("Error create naročilnica : " + sErrorDesc);
            
            if (sErrorDesc.Length > 0) throw new Exception(ValidationExceptionError.res_28 + "<br><br>" + sErrorDesc);

            _coData.ExportPath = sExportPath;
            _coData.PDFFile = sPDFFileName;
            _coData.ErrorDesc = sErrorDesc;


            return _coData;
        }


    }
}