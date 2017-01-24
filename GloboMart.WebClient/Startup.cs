using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GloboMart.WebClient.Startup))]
namespace GloboMart.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
