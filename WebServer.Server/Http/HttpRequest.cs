using System;
using System.Collections.Generic;
using System.Linq;

namespace WebServer.Server.Http
{
    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod Method { get; private set; }

        public string Path { get; private set; }

        public Dictionary<string, string> Query { get; private set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);
            var startLine = lines.First().Split(" ");
            var method = ParseHttpMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url); // Tuple - returns two variables

            var headerLines = lines.Skip(1);
            var headers = ParseHttpHeaderCollection(headerLines);
            var bodyLines = lines.Skip(headers.Count + 2).ToArray();
            var body = string.Join(NewLine, bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = headers,
                Body = body
            };

        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?');
            var path = urlParts[0].ToLower();
            var query = urlParts.Length > 1
                ? ParseQuery(urlParts[1])
                : new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string queryString)
                 => queryString
                    .Split('&')
                    .Select(part => part.Split('='))
                    .Where(part => part.Length == 2)
                    .ToDictionary(p => p[0], p => p[1]);


        private static HttpHeaderCollection ParseHttpHeaderCollection(IEnumerable<string> headerLines)
        {
            var headerCollection = new HttpHeaderCollection();
            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty)
                {
                    break;
                }

                var headerParts = headerLine.Split(":", 2);


                if (headerParts.Length != 2)
                {
                    throw new InvalidOperationException("Request is not valid.");
                }

                var headerName = headerParts[0];
                var headerValue = headerParts[1].Trim();

                headerCollection.Add(headerName, headerValue);

            }

            return headerCollection;


        }

        private static HttpMethod ParseHttpMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.GET,
                "POST" => HttpMethod.POST,
                "PUT" => HttpMethod.PUT,
                "DELETE" => HttpMethod.DELETE,
                _ => throw new InvalidOperationException($"Method '{method}' is not supported."),
            };
        }

        //private static string[] GetStartLine(string request)
        //{

        //}
    }
}
