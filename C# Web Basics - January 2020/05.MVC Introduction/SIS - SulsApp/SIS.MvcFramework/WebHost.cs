using SIS.HTTP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static async Task StartAsync(IMvcApplication application)
        {
            var routeTable = new List<Route>();
            application.ConfigureServices();
            application.Configure(routeTable);

            var httpSurver = new HttpServer(80, routeTable);
            await httpSurver.StartAsync();
        }
    }
}
