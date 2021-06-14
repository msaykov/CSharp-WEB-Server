﻿using System.Text;
using WebServer.Server.Http;
using WebServer.Server.Common;

namespace WebServer.Server.Responses
{
    public class ContentResponse
        : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);
            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();
            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);
            this.Content = text;
        }
    }
}
