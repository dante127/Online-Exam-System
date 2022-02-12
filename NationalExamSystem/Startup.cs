using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NationalExamSystem.Startup))]
namespace NationalExamSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
