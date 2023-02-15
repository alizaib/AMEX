using System;
using System.Drawing.Drawing2D;
using static CaptchaValidationSample.Captcha.RandomHelper;

namespace CaptchaValidationSample.Captcha
{
    public class CaptchaHatchStyle
    {
        public static HatchStyle Random => GetRandom((HatchStyle[])Enum.GetValues(typeof(HatchStyle)));
        
    }
}