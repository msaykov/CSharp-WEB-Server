using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebServer.Server.Http;

namespace WebServer.Server.Responses
{
    public class ViewResponse
        : HttpResponse
    {
        private const char PathSeparator = '/';

        public ViewResponse(string viewName, string controllerName) 
            : base(HttpStatusCode.OK)
        {
            this.GetHtml(viewName, controllerName);
        }

        private void GetHtml(string viewName, string controllerName)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");
            if (!File.Exists(viewPath))
            {
                this.StatusCode = HttpStatusCode.NotFound;
                return;
            }

            var viewContent = File.ReadAllText(viewPath);
            this.PrepareContent(viewContent, HttpContentType.Html);

        }
    }
}
