using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicineProject.Startup))]
namespace MedicineProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
