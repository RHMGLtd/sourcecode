using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace coolbunny.common.Extensions
{
    public static class imageExtension
    {
        static private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static void SaveAsJpeg(this Image img, string fileName, Int64 quality)
        {
            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            var qualityEncoder = Encoder.Quality;

            using (var ep = new EncoderParameters(1))
            {
                using (var qualityEncoderParameter = new EncoderParameter(qualityEncoder, quality))
                {
                    ep.Param[0] = qualityEncoderParameter;
                    img.Save(fileName, jgpEncoder, ep);
                }
            }
        }

        public static void SaveAsGif(this Image img, string fileName, Int64 quality)
        {
            var gifEncoder = GetEncoder(ImageFormat.Gif);
            var qualityEncoder = Encoder.Quality;

            using (var ep = new EncoderParameters(1))
            {
                using (var qualityEncoderParameter = new EncoderParameter(qualityEncoder, quality))
                {
                    ep.Param[0] = qualityEncoderParameter;
                    img.Save(fileName, gifEncoder, ep);
                }
            }
        }

        public static Image Resize(this Image img, int width, int height, InterpolationMode interpolationMode)
        {
            Image cropedImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(cropedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = interpolationMode;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, 0, 0, width, height);
            }

            return cropedImage;
        }

        public static Image Resize(this Image img, int width, int height)
        {
            return img.Resize(width, height, InterpolationMode.HighQualityBicubic);
        }

        public static Image Resize(this Image img, int width)
        {
            var changeRatio = width / (decimal)img.Width;
            var extrapolatedHeight = img.Height * changeRatio;
            return Resize(img, width, (int)Math.Ceiling(extrapolatedHeight));
        }

        private static Rectangle EnsureAspectRatio(this Image image, int width, int height)
        {
            var aspectRatio = width / (float)height;
            float calculatedWidth = image.Width;
            float calculatedHeight = image.Height;
            if (image.Width >= image.Height)
            {
                if (width > height)
                {
                    calculatedHeight = image.Width / aspectRatio;
                    if (calculatedHeight > image.Height)
                    {
                        calculatedHeight = image.Height;
                        calculatedWidth = calculatedHeight * aspectRatio;
                    }
                }
                else
                {
                    calculatedWidth = image.Height * aspectRatio;
                    if (calculatedWidth > image.Width)
                    {
                        calculatedWidth = image.Width;
                        calculatedHeight = calculatedWidth / aspectRatio;
                    }
                }
            }
            else
            {
                if (width < height)
                {
                    calculatedHeight = image.Width / aspectRatio;
                    if (calculatedHeight > image.Height)
                    {
                        calculatedHeight = image.Height;
                        calculatedWidth = calculatedHeight * aspectRatio;
                    }
                }
                else
                {
                    calculatedWidth = image.Height * aspectRatio;
                    if (calculatedWidth > image.Width)
                    {
                        calculatedWidth = image.Width;
                        calculatedHeight = calculatedWidth / aspectRatio;
                    }
                }
            }
            return Rectangle.Ceiling(new RectangleF((image.Width - calculatedWidth) / 2, 0, calculatedWidth, calculatedHeight));
        }

        public static Image ResizeToCanvas(this Image img, int width, int height, out Rectangle cropRectangle)
        {
            return img.ResizeToCanvas(width, height, InterpolationMode.HighQualityBicubic, out cropRectangle);
        }

        public static Image ResizeToCanvas(this Image img, int width, int height, InterpolationMode interpolationMode, out Rectangle cropRectangle)
        {
            cropRectangle = EnsureAspectRatio(img, width, height);
            Image cropedImage = new Bitmap(width, height);

            using (var g = Graphics.FromImage(cropedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = interpolationMode;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, width, height), cropRectangle, GraphicsUnit.Pixel);
            }

            return cropedImage;
        }

        public static Image ResizeToCanvas(this Image img, int width, int height, RectangleF cr)
        {
            return img.ResizeToCanvas(width, height, InterpolationMode.HighQualityBicubic, cr);
        }

        public static Image ResizeToCanvas(this Image img, int width, int height, InterpolationMode interpolationMode, RectangleF cr)
        {
            Image cropedImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(cropedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = interpolationMode;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, width, height), cr, GraphicsUnit.Pixel);
            }

            return cropedImage;
        }

    }

}