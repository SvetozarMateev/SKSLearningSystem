using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SKSLearningSystem.Startup))]
namespace SKSLearningSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
