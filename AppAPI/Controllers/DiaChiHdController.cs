using AppAPI.IService;
using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaChiHdController : ControllerBase
    {
        private readonly IDiaChiHdService _service;
        public DiaChiHdController(IDiaChiHdService service)
        {
            _service = service;
        }
        [HttpGet("GetProvinces")]
        public async Task<IActionResult> GetProvinces()
        {
            var getProvinces = await _service.GetProvincesAsync();
            return Ok(getProvinces);
        }
        [HttpGet("GetDistricts/{provinceId}")]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            var getDistricts = await _service.GetDistrictsAsync(provinceId);
            return Ok(getDistricts);
        }
        [HttpGet("GetWards/{districtId}")]
        public async Task<IActionResult> GetWards(int districtId)
        {
            var getWard = await _service.GetWardsAsync(districtId);
            return Ok(getWard);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpGet("GetByIdAsync")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var dc = await _service.GetByIdAsync(id);
                if (dc == null)
                {
                    return NotFound($"DiaChi with ID {id} not found.");
                }
                return Ok(dc);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("them")]
        public async Task<IActionResult> Add(DiaChiHoaDon dc)
        {
            return Ok(await _service.AddAsync(dc));
        }
        [HttpPut("Sua")]
        public async Task<IActionResult> Put(Guid id, DiaChiHoaDon diaChi)
        {
            diaChi.IdDiaChiHoaDon = id;
            return Ok(await _service.UpdateAsync(diaChi));
        }
        [HttpDelete("Xoa")]
        public async Task<IActionResult> Delete(Guid idDiaChi)
        {
            return Ok(await _service.DeleteAsync(idDiaChi));
        }
    }
}
