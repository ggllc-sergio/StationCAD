using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StationCAD.Web.Startup))]
namespace StationCAD.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
