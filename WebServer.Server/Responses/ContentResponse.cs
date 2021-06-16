using System.Text;
using WebServer.Server.Http;
using WebServer.Server.Common;

namespace WebServer.Server.Responses
{
    public class ContentResponse
        : HttpResponse
    {
        public ContentResponse(string content, string contentType)
            : base(HttpStatusCode.OK)
        {
            this.PrepareContent(content, contentType);
        }
    }
}
