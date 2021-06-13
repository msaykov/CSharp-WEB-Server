using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;
using WebServer.Server.Http;
using WebServer.Server.Responses;

namespace WebServer.Server.Routing
{
    public class RoutingTable 
        : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new Dictionary<HttpMethod, Dictionary<string, HttpResponse>>()  // In C#9 we can use:  this.routes = new();
        
            {
                [HttpMethod.GET] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.POST] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.PUT] = new Dictionary<string, HttpResponse>(),
                [HttpMethod.DELETE] = new Dictionary<string, HttpResponse>(),
            };
        
    }

        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
        {
            return method switch
            {
                HttpMethod.GET => this.MapGet(url, response),
                HttpMethod.POST => this.MapGet(url, response),
                HttpMethod.PUT => this.MapGet(url, response),
                HttpMethod.DELETE => this.MapGet(url, response),
                _ => throw new InvalidOperationException($"Method '{method}' is not supported."),
            };
        }

        public IRoutingTable MapGet(string url, HttpResponse response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[HttpMethod.GET][url] = response;

            return this;
        }

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestUrl];
        }
    }
}
