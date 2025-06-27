using AppAPI.IService;
using AppData;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _service;

        public KhachHangController(IKhachHangService service)
        {
            _service = service;
        }
        [HttpPost("DangKy")]
        public async Task<IActionResult> DangKy([FromBody] KhachHang khachHang)
        {
            try
            {
                var result = await _service.AddKhachHang(khachHang);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetAllKhachHang")]
        public async Task<ActionResult<List<KhachHang>>> GetAllKhachHang()
        {
            return Ok(await _service.GetAllKhachHang());
        }
        
     
        [HttpPost("Login") ]
        public async Task<IActionResult> LoginKH([FromBody] KhachHang khach)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var kh = await _service.Login(khach.Email, khach.MatKhau);
                    return Ok(kh);
                }
                catch(Exception ex)
                {
                    return BadRequest(new {message = ex.Message});
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<KhachHang>> Get(Guid id)
        {
            var kh = await _service.GetIdKhachHang(id);
            if(kh == null)
            {
                return NotFound();
            }    
            return Ok(kh);
        }
        [HttpPut("Sua")]
        public async Task<ActionResult<KhachHang>> Put(KhachHang kh)
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateKhachHang(kh);
                return Ok(kh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("SuaChoKhachHang")]
        public async Task<ActionResult<KhachHang>> khachHangSua(KhachHang kh)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.UpdateKHThongTin(kh);
                return Ok(kh);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("Xoa")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _service.DeleteKhachHang(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("DoiMK")]
        public async Task<IActionResult> ChangePassword(Guid id, string newPassword, string confirmPassword)
        {
            if(newPassword != confirmPassword)
            {
                return BadRequest(new { message = "Mật Khẩu xác nhân không khớp." });
            }
            var result = await _service.ChangePassword(id, newPassword);
            if (result)
            {
                return Ok(new { message = "Đổi mật khẩu thành công." });
            }
            return BadRequest(new { message = "Đổi mật khẩu thất bại." });
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> RestPassword(string email, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                return BadRequest(new { message = "Mật Khẩu xác nhân không khớp." });
            }
            var result = await _service.ResetPass(email, newPassword);
            if (result)
            {
                return Ok(new { message = "Đổi Mật Khẩu Thành Công." });
            }
            return BadRequest(new { message = "Đổi mật khẩu thất bại." });
        }

        [HttpGet("GetCustomerByPhoneOrEmail")]
        public async Task<IActionResult> GetCustomerByPhoneOrEmail([FromQuery] string phoneOrEmail)
        {
            try
            {
                var customer = await _service.GetCustomerByPhoneOrEmailAsync(phoneOrEmail);
                if (customer == null)
                {
                    return NotFound(new { message = "Không tìm thấy khách hàng." });
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //Check sdt voi email HoangLong
        [HttpGet("CheckSDT")]
        public async Task<IActionResult> CheckSdt(string soDienThoai)
        {
            try
            {
                var sdt = await _service.CheckSDT(soDienThoai);
                return Ok(sdt);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi trong quá trình kiểm tra", error = ex.Message });
            }
        }
        [HttpGet("CheckMail")]
        public async Task<IActionResult> CheckMail(string mail)
        {
            try
            {
                var email = await _service.CheckMail(mail);
                return Ok(email);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi trong quá trình kiểm tra", error = ex.Message });
            }
        }
        [HttpGet("SearchKhachHang")]
        public async Task<IActionResult> SearchKhachHang(string? name)
        {
            try
            {
                var sanPham = await _service.SearchKhachHang(name); // Await the async method
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
