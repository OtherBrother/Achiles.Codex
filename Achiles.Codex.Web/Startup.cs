using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Achilles.Codex.Web.Startup))]
namespace Achilles.Codex.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
