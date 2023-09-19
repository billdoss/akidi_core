using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BackEndServices.Models
{
    public class ReturnMessage
    {
        public string message { get; set; }
        public Object returnObject { get; set; }

        public HttpStatusCode code { get; set; }
    }
}