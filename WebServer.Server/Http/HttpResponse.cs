using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; private set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Content { get; set; } // Rresponse Body




    }
}
