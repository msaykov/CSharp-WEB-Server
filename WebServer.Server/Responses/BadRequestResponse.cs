using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Http;

namespace WebServer.Server.Responses
{
    public class BadRequestResponse
        : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {

        }
    }
}
