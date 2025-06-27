using AppAPI.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendCodeController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IDistributedCache _cache;
        public SendCodeController( IEmailService emailService, IDistributedCache cache)
        {
            _emailService = emailService;
            _cache = cache;
        }
        //Gửi mã
        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email không được để trống.");
            }
            var verificationCode = GenerateVerificationCode();

            // Gửi mã xác minh qua email
            await _emailService.SendVerificationCode(email, verificationCode);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            };
            await _cache.SetStringAsync($"VerificationCode_{email}", verificationCode, cacheOptions);

            return Ok("Mã xác minh đã được gửi thành công.");
        }
        [HttpGet("GetVerificationCode")]
        public async Task<IActionResult> GetVerificationCode(string email)
        {
            var verificationCode = await _cache.GetStringAsync($"VerificationCode_{email}");
            if (verificationCode == null)
            {
                return NotFound("Mã xác minh không tìm thấy hoặc đã hết hạn");
            }
            return Ok(verificationCode);
        }
        private string GenerateVerificationCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
