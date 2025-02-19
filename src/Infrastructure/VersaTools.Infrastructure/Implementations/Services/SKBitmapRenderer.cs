
using SkiaSharp;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace VersaTools.Infrastructure.Implementations.Services
{
    public class SKBitmapRenderer : IBarcodeRenderer<SKBitmap>
    {
        public SKBitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {

            int margin = options.Margin;
            int inputWidth = matrix.Width;
            int inputHeight = matrix.Height;
            int desiredWidth = options.Width;
            int desiredHeight = options.Height;


            int availableWidth = desiredWidth - 2 * margin;
            int availableHeight = desiredHeight - 2 * margin;
            float scaleX = (float)availableWidth / inputWidth;
            float scaleY = (float)availableHeight / inputHeight;
            float scale = Math.Min(scaleX, scaleY);


            int finalWidth = (int)(inputWidth * scale) + 2 * margin;
            int finalHeight = (int)(inputHeight * scale) + 2 * margin;


            SKBitmap bitmap = new SKBitmap(finalWidth, finalHeight);
            using (var canvas = new SKCanvas(bitmap))
            {

                canvas.Clear(SKColors.White);
                using (var paint = new SKPaint { Color = SKColors.Black, IsAntialias = false })
                {

                    for (int y = 0; y < inputHeight; y++)
                    {
                        for (int x = 0; x < inputWidth; x++)
                        {
                            if (matrix[x, y])
                            {
                                float rectLeft = margin + x * scale;
                                float rectTop = margin + y * scale;
                                canvas.DrawRect(rectLeft, rectTop, scale, scale, paint);
                            }
                        }
                    }
                }
            }
            return bitmap;
        }


        public SKBitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, new EncodingOptions());
        }
    }
}
