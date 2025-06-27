using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Mvc;

namespace APPMVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class DiaChiHdController : Controller
    {
        private readonly IDiaChiHDService _service;
        public DiaChiHdController(IDiaChiHDService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var diaChi = await _service.GetAll();
            return View(diaChi);

        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.Provinces = await _service.GetProvincesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DiaChiHoaDon dc)
        {
            if(!ModelState.IsValid)
            {
                foreach(var error in ModelState.Values.SelectMany(v => v.Errors)) 
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                ViewBag.Provinces = await _service.GetProvincesAsync();
                await LoadDropDownsCreate(dc);
                return View(dc);               
            }
            bool success = await _service.AddAsync(dc);
            if (success)
            {
                return RedirectToAction("Checkout", "HomeClient");

            }
            await LoadDropDowns(dc);
            return View(dc);
        }
        public async Task<IActionResult> Edit(Guid IdDiaChiHoaDon)
        {
            var diachi = await _service.GetByIdAsync(IdDiaChiHoaDon);
            await LoadDropDowns(diachi);
            if (diachi == null) return NotFound();
            await LoadDropDowns(diachi);
            return View(diachi);
        }
        [HttpGet]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            if (provinceId == 0)
            {
                return Json(new { error = "Invalid provinceId" });
            }
            var districts = await _service.GetDistrictsAsync(provinceId);
            if (districts == null || districts.Count == 0)
            {
                return Json(new { error = "No districts found" });
            }
            var districtList = districts.Select(d => new { DistrictId = d.DistrictId, DistrictName = d.DistrictName }).ToList();
            return Json(districtList);
        }
        [HttpGet]
        public async Task<IActionResult> GetWards(int districtId)
        {
            var wards = await _service.GetWardsAsync(districtId);
            var wardList = wards.Select(w => new {
                WardId = w.WardId.ToString(),
                WardName = w.WardName
            }).ToList();

            return Json(wardList);
        }

        //Hàm Phụ
        private async Task LoadDropDowns(DiaChiHoaDon dc)
        {
            // Lấy danh sách tỉnh từ DB
            ViewBag.Provinces = await _service.GetProvincesAsync();

            // Nếu ProvinceId có giá trị hợp lệ, lấy danh sách các quận theo ProvinceId
            if (dc != null && dc.ProvinceId > 0)
            {
                ViewBag.Districts = await _service.GetDistrictsAsync(dc.ProvinceId);
            }
            else
            {
                ViewBag.Districts = new List<District>(); // Danh sách trống nếu dc không có ProvinceId
            }

            // Nếu DistrictId có giá trị hợp lệ, lấy danh sách các phường theo DistrictId
            if (dc != null && dc.DistrictId > 0)
            {
                ViewBag.Wards = await _service.GetWardsAsync(dc.DistrictId);
            }
            else
            {
                ViewBag.Wards = new List<Ward>(); // Danh sách trống nếu dc không có DistrictId
            }
        }
        public async Task LoadDropDownsCreate(DiaChiHoaDon dc)
        {
            ViewBag.Districts = dc.ProvinceId != null
                ? await _service.GetDistrictsAsync(dc.ProvinceId)
                : new List<District>(); // Danh sách trống nếu chưa chọn tỉnh

            ViewBag.Wards = dc.DistrictId != null
                ? await _service.GetWardsAsync(dc.DistrictId)
                : new List<Ward>(); // Danh sách trống nếu chưa chọn quận
        }
    }
}
