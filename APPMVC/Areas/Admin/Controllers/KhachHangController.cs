using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using X.PagedList;
using AppData.ViewModel;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachHangController : Controller
    {
        private readonly IKhachHangService _service;
        private readonly IDiaChiService _diaChiService;
        public KhachHangController(IKhachHangService service, IDiaChiService diaChiService)
        {
            _service = service;
            _diaChiService = diaChiService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;

            // Lấy tất cả khách hàng
            var khachHangs = await _service.GetAllKhachHang();

            // Lọc khách hàng có trạng thái = 1
            var filteredKhachHangs = khachHangs
                .Where(kh => kh.KichHoat == 1) // Giả sử TrangThai là thuộc tính của KhachHang
                .ToList();

            var khachHangViewModels = new List<KhachHangViewModel>();

            foreach (var khachHang in filteredKhachHangs)
            {
                var diaChiList = await _diaChiService.GetAllAsync(khachHang.IdKhachHang);
                var diaChi = diaChiList?.FirstOrDefault();

                khachHangViewModels.Add(new KhachHangViewModel
                {
                    KhachHang = khachHang,
                    DiaChi = diaChi?.WardName
                });
            }

            var sortedKhachHangViewModels = khachHangViewModels
                .OrderByDescending(k => k.KhachHang.NgayTao) // Sắp xếp theo NgayTao
                .ToList();

            var pagedKhachHangViewModels = sortedKhachHangViewModels.ToPagedList(page, pageSize);
            return View(pagedKhachHangViewModels);
        }

        public async Task<IActionResult> SearchKhachHang(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;

            // Lấy tất cả khách hàng nếu name là null hoặc rỗng
            IEnumerable<KhachHang> khachHangs;

            if (string.IsNullOrWhiteSpace(name))
            {
                // Nếu không có tên tìm kiếm, lấy tất cả khách hàng
                khachHangs = await _service.GetAllKhachHang();
            }
            else
            {
                // Gọi dịch vụ để tìm kiếm khách hàng theo tên
                khachHangs = await _service.SearchKhachHang(name);
            }

            // Lọc khách hàng có trạng thái = 1
            var filteredKhachHangs = khachHangs
                .Where(kh => kh.KichHoat == 1) // Giả sử KichHoat là thuộc tính của KhachHang
                .ToList();

            var khachHangViewModels = new List<KhachHangViewModel>();

            foreach (var khachHang in filteredKhachHangs)
            {
                var diaChiList = await _diaChiService.GetAllAsync(khachHang.IdKhachHang);
                var diaChi = diaChiList?.FirstOrDefault();

                khachHangViewModels.Add(new KhachHangViewModel
                {
                    KhachHang = khachHang,
                    DiaChi = diaChi?.WardName
                });
            }

            // Sắp xếp theo NgayTao
            var sortedKhachHangViewModels = khachHangViewModels
                .OrderByDescending(k => k.KhachHang.NgayTao)
                .ToList();

            // Phân trang
            var pagedKhachHangViewModels = sortedKhachHangViewModels.ToPagedList(page, pageSize);
            return View("Index",pagedKhachHangViewModels);
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhachHang kh)
        {
            // Kiểm tra các trường thông tin bắt buộc
            if (string.IsNullOrWhiteSpace(kh.HoTen))
            {
                TempData["ErrorKH"] = "Đăng ký thất bại! Họ tên không được để trống.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(kh.SoDienThoai))
            {
                TempData["ErrorKH"] = "Đăng ký thất bại! Số điện thoại không được để trống.";
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(kh.Email))
            {
                TempData["ErrorKH"] = "Đăng ký thất bại! Email không được để trống.";
                return RedirectToAction("Index");
            }

            var checkSdt = await _service.CheckSDT(kh.SoDienThoai);
            if (checkSdt)
            {
                TempData["ErrorKH"] = "Đăng ký thất bại! Số điện thoại đã tồn tại.";
                return RedirectToAction("Index");
            }

            var checkEmail = await _service.CheckMail(kh.Email);
            if (checkEmail)
            {
                TempData["ErrorKH"] = "Đăng ký thất bại! Email này đã tồn tại.";
                return RedirectToAction("Index");
            }

            kh.MatKhau = "123456";

            if (ModelState.IsValid)
            {
                await _service.AddKhachHang(kh);
                return RedirectToAction("Index");
            }

            return View(kh);
        }
        public async Task<IActionResult> Edit(Guid id)
        {

            var kh = await _service.GetIdKhachHang(id);
            if(kh == null)
            {
                return NotFound();
            }
            return View(kh);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(KhachHang kh)
        {
            var currentKhachHang = await _service.GetIdKhachHang(kh.IdKhachHang);

            // Kiểm tra số điện thoại
            if (currentKhachHang.SoDienThoai != kh.SoDienThoai)
            {
                var checkSdt = await _service.CheckSDT(kh.SoDienThoai);
                if (checkSdt)
                {
                    TempData["ErrorKH"] = "Sửa thất bại! Số điện thoại đã tồn tại.";
                    return RedirectToAction("Index");
                }
            }

            // Kiểm tra email
            if (currentKhachHang.Email != kh.Email)
            {
                var checkEmail = await _service.CheckMail(kh.Email);
                if (checkEmail)
                {
                    TempData["ErrorKH"] = "Sửa thất bại! Email này đã tồn tại.";
                    return RedirectToAction("Index");
                }
            }
            if (ModelState.IsValid)
            {
                await _service.UpdateKhachHang(kh);
                return RedirectToAction("Index");
            }
            return View(kh);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteKhachHang(id);
            return RedirectToAction("Index");
        }

    }
}
