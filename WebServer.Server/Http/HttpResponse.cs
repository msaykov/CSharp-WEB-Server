﻿using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;

namespace WebServer.Server.Http
{
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers.Add("Server", "My Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
            //this.Content = this.GetContent();
        }

        public HttpStatusCode StatusCode { get; protected set; }

        public HttpHeaderCollection Headers { get; } = new HttpHeaderCollection();

        public string Content { get; protected set; }  // Response Body

        //protected virtual string GetContent()
        //{
        //    return null;
        //}

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");
            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }
            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();
                result.Append(this.Content);
            }
            
            return result.ToString();
        }

        protected void PrepareContent(string content, string contentType)
        {
            Guard.AgainstNull(content, nameof(content));
            Guard.AgainstNull(content, nameof(contentType));
            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();
            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", contentLength);
            this.Content = content;
        }


    }
}
