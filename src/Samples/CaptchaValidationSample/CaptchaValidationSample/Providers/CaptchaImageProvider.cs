using CaptchaValidationSample.Captcha;
using System;
using System.Linq;

namespace CaptchaValidationSample.Providers
{
    //TODO: refactor this as discussed
    //CaptchService -> IImageGenerator, IRepository
    //Captch
    //Validate
    public class CaptchaImageProvider : ICaptchaImageProvider
    {
        public byte[] GenerateCaptchaImage()
        {
            var options = new CaptchaOptions
            {
                Width = 200,
                Height = 50,
                Text = GenerateCaptchaCode()
            };

            var generator = new CaptchaImageGenerator(options);
            return generator.GetImage();
        }

        private string GenerateCaptchaCode()
        {
            var letters = "1234567890ABCDEFGHJKLMNPRTUVWXYZabcdefghijklmnopqurstuvwxyz";            
            var rand = new Random();
            var randomChars = letters.Select(_ => letters[rand.Next(letters.Length)])
                                     .Take(6);
            var result = string.Concat(randomChars.ToArray());

            return result;
        }
    }
}