using System.Web.Mvc;
using CaptchaValidationSample.Providers;
using Unity;
using Unity.AspNet.Mvc;

namespace CaptchaValidationSample.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ICaptchaImageProvider, CaptchaImageProvider>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}