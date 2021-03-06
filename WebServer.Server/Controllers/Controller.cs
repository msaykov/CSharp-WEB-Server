using System.Runtime.CompilerServices;
using WebServer.Server.Http;
using WebServer.Server.Responses;

namespace WebServer.Server.Controllers
{
    public abstract class Controller
    {
        public Controller(HttpRequest request)
        => this.Request = request;

        protected HttpRequest Request { get; private set; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string html)
            => new HtmlResponse(html);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName , this.GetControllerName());

        //protected HttpResponse View(string view, object model = null)
        //    => new ViewResponse(view);

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}

