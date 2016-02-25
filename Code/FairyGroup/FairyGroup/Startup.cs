using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FairyGroup.Startup))]
namespace FairyGroup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
