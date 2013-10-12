using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TontineClient.Sage.Startup))]
namespace TontineClient.Sage
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
