using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchHack.Web.Startup))]
namespace SearchHack.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
