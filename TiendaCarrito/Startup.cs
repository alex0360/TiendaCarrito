using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaCarrito.Startup))]
namespace TiendaCarrito
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
