using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EzPartyEzLife.Startup))]
namespace EzPartyEzLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
