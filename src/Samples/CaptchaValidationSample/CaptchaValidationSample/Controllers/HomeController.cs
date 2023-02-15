using System.Web.Mvc;
using CaptchaValidationSample.Providers;
using CaptchaValidationSample.Request;

namespace CaptchaValidationSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICaptchaImageProvider _captchaImageProvider;

        public HomeController(ICaptchaImageProvider captchaImageProvider)
        {
            _captchaImageProvider = captchaImageProvider;
        }
        public ActionResult Index()
        {
            return View(new CaptchaValidateRequest());
        }

        public ActionResult CaptchaImage()
        {
            var s = _captchaImageProvider.GenerateCaptchaImage();
            return File(s, "image/png");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}