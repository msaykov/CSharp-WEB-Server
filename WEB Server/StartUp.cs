using WebServer.Server.Controllers;
using System.Threading.Tasks;
using WebServer.Server;
using WEB_Server.Controllers;

namespace WEB_Server
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            // http://localhost:1550

            var server = new HttpServer(routes => routes
            .MapGet<HomeController>("/", c => c.Index())
            .MapGet<AnimalsController>("/Cats", c => c.Cats())
            .MapGet<AnimalsController>("/Dogs", c => c.Dogs()));
            await server.Start();

        }
    }
}
