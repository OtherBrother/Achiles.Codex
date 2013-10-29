using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Achiles.Codex.Web.Startup))]
namespace Achiles.Codex.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
