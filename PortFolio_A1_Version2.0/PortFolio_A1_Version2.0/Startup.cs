using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortFolio_A1_Version2._0.Startup))]
namespace PortFolio_A1_Version2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
