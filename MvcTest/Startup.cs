using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcTest.Startup))]
namespace MvcTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
