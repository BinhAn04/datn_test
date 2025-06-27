using AppData.Model;
using AppData.ViewModel;
using APPMVC.IService;
using Castle.Core.Internal;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DiaChiController : Controller
	{
		public IDiaChiService _services;
		public IKhachHangService _serviceKH;
		public DiaChiController(IDiaChiService services, IKhachHangService serviceKH)
		{
			_services = services;
			_serviceKH = serviceKH;
		}


		public async Task<IActionResult> GetAll()
		{
			var diaChi = await _services.GetAll();
			return View(diaChi);
		}

		public async Task<IActionResult> Index(Guid idKhachHang)
		{
			var addresses = await _services.GetAllAsync(idKhachHang);
			return View(addresses);
		}
		public async Task<IActionResult> Create()
		{
            var dc = new DiaChi();
			var khachHang = (await _serviceKH.GetAllKhachHang()).Where(kh => kh.KichHoat != 0).ToList();
            ViewBag.khachHang = khachHang;
			ViewBag.Provinces = await _services.GetProvincesAsync();

			//await LoadDropDownsCreate(dc);

			return View(dc);
		}
		[HttpPost]
		public async Task<IActionResult> Create(DiaChi dc)
		{
			//Check số lượng địa chỉ
			int addressCount = await _services.GetAddressCountByCustomerId(dc.IdKhachHang);
			if (addressCount >= 3)
			{
				ModelState.AddModelError("", "Khách hàng này đã có tối đa 3 địa chỉ.");
                ViewBag.Provinces = await _services.GetProvincesAsync();
                await LoadDropDownsCreate(dc);
				ViewBag.khachHang = (await _serviceKH.GetAllKhachHang()).Where(kh => kh.KichHoat != 0).ToList();
					
                return View(dc);
			}
			if (!ModelState.IsValid)
			{
				foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
				{
					Console.WriteLine($"Error: {error.ErrorMessage}");  // In lỗi ra console
				}
                ViewBag.Provinces = await _services.GetProvincesAsync();
                await LoadDropDownsCreate(dc);
                ViewBag.khachHang = (await _serviceKH.GetAllKhachHang()).Where(kh => kh.KichHoat == 1).ToList();
                return View(dc);
			}

			//Nếu tất cả ok thì thêm =))
			bool success = await _services.AddAsync(dc);
			if (success)
			{
				return RedirectToAction("GetAll");
			}
			await LoadDropDownsCreate(dc);
			return View(dc);
		}
		//Edit 
		public async Task<IActionResult> EditDC(Guid IdDiaChi)
		{
			var diaChi = await _services.GetByIdAsync(IdDiaChi);
			await LoadDropDowns(diaChi);
			if (diaChi == null) return NotFound();
			ViewBag.HasDefaultAddress = await _services.HasDefaultAddressAsync(diaChi.IdKhachHang) && !diaChi.DiaChiMacDinh;

			await LoadDropDowns(diaChi);
			return View(diaChi);
		}
		[HttpPost]
		public async Task<IActionResult> EditDC(Guid IdDiaChi, DiaChi dc)
		{
			if (dc.WardId == null)
			{
				ModelState.AddModelError("WardId", "Vui lòng chọn phường/xã.");
				await LoadDropDowns(dc);  // Tải lại các dropdown
				return View(dc);  // Trả về lại view với thông báo lỗi
			}
			if (IdDiaChi == Guid.Empty)
			{
				ModelState.AddModelError("", "ID địa chỉ không hợp lệ.");
				await LoadDropDowns(dc);
				return View(dc);
			}


			if (!ModelState.IsValid)
			{
				await LoadDropDowns(dc);
				return View(dc);
			}

			bool success = await _services.UpdateAsync(IdDiaChi, dc);
			if (success)
			{
				return RedirectToAction("GetAll");
			}

			await LoadDropDowns(dc);
			return View(dc);
		}
		//Xóa
		public async Task<IActionResult> Delete(Guid idDiaChi)
		{
			await _services.DeleteAsync(idDiaChi);
			return RedirectToAction("GetAll");
		}

		[HttpGet]
		public async Task<IActionResult> GetDistricts(int provinceId)
		{
			if (provinceId == 0)
			{
				// Nếu không có provinceId hợp lệ, trả về danh sách trống
				return Json(new { error = "Invalid provinceId" });
			}
			var districts = await _services.GetDistrictsAsync(provinceId);
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
			var wards = await _services.GetWardsAsync(districtId);
			var wardList = wards.Select(w => new {
				WardId = w.WardId.ToString(),
				WardName = w.WardName
			}).ToList();

			return Json(wardList);
		}
		[HttpGet]
		public async Task<IActionResult> GetAddressCountByCustomerId(Guid customerId)
		{
			int count = await _services.GetAddressCountByCustomerId(customerId);
			return Json(count);
		}
		//Hàm Phụ
		private async Task LoadDropDowns(DiaChi dc)
		{
			// Lấy danh sách tỉnh từ DB
			ViewBag.Provinces = await _services.GetProvincesAsync();

			// Nếu ProvinceId có giá trị hợp lệ, lấy danh sách các quận theo ProvinceId
			if (dc != null && dc.ProvinceId > 0)
			{
				ViewBag.Districts = await _services.GetDistrictsAsync(dc.ProvinceId);
			}
			else
			{
				ViewBag.Districts = new List<District>(); // Danh sách trống nếu dc không có ProvinceId
			}

			// Nếu DistrictId có giá trị hợp lệ, lấy danh sách các phường theo DistrictId
			if (dc != null && dc.DistrictId > 0)
			{
				ViewBag.Wards = await _services.GetWardsAsync(dc.DistrictId);
			}
			else
			{
				ViewBag.Wards = new List<Ward>(); // Danh sách trống nếu dc không có DistrictId
			}

			//Lấy danh sách tất cả khách hàng
			ViewBag.khachHang = await _serviceKH.GetAllKhachHang();
		}
		public async Task LoadDropDownsCreate(DiaChi dc)
		{
            ViewBag.Districts = dc.ProvinceId != null
                ? await _services.GetDistrictsAsync(dc.ProvinceId)
                : new List<District>(); // Danh sách trống nếu chưa chọn tỉnh

            ViewBag.Wards = dc.DistrictId != null
                ? await _services.GetWardsAsync(dc.DistrictId)
                : new List<Ward>(); // Danh sách trống nếu chưa chọn quận
		}

    }
}
