using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilterRLC.Startup))]
namespace FilterRLC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
