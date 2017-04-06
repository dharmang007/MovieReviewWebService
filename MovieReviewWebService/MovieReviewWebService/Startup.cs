using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovieReviewWebService.Startup))]
namespace MovieReviewWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
