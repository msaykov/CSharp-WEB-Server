using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Http
{
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
            => this.headers.Add(header.Name, header);

        public int Count => this.headers.Count;
    }
}
