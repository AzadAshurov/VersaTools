namespace VersaTools.Application.Abstractions.Services
{
    public interface IBarcodeService
    {
        byte[] GenerateQrCode(string text);
        byte[] GenerateBarcode(string text);
    }
}

