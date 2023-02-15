using System.Drawing;
using System.Linq;
using System;
using static CaptchaValidationSample.Captcha.RandomHelper;

namespace CaptchaValidationSample.Captcha
{
    public class CaptchaFont
    {
        public static readonly string[] FontNames = new [] { "Arial", "Helvetica", "Times New Roman", "Verdana" };        
        public static readonly float[] FontEmSizes  = new []{ 15f, 20f, 25f/*, 30f, 35f*/ };        

        public static Font GetFont(string fontName, float emSize, FontStyle fontStyle) {
            if (!FontEmSizes.Contains(emSize))
                throw new Exception($"{emSize} is not allowed as font size");

            if (!FontNames.Contains(fontName))
                throw new Exception($"{fontName} is not allowed as font name");

            var font = new Font(fontName, emSize, fontStyle);
            return font;
        }

        public static Font Random => GetFont(GetRandom(FontNames), GetRandom(FontEmSizes), 
                                             GetRandom((FontStyle[])Enum.GetValues(typeof(FontStyle))));

    }
}