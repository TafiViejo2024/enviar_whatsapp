using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(web_app_mensajes.Startup))]
namespace web_app_mensajes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
