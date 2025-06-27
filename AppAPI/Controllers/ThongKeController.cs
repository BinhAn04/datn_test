using AppAPI.IService;
using AppData.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeServices _service;
        public ThongKeController(IThongKeServices services)
        {
            _service = services;
        }
        [HttpGet("ThongKeTheoNgay")]
        public async Task<IActionResult> ThongKeTheoNgay([FromQuery] DateTime date) 
        { 
            var result = await _service.GetStatisticsByDate(date);
            
            return Ok(result);
        }
        // Thống kê theo tuần
        [HttpGet("ThongKeTheoTuan")]
        public async Task<IActionResult> ThongKeTheoTuan([FromQuery] DateTime date)
        {
            var result = await _service.GetStatisticsByWeek(date);
            
            return Ok(result);
        }
        // Thống kê theo tháng
        [HttpGet("ThongKeTheoThang")]
        public async Task<IActionResult> ThongKeTheoThang([FromQuery] int year, [FromQuery] int month)
        {
            var result = await _service.GetStatisticsByMonth(year, month);
           
            return Ok(result);
        }


        [HttpGet("ThongKeTheoNam")]
        public async Task<IActionResult> ThongKeTheoNam([FromQuery] int year)
        {
            var result = await _service.GetStatisticsByYear(year);
            return Ok(result);
        }
        [HttpGet("TongQuan")]
        public async Task<IActionResult> TongQuan()
        {
            var result = await _service.GetTotalOrdersAndRevenue();
            return Ok(result);
        }
        [HttpGet("ThongKeTheoKhoangThoiGian")]
        public async Task<IActionResult> ThongKeTheoKhoangThoiGian([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _service.GetStatisticsByTimeRange(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("TopSellingProducts")]
        public async Task<IActionResult> GetTopSellingProducts([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _service.GetTopSellingProductsAsync(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("ThongKeDoanThu")]
        public async Task<IActionResult> GetRevenueStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetRevenueStatisticsAsync(startDate,endDate);
            return Ok(result);
        }
    }
}
