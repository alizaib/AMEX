using System;

namespace CaptchaValidationSample.Captcha
{
    public static class RandomHelper
    {
        public static T GetRandom<T>(T[] items) {
            var random = new Random();
            return items[random.Next(items.Length)];
        }
    }
}