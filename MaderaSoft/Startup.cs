using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MaderaSoft.Startup))]
namespace MaderaSoft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
