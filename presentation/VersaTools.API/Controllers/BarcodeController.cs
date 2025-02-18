using Microsoft.AspNetCore.Mvc;
using VersaTools.Application.Abstractions.Services;

namespace VersaTools.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeService _barcodeService;

        public BarcodeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        [HttpGet("qrcode")]
        public IActionResult GenerateQrCode([FromQuery] string text)
        {
            byte[] image = _barcodeService.GenerateQrCode(text);
            return File(image, "image/png");
        }

        [HttpGet("barcode")]
        public IActionResult GenerateBarcode([FromQuery] string text)
        {
            byte[] image = _barcodeService.GenerateBarcode(text);
            return File(image, "image/png");
        }
    }
}
