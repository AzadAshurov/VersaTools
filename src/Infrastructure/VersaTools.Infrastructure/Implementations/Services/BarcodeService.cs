using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;
using SkiaSharp;
using VersaTools.Application.Abstractions.Services;
using ZXing;
using ZXing.Common;


namespace VersaTools.Infrastructure.Implementations.Services
{
    public class BarcodeService : IBarcodeService
    {
        public byte[] GenerateQrCode(string text)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            using (var qrCode = new BitmapByteQRCode(qrCodeData))
            {
                return qrCode.GetGraphic(20);
            }
        }

        public byte[] GenerateBarcode(string text)
        {
            var writer = new BarcodeWriter<SKBitmap>
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 300,
                    Margin = 10
                },
                Renderer = new SKBitmapRenderer()
            };

            SKBitmap bitmap = writer.Write(text);
            byte[] result = ConvertSKBitmapToByteArray(bitmap);
            bitmap.Dispose();
            return result;
        }

        private byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            if (OperatingSystem.IsWindows())
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            else
            {
                throw new PlatformNotSupportedException("Bitmap to byte array conversion is only supported on Windows.");
            }
        }

        private byte[] ConvertSKBitmapToByteArray(SKBitmap bitmap)
        {
            using (SKImage image = SKImage.FromBitmap(bitmap))
            using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                return data.ToArray();
            }
        }
    }
}
