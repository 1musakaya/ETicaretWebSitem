using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ETicaretWebSitem.Startup))]
namespace ETicaretWebSitem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
