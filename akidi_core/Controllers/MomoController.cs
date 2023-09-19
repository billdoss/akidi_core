using BackEndServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static BackEndServices.Models.MobileMoney;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BackEndServices.Controllers
{


    [Route("[controller]")]
    public class MomoController : ApiController
    {
        private readonly MomoService mobileMoneyService = new MomoService();
        [HttpPost]
        [Route("api/momo/transfer")]
        public HttpResponseMessage MomoTransferPayOut(MobileMoneyPayload payload)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/transfer", JsonConvert.SerializeObject(payload));
            var momoPaymentResponse = mobileMoneyService.MobileMoneyPayOut(payload);
            return  new HttpResponseMessage(momoPaymentResponse.code) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(momoPaymentResponse) };
        }


        [HttpPost]
        [Route("api/momo/payment")]
        public HttpResponseMessage MomoPayment(MomoPaymentPayload payload)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/payment", JsonConvert.SerializeObject(payload));
            var momoPaymentResponse = mobileMoneyService.StartPaymentProcess(payload);
            return new HttpResponseMessage(momoPaymentResponse.code) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(momoPaymentResponse) };
        }




        [HttpGet]
        [Route("api/momo/payment")]
        public HttpResponseMessage FetchMomoPayment()
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/payment","");
            var momoPaymentResponse = mobileMoneyService.GetMobileMoneyPayment();
            return new HttpResponseMessage(momoPaymentResponse.code) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(momoPaymentResponse) };
        }





        [HttpGet]
        [Route("api/momo/{trans_id}")]
        public HttpResponseMessage MomoTransferByTransactionId(string trans_id)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/{trans_id}", JsonConvert.SerializeObject(trans_id));
            var momoTransactionByIdResponce = mobileMoneyService.GetMobileMoneyTransactionById(trans_id);

            return new HttpResponseMessage(momoTransactionByIdResponce.code) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(momoTransactionByIdResponce) };

        }



        [HttpGet]
        [Route("api/momo")]
        public HttpResponseMessage MomoTransferByTransactions()
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }
            Utils.Utils.SaveLog("MomoController", "api/momo","");
            var momoTransactionByIdResponce = mobileMoneyService.GetMobileMoneyTransactions();

            return new HttpResponseMessage(momoTransactionByIdResponce.code) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(momoTransactionByIdResponce) };

        }
    }
}