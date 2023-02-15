using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace CaptchaValidationSample.Captcha
{
    public class CaptchaImageGenerator
    {
        private readonly CaptchaOptions _options;
        private readonly Random _random;
        private readonly int _allowedCharWidth;
        private readonly int _yPosition;

        public CaptchaImageGenerator(CaptchaOptions options)
        {
            _options = options;
            _random = new Random();
            _allowedCharWidth = _options.Width / _options.Text.Length;
            _yPosition = _options.Height/5;
        }

        public byte[] GetImage()
        {
            Bitmap bitmapImage = new Bitmap(_options.Width, _options.Height, PixelFormat.Format24bppRgb);

            using (var graphics = Graphics.FromImage(bitmapImage))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                
                DrawBackground(graphics);

                for (int i = 0; i < _options.Text.Length; i++)
                {   
                    DrawCharacter(graphics,_options.Text[i], _allowedCharWidth * i);
                }
            }

            byte[] bytes = bitmapImage.ToByteArray(ImageFormat.Png);

            bitmapImage.Dispose();

            return bytes;
        }        

        private void DrawBackground(Graphics graphics)
        {
            RectangleF rectangle = new RectangleF(0, 0, _options.Width, _options.Height);
            using (var hatchBrush = new HatchBrush(CaptchaHatchStyle.Random, CaptchColor.Random, Color.White))
            {
                graphics.FillRectangle(hatchBrush, rectangle);
            }
        }

        private void DrawCharacter(Graphics graphics, char character, int xPos)
        {            
            Matrix rotationMatrix = GetRotationMatrix(xPos);

            graphics.Transform = rotationMatrix;

            using (SolidBrush brush = new SolidBrush(CaptchColor.Random))
            {
                graphics.DrawString(character.ToString(), CaptchaFont.Random, brush, xPos, _yPosition);
            }

            graphics.ResetTransform();
        }

        private Matrix GetRotationMatrix(int xPos)
        {
            Matrix rotationMatrix = new Matrix();
            float angle = _random.Next(-20, 20);
            rotationMatrix.RotateAt(angle, new PointF(xPos, _yPosition));
            return rotationMatrix;
        }
    }
}