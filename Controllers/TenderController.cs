﻿using DatabaseWebService.Common;
using DatabaseWebService.DomainOTP.Abstract;
using DatabaseWebService.Models;
using DatabaseWebService.ModelsOTP.Tender;
using DatabaseWebService.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DatabaseWebService.Controllers
{
    public class TenderController : ApiController
    {
        private ITenderRepository tenderRepo;
        private IMSSQLFunctionsRepository sqlFunctionRepo;

        public delegate WebResponseContentModel<T> Del<T>(WebResponseContentModel<T> model, Exception ex = null);
        public TenderController(ITenderRepository itenderRepo, IMSSQLFunctionsRepository isqlFunctionRepo)
        {
            tenderRepo = itenderRepo;
            sqlFunctionRepo = isqlFunctionRepo;
        }

        public WebResponseContentModel<T> ProcessContentModel<T>(WebResponseContentModel<T> model, Exception ex = null)
        {
            if (ex == null)
            {
                if (model.Content != null)
                {
                    model.IsRequestSuccesful = true;
                    model.ValidationError = "";
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_03;
                }
            }
            else
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
            }
            return model;
        }

        [HttpGet]
        public IHttpActionResult GetTenderList()
        {
            WebResponseContentModel<List<TenderFullModel>> tmpUser = new WebResponseContentModel<List<TenderFullModel>>();
            Del<List<TenderFullModel>> responseStatusHandler = ProcessContentModel;
            try
            {
                tmpUser.Content = tenderRepo.GetTenderList();
                responseStatusHandler(tmpUser);
            }
            catch (Exception ex)
            {
                responseStatusHandler(tmpUser, ex);
                return Json(tmpUser);
            }

            return Json(tmpUser);
        }

        [HttpGet]
        public IHttpActionResult GetTenderByID(int tenderID)
        {
            WebResponseContentModel<TenderFullModel> tmpUser = new WebResponseContentModel<TenderFullModel>();
            Del<TenderFullModel> responseStatusHandler = ProcessContentModel;
            try
            {
                tmpUser.Content = tenderRepo.GetTenderModelByID(tenderID);
                responseStatusHandler(tmpUser);
            }
            catch (Exception ex)
            {
                responseStatusHandler(tmpUser, ex);
                return Json(tmpUser);
            }

            return Json(tmpUser);
        }

        [HttpPost]
        public IHttpActionResult SaveTender([FromBody]object tenderData)
        {
            WebResponseContentModel<TenderFullModel> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<TenderFullModel>>(tenderData.ToString());

                if (model.Content != null)
                {
                    if (model.Content.RazpisID > 0)//We update existing record in DB
                        tenderRepo.SaveTender(model.Content);
                    else // We add and save new recod to DB 
                        model.Content.RazpisID = tenderRepo.SaveTender(model.Content, false);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }



        [HttpGet]
        public IHttpActionResult DeleteTender(int tenderID)
        {
            WebResponseContentModel<bool> deleteTender = new WebResponseContentModel<bool>();
            try
            {
                deleteTender.Content = tenderRepo.DeleteTender(tenderID);

                if (deleteTender.Content)
                    deleteTender.IsRequestSuccesful = true;
                else
                {
                    deleteTender.IsRequestSuccesful = false;
                    deleteTender.ValidationError = ValidationExceptionError.res_04;
                }
            }
            catch (Exception ex)
            {
                deleteTender.IsRequestSuccesful = false;
                deleteTender.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(deleteTender);
            }

            return Json(deleteTender);
        }

        [HttpGet]
        public IHttpActionResult GetTenderListByRouteID(int routeID)
        {
            WebResponseContentModel<List<TenderPositionModel>> tmpUser = new WebResponseContentModel<List<TenderPositionModel>>();
            Del<List<TenderPositionModel>> responseStatusHandler = ProcessContentModel;
            try
            {
                tmpUser.Content = tenderRepo.GetTenderListByRouteID(routeID);
                responseStatusHandler(tmpUser);
            }
            catch (Exception ex)
            {
                responseStatusHandler(tmpUser, ex);
                return Json(tmpUser);
            }

            return Json(tmpUser);
        }

        [HttpPost]
        public IHttpActionResult SaveTenders([FromBody]object tendersData)
        {
            WebResponseContentModel<List<TenderFullModel>> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<List<TenderFullModel>>>(tendersData.ToString());

                if (model.Content != null)
                {
                    tenderRepo.SaveTenders(model.Content);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpPost]
        public IHttpActionResult SaveTenderPosition([FromBody]object tenderPositionData)
        {
            WebResponseContentModel<TenderPositionModel> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<TenderPositionModel>>(tenderPositionData.ToString());

                if (model.Content != null)
                {
                    if (model.Content.RazpisPozicijaID > 0)//We update existing record in DB
                        tenderRepo.SaveTenderPosition(model.Content);
                    else // We add and save new recod to DB 
                        model.Content.RazpisPozicijaID = tenderRepo.SaveTenderPosition(model.Content, false);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpPost]
        public IHttpActionResult SaveTenderAndTenderPosition([FromBody]object tenderData)
        {
            WebResponseContentModel<TenderFullModel> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<TenderFullModel>>(tenderData.ToString());

                if (model.Content != null)
                {
                    if (model.Content.RazpisID > 0)//We update existing record in DB
                        tenderRepo.SaveTenderWithTenderPositions(model.Content);
                    else // We add and save new recod to DB 
                        model.Content.RazpisID = tenderRepo.SaveTenderWithTenderPositions(model.Content, false);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpGet]
        public IHttpActionResult DeleteTenderPosition(int tenderPositionID)
        {
            WebResponseContentModel<bool> deleteTenderPosition = new WebResponseContentModel<bool>();
            try
            {
                deleteTenderPosition.Content = tenderRepo.DeleteTenderPosition(tenderPositionID);

                if (deleteTenderPosition.Content)
                    deleteTenderPosition.IsRequestSuccesful = true;
                else
                {
                    deleteTenderPosition.IsRequestSuccesful = false;
                    deleteTenderPosition.ValidationError = ValidationExceptionError.res_04;
                }
            }
            catch (Exception ex)
            {
                deleteTenderPosition.IsRequestSuccesful = false;
                deleteTenderPosition.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(deleteTenderPosition);
            }

            return Json(deleteTenderPosition);
        }

        [HttpPost]
        public IHttpActionResult DeleteTenderPositions([FromBody]object tenderPosData)
        {
            WebResponseContentModel<List<int>> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<List<int>>>(tenderPosData.ToString());

                if (model.Content != null)
                {
                    tenderRepo.DeleteTenderPositions(model.Content);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpPost]
        public IHttpActionResult GetTransportCounByTransporterAndRoute([FromBody]object tendersData)
        {
            WebResponseContentModel<List<TransportCountModel>> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<List<TransportCountModel>>>(tendersData.ToString());

                if (model.Content != null)
                {
                    // Let's also create a sample background job
                    //BackgroundJob.Enqueue(() => tenderRepo.GetTransportCounByTransporterAndRoute(model.Content));
                    model.Content = tenderRepo.GetTransportCounByTransporterAndRoute(model.Content);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpPost]
        public IHttpActionResult GetTransportCounByTransporterIDAndRouteID([FromBody]object tendersData)
        {
            WebResponseContentModel<TransportCountModel> model = null;
            try
            {
                model = JsonConvert.DeserializeObject<WebResponseContentModel<TransportCountModel>>(tendersData.ToString());

                if (model.Content != null)
                {
                    // Let's also create a sample background job
                    //BackgroundJob.Enqueue(() => tenderRepo.GetTransportCounByTransporterAndRoute(model.Content));
                    //model.Content = tenderRepo.GetTransportCounByTransporterAndRoute(model.Content);

                    model.Content = sqlFunctionRepo.GetTransportCounByTransporterAndRoute(model.Content);

                    model.IsRequestSuccesful = true;
                }
                else
                {
                    model.IsRequestSuccesful = false;
                    model.ValidationError = ValidationExceptionError.res_09;
                }
            }
            catch (Exception ex)
            {
                model.IsRequestSuccesful = false;
                model.ValidationError = ExceptionValidationHelper.GetExceptionSource(ex);
                return Json(model);
            }

            return Json(model);
        }

        [HttpGet]
        public IHttpActionResult GetLowestAndMostRecentPriceByRouteID(int routeID)
        {
            WebResponseContentModel<decimal> tmpUser = new WebResponseContentModel<decimal>();
            Del<decimal> responseStatusHandler = ProcessContentModel;
            try
            {
                tmpUser.Content = tenderRepo.GetLowestAndMostRecentPriceByRouteID(routeID);
                responseStatusHandler(tmpUser);
            }
            catch (Exception ex)
            {
                responseStatusHandler(tmpUser, ex);
                return Json(tmpUser);
            }

            return Json(tmpUser);
        }

        [HttpGet]
        public IHttpActionResult GetTenderListByRouteIDAndRecallID(int routeID, int recallID)
        {
            WebResponseContentModel<List<TenderPositionModel>> tmpUser = new WebResponseContentModel<List<TenderPositionModel>>();
            Del<List<TenderPositionModel>> responseStatusHandler = ProcessContentModel;
            try
            {
                tmpUser.Content = tenderRepo.GetTenderListByRouteIDAndRecallID(routeID, recallID);
                responseStatusHandler(tmpUser);
            }
            catch (Exception ex)
            {
                responseStatusHandler(tmpUser, ex);
                return Json(tmpUser);
            }

            return Json(tmpUser);
        }
    }
}