﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Http
{
    public class HttpHeaderCollection
        : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);
            this.headers.Add(name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        public int Count => this.headers.Count;
    }
}
