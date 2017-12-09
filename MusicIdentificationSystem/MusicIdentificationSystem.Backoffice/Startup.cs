using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicIdentificationSystem.Backoffice.Startup))]
namespace MusicIdentificationSystem.Backoffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
