using WebServer.Server.Controllers;
using WebServer.Server.Http;

namespace WEB_Server.Controllers
{
    public class HomeController
        : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
        => Text("Hello from Homepage");
    }
}
