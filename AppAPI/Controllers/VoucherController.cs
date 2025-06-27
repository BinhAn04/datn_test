using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _service;
        private readonly ILogger<VoucherController> _logger;
        public VoucherController(IVoucherService service, ILogger<VoucherController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllVouchers")]
        public async Task<IActionResult> GetAllVouchers()
        {
            var vouchers = await _service.GetVouchersAsync();
            return Ok(vouchers);
        }

        [HttpPost("CreateVoucher")]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherDto voucher, [FromQuery] List<Guid> selectedKhachHangIds)
        {
            try
            {
                // Kiểm tra thông tin voucher
                if (voucher == null)
                    return BadRequest("Voucher cannot be null");

                // Đảm bảo ID mới
                voucher.VoucherId = Guid.NewGuid();
                if (string.IsNullOrEmpty(voucher.NguoiTao))
                    voucher.NguoiTao = "Admin";
                voucher.SoLuongVoucherConLai = voucher.TongSoLuongVoucher;

                // Gọi phương thức CreateAsync với cả voucher và danh sách khách hàng
                var result = await _service.CreateAsync(voucher, selectedKhachHangIds);
                if (result)
                {
                    return Ok(voucher);
                }

                return BadRequest("Failed to create voucher");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating voucher");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("UpdateVoucher/{id}")]
        public async Task<IActionResult> UpdateVoucher(Guid id, [FromBody] Voucher voucher)
        {
            try
            {
                if (id != voucher.VoucherId)
                {
                    return BadRequest("Id mismatch");
                }

                var existingVoucher = await _service.GetVoucherByIdAsync(id);
                if (existingVoucher == null)
                {
                    return NotFound("Voucher not found");
                }

                // Preserve original creation info
                voucher.NgayTao = existingVoucher.NgayTao;
                voucher.NguoiTao = existingVoucher.NguoiTao;

                // Set update info
                voucher.NgayUpdate = DateTime.Now;
               

                var result = await _service.UpdateAsync(voucher);
                if (result)
                {
                    return Ok(voucher);
                }
                return BadRequest("Failed to update voucher");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("UpdateVoucherStatusAsync/{id}")]
        public async Task<IActionResult> UpdateVoucherStatusAsync(Guid id, [FromBody] Voucher voucher, int status)
        {
            try
            {
                if (id != voucher.VoucherId)
                {
                    return BadRequest("Id mismatch");
                }

                var existingVoucher = await _service.GetVoucherByIdAsync(id);
                if (existingVoucher == null)
                {
                    return NotFound("Voucher not found");
                }
                var result = await _service.UpdateVoucherStatusAsync(voucher,status);
                if (result)
                {
                    return Ok(voucher);
                }
                return BadRequest("Failed to update voucher");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteVoucher")]
        public async Task<IActionResult> DeleteVoucher(Guid id)
        {
            var voucher = await _service.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound("Voucher not found");
            }
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return StatusCode(500, "Error while deleting the Voucher");
            }
            return Ok("Voucher deleted successfully");
        }

        [HttpGet("GetVoucherById")]
        public async Task<IActionResult> GetVoucherById(Guid id)
        {
            var voucher = await _service.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound("Voucher not found.");
            }
            return Ok(voucher);
        }
        [HttpPost("AddLichSuSuDungVoucher")]
        public async Task<IActionResult> AddLichSuSuDungVoucher([FromBody] LichSuSuDungVoucher lichSuSuDungVoucher)
        {
            try
            {
                if (lichSuSuDungVoucher == null)
                {
                    return BadRequest("LichSuSuDungVoucher cannot be null");
                }

                var result = await _service.AddLichSuSuDungVoucherAsync(lichSuSuDungVoucher);
                if (result)
                {
                    return Ok("LichSuSuDungVoucher added successfully");
                }
                return BadRequest("Failed to add LichSuSuDungVoucher");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding LichSuSuDungVoucher ");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetKHDaNhanVoucher")]
        public async Task<IActionResult> GetKhachHangDaNhanVoucherAsync(Guid voucherId)
        {
            var khachhangs = await _service.GetKhachHangDaNhanVoucherAsync(voucherId);
            if (khachhangs == null)
            {
                return NotFound("Khach Hang Da Nhan Voucher not found.");
            }
            return Ok(khachhangs);
        }

        [HttpGet("available/{khachHangId}")]
        public async Task<IActionResult> GetAvailableVouchers(Guid khachHangId)
        {
            try
            {
                Console.WriteLine($"API: Requesting vouchers for customer ID: {khachHangId}");

                var vouchers = await _service.GetAvailableVouchersForCustomerAsync(khachHangId);

                Console.WriteLine($"API: Found {vouchers.Count} vouchers");

                if (vouchers == null || vouchers.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "Không tìm thấy voucher khả dụng cho khách hàng này.",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                return Ok(vouchers); // Trả về trực tiếp danh sách vouchers
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = "Đã xảy ra lỗi khi lấy danh sách voucher.",
                        StatusCode = StatusCodes.Status500InternalServerError
                    }
                );
            }
        }
        [HttpGet("checkMaVoucher")]
        public async Task<IActionResult> CheckMaVoucher(string maVoucher)
        {
            try
            {
                var email = await _service.MaVoucher(maVoucher);
                return Ok(email);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi trong quá trình kiểm tra", error = ex.Message });
            }
        }
    }
}
