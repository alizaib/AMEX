namespace CaptchaValidationSample.Providers
{
    public interface ICaptchaImageProvider
    {
        byte[] GenerateCaptchaImage();
    }
}
