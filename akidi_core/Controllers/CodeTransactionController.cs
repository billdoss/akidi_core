using BackEndServices.Models;
using BackEndServices.Services;
using BackEndServices.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = System.Web.Http.FromBodyAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace BackEndServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeTransactionController : ApiController
    {
        CodeTransactionService codeTransactionService = new CodeTransactionService();
        //CustomerService customerService = new CustomerService();
/*
        
        [HttpPost]
        [ActionName("log_code_transaction")]
        public HttpResponseMessage Log(CodeTransaction codeTransaction)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }

            Utils.Utils.SaveLog("CodeTransactionController", "/CodeTransaction", JsonConvert.SerializeObject(codeTransaction));
            int resp = codeTransactionService.Save(codeTransaction);

            AuthenticateResponse authenticateResponse = new AuthenticateResponse();

            if (resp > 0)
            {
                
                authenticateResponse = new AuthenticateResponse() { code = 1000, message = "Success", customerInfos = codeTransaction };
                return new HttpResponseMessage(HttpStatusCode.OK) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(authenticateResponse) };
            }

            authenticateResponse = new AuthenticateResponse() { code = 1010, message = "Creation failed", customerInfos = null };
            return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = JsonConvert.SerializeObject(authenticateResponse) };
        }*/


        [HttpPost]
        [Route("api/get-code")]
        public HttpResponseMessage generateTransCode([FromBody] GenerateCodeRequest transferRequestParam)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }


            Utils.Utils.SaveLog("CodeTransactionController", "/get-code", JsonConvert.SerializeObject(transferRequestParam));
            transferResponse response = TransferFundService.generateCode(transferRequestParam);
            var responseStatusCode = (response.code == 500) ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            return Request.CreateResponse(responseStatusCode, response, "application/json");
        }



        [HttpPost]
        [Route("api/cancelCode")]
        public HttpResponseMessage cancelTransCode([FromBody] payCancelCodeRequest transferRequestParam)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }
            Utils.Utils.SaveLog("CodeTransactionController", "/cancelTransCode", JsonConvert.SerializeObject(transferRequestParam));

            transferResponse response = TransferFundService.cancelRequestCode(transferRequestParam);
            var responseStatusCode = (response.code == 500) ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            return Request.CreateResponse(responseStatusCode, response, "application/json");
        }


        [HttpPost]
        [Route("api/pay-code")]
        public HttpResponseMessage payCode([FromBody] payCodeRequest payCodeRequest)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }
            Utils.Utils.SaveLog("CodeTransactionController", "/payCode", JsonConvert.SerializeObject(payCodeRequest));         

            transferResponse response = TransferFundService.payRequestCode(payCodeRequest);
            var responseStatusCode = (response.code == 500) ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            return Request.CreateResponse(responseStatusCode, response, "application/json");
        }


        [HttpPost]
        [Route("api/send-money")]
        public HttpResponseMessage sendMoney([FromBody] TransferPayload transferPayload)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = Request, ReasonPhrase = "Data missing" };
            }
            Utils.Utils.SaveLog("CodeTransactionController", "/sendMoney", JsonConvert.SerializeObject(transferPayload));

            transferResponse response = TransferFundService.transferFund(transferPayload);
            var responseStatusCode = (response.code == 500) ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            return Request.CreateResponse(responseStatusCode, response, "application/json");
        }
    }
}
