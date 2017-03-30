using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LetsGoMangoDb.Startup))]
namespace LetsGoMangoDb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
