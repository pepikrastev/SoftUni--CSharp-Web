using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System.IO;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            //with [CallerMemberName]
            return this.View();

            // return this.View("Index");
        }
    }
}
