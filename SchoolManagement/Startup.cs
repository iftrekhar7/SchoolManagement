using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolManagement.Startup))]
namespace SchoolManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
