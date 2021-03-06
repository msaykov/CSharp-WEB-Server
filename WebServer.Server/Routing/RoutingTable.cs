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
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

        public RoutingTable()
        {
            this.routes = new Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>>  // In C#9 we can use:  this.routes = new();

            {
                [HttpMethod.GET] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.POST] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.PUT] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
                [HttpMethod.DELETE] = new Dictionary<string, Func<HttpRequest, HttpResponse>>(),
            };

        }

        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(response, nameof(response));

            return this.Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunction, nameof(responseFunction));

            this.routes[method][path.ToLower()] = responseFunction;

            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        => MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
        => Map(HttpMethod.GET, path, responseFunction);


        public IRoutingTable MapPost(string path, HttpResponse response)
        => MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
        => Map(HttpMethod.POST, path, responseFunction);


        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!this.routes.ContainsKey(requestMethod) || !this.routes[requestMethod].ContainsKey(requestPath))
            {
                return new NotFoundResponse();
            }

            var responseFunction = this.routes[requestMethod][requestPath];
            return responseFunction(request);
        }





        
    }
}
