using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;

namespace CaptchaValidationSample.Captcha
{
    public class CaptchaOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Text { get; set; }
        public CaptchaFont Font { get; set; }
        public Style Styles { get; set; }
    }

    public class CaptchaFont
    {
        public string[] FontNames { get; } = { "Arial", "Helvetica", "Times New Roman", "Verdana" };
        public FontStyle[] FontStyles { get; } = { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, FontStyle.Bold | FontStyle.Italic };
        public int[] FontEmSizes { get; } = { 15, 20, 25, 30, 35 };

    }

    public class Style
    {
        public HatchStyle[] HatchStyles { get; } = {
            HatchStyle.BackwardDiagonal, HatchStyle.Cross,
            HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
            HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical,
            HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
            HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid,
            HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
            HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard,
            HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
            HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal,
            HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
            HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal,
            HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
            HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard,
            HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
            HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis,
            HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
            HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
        };
    }
    public class CaptchaImageGenerator
    {
        private readonly CaptchaOptions _options;

        public CaptchaImageGenerator(CaptchaOptions options)
        {
            _options = options;
        }

        public byte[] GetImage()
        {
            Random random = new Random();

            Bitmap outputBitmap = new Bitmap(_options.Width, _options.Height, PixelFormat.Format24bppRgb);

            using (var graphics = Graphics.FromImage(outputBitmap))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                RectangleF rectangle = new RectangleF(0, 0, _options.Width, _options.Height);
                DrawBackground(graphics, rectangle, _options.Styles.HatchStyles, random);

                int charsCount = _options.Text.Length;
                int charXPosition = _options.Width / (charsCount + 1);

                Font[] fonts = _options.Font.FontNames.SelectMany(
                    name => _options.Font.FontStyles.Select(
                        style => new Font(name, _options.Font.FontEmSizes[0], style))).ToArray();

                for (int i = 0; i < charsCount; i++)
                {
                    int x = charXPosition * (i + 1);
                    int y = _options.Height / 2;
                    DrawCharacter(
                        graphics,
                        _options.Text[i],
                        fonts,
                        _options.Font.FontEmSizes,
                        Color.FromArgb(random.Next(0, 100), random.Next(0, 100), random.Next(0, 100)),
                        random,
                        x,
                        y
                    );
                }
            }

            MemoryStream memoryStream = new MemoryStream();
            outputBitmap.Save(memoryStream, ImageFormat.Png);
            byte[] bytes = memoryStream.ToArray();

            outputBitmap.Dispose();

            return bytes;
        }        

        private void DrawBackground(Graphics graphics, RectangleF rectangle, HatchStyle[] hatchStyles, Random random)
        {
            using (var hatchBrush = new HatchBrush(
                       hatchStyles[random.Next(hatchStyles.Length)],
                       Color.FromArgb((random.Next(100, 255)), (random.Next(100, 255)), (random.Next(100, 255))),
                       Color.White))
            {
                graphics.FillRectangle(hatchBrush, rectangle);
            }
        }

        private void DrawCharacter(Graphics graphics, char character, Font[] fonts, int[] fontSizes, Color color, Random random, int x, int y)
        {
            Matrix rotationMatrix = new Matrix();
            float angle = random.Next(-40, 40);
            rotationMatrix.RotateAt(angle, new PointF(x, y));
            graphics.Transform = rotationMatrix;

            Font font = fonts[random.Next(fonts.Length)];
            int fontSize = fontSizes[random.Next(fontSizes.Length)];
            font = new Font(font.FontFamily, fontSize, font.Style);

            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.DrawString(character.ToString(), font, brush, x, y);
            }

            graphics.ResetTransform();
        }

    }
}