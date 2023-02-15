using System;

namespace Captcha_Validation.Response
{
    public class CaptchaResultResponse
    {
        public string CaptchaCode { get; set; }

        public byte[] CaptchaByteData { get; set; }

        public string CaptchaBase64Data => Convert.ToBase64String(CaptchaByteData);

        public DateTime Timestamp { get; set; }
    }
}
