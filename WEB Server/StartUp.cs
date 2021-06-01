using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebServer.Server;

namespace WEB_Server
{
    public class StartUp
    {
        public static async Task Main(string[] args)
        {
            // http://localhost:1550

            var server = new HttpServer("127.0.0.1" , 1550);
            await server.Start();

        }
    }
}
