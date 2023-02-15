using System;
using System.Text;
using CaptchaValidationSample.Captcha;

namespace CaptchaValidationSample.Providers
{
    public class CaptchaImageProvider : ICaptchaImageProvider
    {
        private readonly CaptchaFont _fontOptions = new CaptchaFont();
        private readonly Style _styleOptions = new Style();

        public byte[] GenerateCaptchaImage()
        {

            var options = new CaptchaOptions
            {
                Width = 150,
                Height = 50,
                Text = GenerateCaptchaCode(),
                Font = _fontOptions,
                Styles = _styleOptions
            };

            var generator = new CaptchaImageGenerator(options);
            return generator.GetImage();
        }

        private string GenerateCaptchaCode()
        {
            var letters = "1234567890ABCDEFGHJKLMNPRTUVWXYZabcdefghijklmnopqurstuvwxyz";

            var rand = new Random();
            var maxRand = 6;

            var sb = new StringBuilder();
            for (var i = 0; i < 6; i++)
            {
                var index = rand.Next(maxRand);
                sb.Append(letters[index]);
            }

            return sb.ToString();
        }
    }
}