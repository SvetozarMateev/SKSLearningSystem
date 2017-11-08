using Microsoft.Owin;
using Owin;
using SKSLearningSystem.Data;
using SKSLearningSystem.Migrations;
using System.Data.Entity;

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
