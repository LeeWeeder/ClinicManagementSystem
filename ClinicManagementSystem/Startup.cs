using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicManagementSystem.Startup))]
namespace ClinicManagementSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
