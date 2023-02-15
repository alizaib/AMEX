using System;
using System.Drawing;

namespace CaptchaValidationSample.Captcha
{
    public class CaptchColor
    {
        private static Random random = new Random();
        public static Color Random => Color.FromArgb((random.Next(100, 255)), (random.Next(100, 255)), (random.Next(100, 255)));
    }
}
