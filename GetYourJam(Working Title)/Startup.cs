using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetYourJam_Working_Title_.Startup))]
namespace GetYourJam_Working_Title_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
