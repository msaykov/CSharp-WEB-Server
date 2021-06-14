using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebServer.Server;
using WebServer.Server.Http;
using WebServer.Server.Responses;

namespace WEB_Server
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            // http://localhost:1550

            var server = new HttpServer(routes => routes
            .MapGet("/", new TextResponse(@"Hello from '/'"))
            .MapGet("/Cats", new HtmlResponse("<h1>Hello from Cats</h1>"))
            .MapGet("/Dogs", new HtmlResponse("<h1>Hello from Dogs</h1>")));
            await server.Start();

        }
    }
}
