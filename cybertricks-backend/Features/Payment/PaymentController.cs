using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Text;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/vietqr")]
    public class VietQrController : ControllerBase
    {
        private static string Tlv(string id, string value)
        {
            var len = value?.Length ?? 0;
            return $"{id}{len:00}{value}";
        }

        private static ushort Crc16Ccitt(byte[] data)
        {
            ushort crc = 0xFFFF;
            foreach (var b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                    crc = (ushort)(((crc & 0x8000) != 0) ? (crc << 1) ^ 0x1021 : crc << 1);
            }
            return crc;
        }

        private static string BuildVietQr(
            string bankBin, string accountNumber, string receiverName, string city,
            long? amountVnd = null, string? description = null, bool dynamic = false)
        {
            if (string.IsNullOrWhiteSpace(bankBin)) throw new ArgumentException("bankBin required");
            if (string.IsNullOrWhiteSpace(accountNumber)) throw new ArgumentException("account required");
            if (string.IsNullOrWhiteSpace(receiverName)) receiverName = "RECEIVER";

            var p = new StringBuilder();
            p.Append(Tlv("00", "01"));
            p.Append(Tlv("01", dynamic ? "12" : "11"));

            var t38 = new StringBuilder();
            t38.Append(Tlv("00", "A000000727")); // VietQR AID
            t38.Append(Tlv("01", bankBin.Trim()));
            t38.Append(Tlv("02", accountNumber.Trim()));
            t38.Append(Tlv("08", "QRIBFTTA"));
            p.Append(Tlv("38", t38.ToString()));

            p.Append(Tlv("52", "0000"));
            p.Append(Tlv("53", "704"));
            if (amountVnd.HasValue && amountVnd.Value > 0)
                p.Append(Tlv("54", amountVnd.Value.ToString()));
            p.Append(Tlv("58", "VN"));
            p.Append(Tlv("59", receiverName.Trim().ToUpperInvariant()));
            p.Append(Tlv("60", string.IsNullOrWhiteSpace(city) ? "HANOI" : city.Trim().ToUpperInvariant()));

            if (!string.IsNullOrWhiteSpace(description))
            {
                var t62 = new StringBuilder();
                t62.Append(Tlv("01", description));
                p.Append(Tlv("62", t62.ToString()));
            }

            var baseStr = p.ToString() + "6304" + "0000";
            // Dùng ASCII là ổn nếu không có dấu; mô tả nên bỏ dấu để nhất quán
            var bytes = Encoding.ASCII.GetBytes(baseStr);
            var crc = Crc16Ccitt(bytes).ToString("X4");
            return p.ToString() + "6304" + crc;
        }

        // ============== API ==============
        /// GET /api/vietqr?bankBin=970415&account=1900123456789&name=CONG%20TY%20ABC&city=HANOI&amount=150000&desc=Thanh%20toan%20don%20123
        [HttpGet("")]
        [AllowAnonymous]
        [Produces("image/png")]
        public IActionResult Get(
            [FromQuery] string bankBin,
            [FromQuery] string account,
            [FromQuery] string name,
            [FromQuery] string city = "HANOI",
            [FromQuery] long? amount = null,
            [FromQuery] string? desc = null,
            [FromQuery] bool dynamic = false,
            [FromQuery] int pixelsPerModule = 10)
        {
            try
            {
                var emv = BuildVietQr(bankBin, account, name, city, amount, desc, dynamic);
                using var gen = new QRCodeGenerator();
                using var data = gen.CreateQrCode(emv, QRCodeGenerator.ECCLevel.M);
                using var qr = new PngByteQRCode(data);
                var pngBytes = qr.GetGraphic(Math.Max(4, Math.Min(pixelsPerModule, 24)));
                Response.Headers.CacheControl = "no-store";
                return File(pngBytes, "image/png");
            }
            catch (Exception ex)
            {
                // Log ex (Serilog/NLog...) nếu có
                return Problem(
                    title: "Cannot create VietQR",
                    detail: ex.Message,
                    statusCode: 400);
            }
        }
    }
}
