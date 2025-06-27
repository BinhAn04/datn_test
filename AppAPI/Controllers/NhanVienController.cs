using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienService _service;
        
        public NhanVienController(INhanVienService service)
        {
            _service = service;   
        }
        [HttpGet("GetAllNhanVien")]
        public async Task<ActionResult<List<NhanVien>>> GetAllNhanVien()
        {
            return Ok(await _service.GetAllNhaVien());
        }
        [HttpGet("GetIDNhanVien")]
        public async Task<ActionResult<NhanVien>> GetIdNhanVien(Guid id) 
        {
            var nhanVien = await _service.GetIdNhanVien(id);
            if(nhanVien == null)
            {
                return NotFound();
            }
            return Ok(nhanVien);
        }
        [HttpPost("CreateNhanVien")]
        public async Task<ActionResult<NhanVien>> CreateNhanVien(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdNhanVien = await _service.CreateNV(nhanVien);
                return CreatedAtAction(nameof(GetIdNhanVien), new { id = createdNhanVien.IdNhanVien }, createdNhanVien);
            }
            catch (Exception ex)
            {
                // Logger.LogError(ex, "Error creating NhanVien");
                return BadRequest($"An error occurred while creating the employee: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginKH([FromBody] NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kh = await _service.Login(nv.Email, nv.MatKhau);
                    return Ok(kh);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }
		[HttpPut("Sua")]
		public IActionResult Put(NhanVien nv)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_service.UpdateNV(nv);
				return Ok(nv);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
        [HttpPut("SuaChoNhanVien")]
        public IActionResult NhanVienEdit(NhanVien nv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _service.UpdateThongTin(nv);
                return Ok(nv);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // DELETE: api/DanhMuc/Xoa?id=<guid>
        [HttpDelete("Xoa")]
		public IActionResult Delete(Guid id)
		{
			try
			{
				_service.DeleteDM(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(Guid id, string newPassword, string confirmPassword)
        {
            if(newPassword != confirmPassword)
            {
                return BadRequest(new { message = "Mật Khẩu xác nhân không khớp." });

            }
            var result = await _service.DoiMK(id, newPassword);
            if(result)
            {
                return Ok(new { message = "Đổi mật khẩu thành công." });
            }
            return BadRequest(new { message = "Đổi mật khẩu thất bại." });
        }
        //RestPass
        [HttpPost("RestPassword")]
        public async Task<IActionResult> RestPassword(string email,  string newPassword, string confirmPassword)
        {
            if(newPassword != confirmPassword)
            {
                return BadRequest(new { message = "Mật Khẩu xác nhân không khớp." });
            }
            var result = await _service.ResetPass(email, newPassword);
            if(result)
            {
                return Ok(new { message = "Đổi Mật Khẩu Thành Công." });
            }
            return BadRequest(new { message = "Đổi mật khẩu thất bại." });
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
            catch (Exception ex)
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
        [HttpGet("SearchNhanVien")]
        public async Task<IActionResult> SearchNhanVien(string? name)
        {
            try
            {
                var sanPham = await _service.SearchNhanVien(name); // Await the async method
                return Ok(sanPham);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
