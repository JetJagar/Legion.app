using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Legion.app.Startup))]
namespace Legion.app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
