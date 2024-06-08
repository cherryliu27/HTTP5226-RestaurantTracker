using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantTracker.Startup))]
namespace RestaurantTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
