using AppData.Model;
using AppData.Model.Vnpay;
using AppData.ViewModel;
using APPMVC.IService;
using APPMVC.Libraries;
using APPMVC.Service.Vnpay;
using DinkToPdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using static AppData.ViewModel.HoaDonChiTietViewModel;
using static AppData.ViewModel.PromotionViewModel;
using SanPhamChiTietViewModel = AppData.ViewModel.HoaDonChiTietViewModel.SanPhamChiTietViewModel;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanHangTQController : Controller
    {
        private readonly ISanPhamChiTietService _sanPhamCTService;
        private readonly ISanPhamService _sanPhamService;
        private readonly IKhachHangService _khachHangService;
        private readonly IHoaDonChiTietService _hoaDonChiTietService;
        private readonly IHoaDonService _hoaDonService;
        private readonly INhanVienService _nhanVienService;
        private readonly ILichSuHoaDonService _lichSuHoaDonService;
        private readonly IMauSacService _mauSacService;
        private readonly IKichCoService _kichCoService;
        private readonly ISanPhamChiTietMauSacService _sanPhamChiTietMauSacService;
        private readonly ISanPhamChiTietKichCoService _sanPhamChiTietKichCoService;
        private readonly IHinhAnhService _hinhAnhService;
        private readonly IVnPayService _vnPayServie;
        private readonly IConfiguration _configuration;
        private readonly IDiaChiService _services;
        private readonly ILichSuThanhToanService _lichSuThanhToanService;
        private readonly GiaoHangNhanhService _giaoHangNhanhService;
        private readonly IPromotionSanPhamChiTietService _promotionSanPhamChiTietService;
        private readonly IPromotionService _promotionService;

        public BanHangTQController(
            ISanPhamChiTietService sanPhamChiTietService,
            ISanPhamService sanPhamService,
            IKhachHangService khachHangService,
            IHoaDonChiTietService hoaDonChiTietService,
            IHoaDonService hoaDonService,
            INhanVienService nhanVienService,
            ILichSuHoaDonService lichSuHoaDonService,
            IMauSacService mauSacService,
            IKichCoService kichCoService,
            ISanPhamChiTietMauSacService sanPhamChiTietMauSacService,
            ISanPhamChiTietKichCoService sanPhamChiTietKichCoService,
            IHinhAnhService hinhAnhService,
            IVnPayService vnPayServie,
            IConfiguration configuration,
            IDiaChiService services,
            GiaoHangNhanhService giaoHangNhanhService,
            ILichSuThanhToanService lichSuThanhToanService,
            IPromotionSanPhamChiTietService promotionSanPhamChiTietService,
            IPromotionService promotionService
            )
        {
            _sanPhamCTService = sanPhamChiTietService;
            _sanPhamService = sanPhamService;
            _khachHangService = khachHangService;
            _hoaDonChiTietService = hoaDonChiTietService;
            _hoaDonService = hoaDonService;
            _nhanVienService = nhanVienService;
            _lichSuHoaDonService = lichSuHoaDonService;
            _mauSacService = mauSacService;
            _kichCoService = kichCoService;
            _sanPhamChiTietMauSacService = sanPhamChiTietMauSacService;
            _sanPhamChiTietKichCoService = sanPhamChiTietKichCoService;
            _hinhAnhService = hinhAnhService;
            _vnPayServie = vnPayServie;
            _configuration = configuration;
            _services = services;
            _lichSuThanhToanService = lichSuThanhToanService;
            _giaoHangNhanhService = giaoHangNhanhService;
            _promotionSanPhamChiTietService = promotionSanPhamChiTietService;
            _promotionService = promotionService;
        }
        private static SynchronizedConverter _converter = new SynchronizedConverter(new PdfTools());
        public async Task<ActionResult> Index()
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login", "NhanVien");
            }
            var hoaDons = await _hoaDonService.GetAllAsync();

            if (hoaDons == null || !hoaDons.Any())
            {
                return View(new List<HoaDonChiTietViewModel>());
            }

            var filteredOrders = await Task.WhenAll(hoaDons
                .Where(hd => hd.TrangThai == "Tạo đơn hàng")
                .Select(async hd => {
                    var sanPhamChiTiets = new List<HoaDonChiTietViewModel.SanPhamChiTietViewModel>();
                    try
                    {
                        var sanPhamDetails = await _hoaDonChiTietService.GetByIdHoaDonAsync(hd.IdHoaDon);
                        sanPhamChiTiets = sanPhamDetails.Select(sp => new HoaDonChiTietViewModel.SanPhamChiTietViewModel
                        {
                            IdSanPhamChiTiet = sp.IdSanPhamChiTiet,
                        }).ToList();
                    }
                    catch (HttpRequestException ex)
                    {
                        // Ghi lại lỗi hoặc xử lý theo cách bạn muốn
                        Console.WriteLine($"Lỗi khi gọi API: {ex.Message}");
                        // Bạn có thể thực hiện một hành động nào đó nếu không thể lấy sản phẩm chi tiết
                    }
                    return new HoaDonChiTietViewModel
                    {
                        IdHoaDon = hd.IdHoaDon,
                        HoaDon = new HoaDonChiTietViewModel.HoaDonViewModel
                        {
                            MaDon = hd.MaDon
                        },
                        SanPhamChiTiets = sanPhamChiTiets
                    };
                })
                .Take(5)
                .ToList());

            return View(filteredOrders);
        }
        [HttpPost]
        public async Task<IActionResult> TaoHD()
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            var NVIdString = HttpContext.Session.GetString("IdNhanVien");
			var nguoiTao = HttpContext.Session.GetString("NhanVienName");

			if (string.IsNullOrEmpty(NVIdString) || !Guid.TryParse(NVIdString, out Guid NVID))
            {
                return RedirectToAction("Login", "NhanVien");
            }

            var currentOrders = await _hoaDonService.GetAllAsync();
            var countPendingOrders = currentOrders.Count(hd => hd.TrangThai == "Tạo đơn hàng");

            if (countPendingOrders >= 5)
            {
                TempData["ErrorMessage"] = "Không thể tạo thêm đơn hàng. Đã đạt giới hạn tối đa 5 đơn hàng với trạng thái 'Tạo đơn hàng'.";
                return RedirectToAction("Index");
            }

            var order = new HoaDon
            {
                IdHoaDon = Guid.NewGuid(),
                MaDon = await GenerateOrderNumberAsync(),
                NguoiNhan = null,
                SoDienThoaiNguoiNhan = null,
                DiaChiGiaoHang = null,
                LoaiHoaDon = "Tại Quầy",
                TienShip = 0,
                TongTienDonHang = 0,
                TongTienHoaDon = 0,
                NgayTao = DateTime.Now,
                NguoiTao = nguoiTao,
                KichHoat = 1,
                TrangThai = "Tạo đơn hàng",
                IdKhachHang = null,
                IdNhanVien = NVID
            };

            try
            {
                await _hoaDonService.AddAsync(order);

                var lichSu = new LichSuHoaDon
                {
                    IdLichSuHoaDon = Guid.NewGuid(),
                    ThaoTac = order.TrangThai,
                    NgayTao = DateTime.Now,
                    NguoiThaoTac = nguoiTao,
                    TrangThai = "1",
                    IdHoaDon = order.IdHoaDon,
                };

                await _lichSuHoaDonService.AddAsync(lichSu);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xử lý đơn hàng. Vui lòng thử lại.");
                Console.WriteLine(ex.Message);
            }

            return View("Index");
        }
        [HttpGet]
        public IActionResult ThemKhachHang()
        {
            return PartialView("ThemKhachHang"); 
        }
        [HttpPost]
        public async Task<IActionResult> ThemKhachHang(HoaDonChiTietViewModel.KhachHangViewModel model)
        {         
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Vui lòng kiểm tra thông tin đã nhập." });
            }

            var checkSdt = await _khachHangService.CheckSDT(model.SoDienThoai);
            if (checkSdt)
            {
                return Json(new { success = false, message = "Số điện thoại đã tồn tại." });
            }

            var checkEmail = await _khachHangService.CheckMail(model.Email);
            if (checkEmail)
            {
                return Json(new { success = false, message = "Email này đã tồn tại." });
            }

            var khachHang = new KhachHang
            {
                HoTen = model.HoTen,
                SoDienThoai = model.SoDienThoai,
                Email = model.Email,
            };

            try
            {
                await _khachHangService.AddKhachHang(khachHang);
                return Json(new { success = true, customerName = model.HoTen, idKhachHang = khachHang.IdKhachHang });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi thêm khách hàng. Vui lòng thử lại." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> XoaHoaDon(Guid idHoaDon)
        {
            try
            {
                var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
                if (hoaDon == null)
                {
                    return NotFound();
                }

                await _hoaDonService.DeleteAsync(idHoaDon);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa đơn hàng. Vui lòng thử lại.");
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> XoaSanPhamChiTiet(Guid idSanPhamChiTiet)
        {
            try
            {
                await _hoaDonChiTietService.DeleteAsync(idSanPhamChiTiet);
                return RedirectToAction("Index"); // Hoặc trang mà bạn muốn chuyển hướng
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa sản phẩm. Vui lòng thử lại.");
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
        private async Task<string> GenerateOrderNumberAsync()
        {
            var lastOrderNumber = await GetLastOrderNumberAsync();
            int newOrderNumber = lastOrderNumber + 1;
            string newOrderNumberString = $"HD{newOrderNumber}";

            while (await IsOrderNumberExists(newOrderNumberString))
            {
                newOrderNumber++;
                newOrderNumberString = $"HD{newOrderNumber}";
            }

            return newOrderNumberString;
        }

        private async Task<int> GetLastOrderNumberAsync()
        {
            var allOrders = await _hoaDonService.GetAllAsync();
            if (allOrders == null || !allOrders.Any())
            {
                return 0;
            }

            var orderNumbers = new List<int>();
            foreach (var order in allOrders)
            {
                if (order.MaDon.Length > 2 &&
                    int.TryParse(order.MaDon.Substring(2), out int orderNumber))
                {
                    orderNumbers.Add(orderNumber);
                }
            }

            return orderNumbers.Any() ? orderNumbers.Max() : 0;
        }
        private async Task<bool> IsOrderNumberExists(string orderNumber)
        {
            var existingOrder = await _hoaDonService.GetByOrderNumberAsync(orderNumber);
            return existingOrder != null;
        }
        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("ID không hợp lệ.");
            }

            var hoaDon = await _hoaDonService.GetByIdAsync(id);
            if (hoaDon == null)
            {
                return NotFound($"Hóa đơn với ID {id} không tồn tại.");
            }

            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(id) ?? new List<HoaDonChiTiet>();
            var sanPhamChiTietsTasks = hoaDonChiTietList.Select(async hoaDonChiTiet =>
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet);
                if (sanPhamCT != null)
                {
                    var sanPham = await _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);

                    var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var mauSacTenList = mauSacList.Select(ms => ms.TenMauSac).ToList();

                    var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var kichCoTenList = kichCoList.Select(kc => kc.TenKichCo).ToList();

                    var hinhAnhs = await _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);

                    var gia = sanPhamCT.Gia;
                    double? giaDaGiam = null; // Explicitly declare as nullable double
                    double? phanTramGiam = null; // Explicitly declare as nullable double

                    // Check for promotions
                    var promotionId = await _promotionSanPhamChiTietService.GetPromotionsBySanPhamChiTietIdAsync(sanPhamCT.IdSanPhamChiTiet);
                    if (promotionId.HasValue && promotionId.Value != Guid.Empty)
                    {
                        var promotionDetails = await _promotionService.GetPromotionByIdAsync(promotionId.Value);

                        // Check if the promotion is active
                        if (promotionDetails != null && promotionDetails.TrangThai == 1)
                        {
                            // Calculate the discounted price based on the discount amount
                            if (sanPhamCT.GiaGiam > 0) // Only calculate if GiaGiam is greater than 0
                            {
                                giaDaGiam = gia - (gia - sanPhamCT.GiaGiam);

                                if (giaDaGiam.HasValue && giaDaGiam < gia)
                                {
                                    phanTramGiam = Math.Round(((gia - giaDaGiam.Value) / gia) * 100, 2);
                                }
                            }
                        }
                    }

                    return new HoaDonChiTietViewModel.SanPhamChiTietViewModel
                    {
                        IdSanPhamChiTiet = sanPhamCT.IdSanPhamChiTiet,
                        MaSanPham = sanPhamCT.MaSp,
                        Quantity = hoaDonChiTiet.SoLuong,
                        SoLuong = sanPhamCT.SoLuong,
                        Price = gia,
                        ProductName = sanPham?.TenSanPham,
                        MauSac = mauSacTenList,
                        KichCo = kichCoTenList,
                        HinhAnhs = hinhAnhs,
                        IdHoaDonChiTiet = hoaDonChiTiet.IdHoaDonChiTiet,
                        GiaDaGiam = giaDaGiam, // Will be null if promotion is not active or GiaGiam is 0
                        PhanTramGiam = phanTramGiam, // Will be null if promotion is not active
                        KichHoat = sanPhamCT.KichHoat,
                        HoatKich = sanPham.KichHoat
                    };
                }
                return null; // Handle case where sanPhamCT is null
            });

            var sanPhamChiTiets = (await Task.WhenAll(sanPhamChiTietsTasks)).Where(x => x != null).ToList();

            // Calculate total amount
            double tongTienHang = 0;

            foreach (var chiTiet in hoaDonChiTietList)
            {
                var sanPhamChiTiet = sanPhamChiTiets.FirstOrDefault(x => x.IdSanPhamChiTiet == chiTiet.IdSanPhamChiTiet);
                if (sanPhamChiTiet != null)
                {
                    double priceToUse = (sanPhamChiTiet.GiaDaGiam > 0) ? sanPhamChiTiet.GiaDaGiam.Value : sanPhamChiTiet.Price;
                    tongTienHang += chiTiet.SoLuong * priceToUse;

                    double productPriceToCompare = Convert.ToDouble(chiTiet.TienGiam) > 0 ? Convert.ToDouble(chiTiet.TienGiam) : chiTiet.DonGia;

                    if (priceToUse != productPriceToCompare)
                    {
                        chiTiet.TienGiam = Convert.ToDouble(sanPhamChiTiet.GiaDaGiam);
                        chiTiet.TongTien = Math.Round(chiTiet.SoLuong * priceToUse, 2); 

                        await _hoaDonChiTietService.UpdateAsync(new List<HoaDonChiTiet> { chiTiet });
                    }
                }
            }

            var viewModel = new HoaDonChiTietViewModel
            {
                DonGia = tongTienHang,
                GiamGia = hoaDon.TienGiam,
                TongTien = tongTienHang + (hoaDon.TienShip ?? 0),
                PhiVanChuyen = hoaDon.TienShip,
                HoaDon = new HoaDonChiTietViewModel.HoaDonViewModel
                {
                    IdHoaDon = hoaDon.IdHoaDon,
                    MaDon = hoaDon.MaDon,
                    TrangThai = hoaDon.TrangThai,
                    SoDienThoaiNguoiNhan = hoaDon.SoDienThoaiNguoiNhan,
                    LoaiHoaDon = hoaDon.LoaiHoaDon,
                    DiaChiGiaoHang = hoaDon.DiaChiGiaoHang
                },
                SanPhamChiTiets = sanPhamChiTiets
            };

            return PartialView("Edit", viewModel);
        }
        [HttpGet]
        public async Task<ActionResult> GetSanPhamChiTietList(Guid idHoaDon)
        {
            try
            {
                var sanPhamChiTietList = await _sanPhamCTService.GetSanPhamChiTiets();

                if (sanPhamChiTietList == null || !sanPhamChiTietList.Any())
                {
                    return PartialView("~/Areas/Admin/Views/BanHangTQ/ListSanPhamChiTiet.cshtml", (new List<SanPhamChiTietViewModel>(), idHoaDon));
                }

                // Lấy danh sách ID sản phẩm chi tiết
                var sanPhamChiTietIds = sanPhamChiTietList.Select(s => s.IdSanPhamChiTiet).ToList();

                // Gọi song song để lấy tất cả thông tin cần thiết
                var sanPhamTasks = sanPhamChiTietIds.Select(id => _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(id)).ToList();
                var mauSacTasks = sanPhamChiTietIds.Select(id => _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(id)).ToList();
                var kichCoTasks = sanPhamChiTietIds.Select(id => _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(id)).ToList();
                var hinhAnhTasks = sanPhamChiTietIds.Select(id => _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(id)).ToList();
                var promotionTasks = sanPhamChiTietIds.Select(id => _promotionSanPhamChiTietService.GetPromotionsBySanPhamChiTietIdAsync(id)).ToList();

                // Chờ tất cả các tác vụ hoàn thành
                await Task.WhenAll(sanPhamTasks);
                await Task.WhenAll(mauSacTasks);
                await Task.WhenAll(kichCoTasks);
                await Task.WhenAll(hinhAnhTasks);
                await Task.WhenAll(promotionTasks);

                // Tạo danh sách sản phẩm chi tiết view model
                var sanPhamChiTietViewModels = new List<SanPhamChiTietViewModel>();

                for (int i = 0; i < sanPhamChiTietList.Count; i++)
                {
                    var sanPhamCT = sanPhamChiTietList[i];
                    var sanPham = await sanPhamTasks[i];
                    var mauSacList = await mauSacTasks[i];
                    var kichCoList = await kichCoTasks[i];
                    var hinhAnhs = await hinhAnhTasks[i];
                    var promotionId = await promotionTasks[i];

                    double? giaDaGiam = sanPhamCT.GiaGiam;
                    double? phanTramGiam = null;

                    // Tính giá đã giảm nếu có khuyến mãi
                    if (promotionId.HasValue && promotionId.Value != Guid.Empty)
                    {
                        var promotionDetails = await _promotionService.GetPromotionByIdAsync(promotionId.Value);
                        if (promotionDetails != null && promotionDetails.TrangThai == 1 && promotionDetails.PhanTramGiam > 0 && giaDaGiam > 0)
                        {
                            giaDaGiam = giaDaGiam;
                        }
                    }

                    // Nếu không có giá đã giảm, sử dụng giá gốc
                    if (!giaDaGiam.HasValue || giaDaGiam == 0)
                    {
                        giaDaGiam = sanPhamCT.Gia;
                    }

                    // Tính phần trăm giảm giá
                    if (giaDaGiam.HasValue && giaDaGiam < sanPhamCT.Gia)
                    {
                        phanTramGiam = Math.Round(((sanPhamCT.Gia - giaDaGiam.Value) / sanPhamCT.Gia) * 100, 2);
                    }

                    sanPhamChiTietViewModels.Add(new SanPhamChiTietViewModel
                    {
                        IdSanPhamChiTiet = sanPhamCT.IdSanPhamChiTiet,
                        MaSanPham = sanPhamCT.MaSp,
                        Quantity = sanPhamCT.SoLuong,
                        Price = sanPhamCT.Gia,
                        ProductName = sanPham?.TenSanPham,
                        MauSac = mauSacList.Select(ms => ms.TenMauSac).ToList(),
                        KichCo = kichCoList.Select(kc => kc.TenKichCo).ToList(),
                        HinhAnhs = hinhAnhs,
                        GiaDaGiam = giaDaGiam,
                        PhanTramGiam = phanTramGiam,
                        KichHoat = sanPhamCT.KichHoat,
                        HoatKich = sanPham.KichHoat
                    });
                }

                // Sắp xếp theo mã sản phẩm giảm dần
                // Sắp xếp theo phần số trong mã sản phẩm giảm dần
                sanPhamChiTietViewModels = sanPhamChiTietViewModels
                    .OrderByDescending(x =>
                    {
                        var maSanPham = x.MaSanPham;

                        // Tách phần số từ mã sản phẩm, bỏ qua chữ
                        var numberPart = new string(maSanPham.SkipWhile(c => !char.IsDigit(c)).ToArray()); // Lấy phần số
                        int number = int.TryParse(numberPart, out int num) ? num : 0; // Chuyển đổi thành số nguyên

                        return number; // Trả về số để sắp xếp
                    })
                    .ToList();

                return PartialView("~/Areas/Admin/Views/BanHangTQ/ListSanPhamChiTiet.cshtml", (sanPhamChiTietViewModels, idHoaDon));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSanPhamChiTietList: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi lấy danh sách sản phẩm chi tiết." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateSanPhamChiTiet(Guid IdSanPhamChiTiet, Guid IdHoaDon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var sanPhamChiTietTask = _sanPhamCTService.GetSanPhamChiTietById(IdSanPhamChiTiet);
                var existingChiTietTask = _hoaDonChiTietService.GetByIdAndProduct(IdHoaDon, IdSanPhamChiTiet);

                await Task.WhenAll(sanPhamChiTietTask, existingChiTietTask);

                var sanPhamChiTiet = await sanPhamChiTietTask;
                var existingChiTiet = await existingChiTietTask;

                if (sanPhamChiTiet == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại." });
                }

                double? giaDaGiam = sanPhamChiTiet.GiaGiam;
                double gia = sanPhamChiTiet.Gia;

                // Đây là số lượng được yêu cầu từ client, giả định là 1 ở đây
                int requestedQuantity = 1;
                if (requestedQuantity <= 0)
                {
                    return Json(new { success = false, message = "Số lượng phải lớn hơn 0." });
                }

                if (existingChiTiet != null)
                {
                    double newQuantity = existingChiTiet.SoLuong + requestedQuantity;

                    if (newQuantity > sanPhamChiTiet.SoLuong)
                    {
                        return Json(new { success = false, message = "Số lượng yêu cầu vượt quá số lượng có sẵn." });
                    }

                    existingChiTiet.SoLuong = newQuantity;
                    existingChiTiet.TongTien = CalculateTotalPrice(gia, giaDaGiam, newQuantity);
                    await _hoaDonChiTietService.UpdateAsync(new List<HoaDonChiTiet> { existingChiTiet });
                }
                else
                {
                    if (requestedQuantity > sanPhamChiTiet.SoLuong)
                    {
                        return Json(new { success = false, message = "Số lượng yêu cầu vượt quá số lượng có sẵn." });
                    }

                    var hoaDonChiTiet = new HoaDonChiTiet
                    {
                        IdHoaDonChiTiet = Guid.NewGuid(),
                        IdHoaDon = IdHoaDon,
                        IdSanPhamChiTiet = sanPhamChiTiet.IdSanPhamChiTiet,
                        DonGia = gia,
                        SoLuong = requestedQuantity,
                        TongTien = CalculateTotalPrice(gia, giaDaGiam, requestedQuantity),
                        KichHoat = 1,
                        TienGiam = Convert.ToDouble(giaDaGiam)
                    };

                    await _hoaDonChiTietService.AddAsync(new List<HoaDonChiTiet> { hoaDonChiTiet });
                }

                var updatedProductList = await GetSanPhamChiTietList(IdHoaDon);
                return Json(new { success = true, message = "Cập nhật số lượng sản phẩm thành công.", html = updatedProductList });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateSanPhamChiTiet: {ex.Message}");
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi thêm sản phẩm vào hóa đơn." });
            }
        }

        // Hàm để tính tổng tiền
        private double CalculateTotalPrice(double gia, double? giaDaGiam, double quantity)
        {
            return (giaDaGiam.HasValue && giaDaGiam > 0)
                ? Math.Round(giaDaGiam.Value * quantity, 2)
                : Math.Round(gia * quantity, 2);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInvoiceDetails([FromBody] HoaDonChiTietViewModel model)
        {
            if (model == null || model.IdHoaDon == Guid.Empty)
            {
                return Json(new { success = false, message = "Dữ liệu hóa đơn không hợp lệ." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors });
            }

            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(model.IdHoaDon);
            var originalQuantities = new Dictionary<Guid, double>(); 

            foreach (var item in hoaDonChiTietList)
            {
                var updatedItem = model.SanPhamChiTiets.FirstOrDefault(x => x.IdSanPhamChiTiet == item.IdSanPhamChiTiet);
                if (updatedItem != null)
                {
                    // Store original quantity before change
                    originalQuantities[item.IdSanPhamChiTiet] = item.SoLuong;

                    var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(item.IdSanPhamChiTiet);
                    if (sanPhamCT == null)
                    {
                        return Json(new { success = false, message = "Chi tiết sản phẩm không tồn tại." });
                    }

                    // Check available quantity
                    var availableQuantity = sanPhamCT.SoLuong;
                    if (updatedItem.Quantity > availableQuantity)
                    {
                        // Revert to original quantity
                        item.SoLuong = originalQuantities[item.IdSanPhamChiTiet];
                        await _hoaDonChiTietService.UpdateAsync(hoaDonChiTietList);

                        return Json(new { success = false, message = $"Số lượng không được vượt quá {availableQuantity} cho sản phẩm ID {item.IdSanPhamChiTiet}." });
                    }

                    item.SoLuong = updatedItem.Quantity;
                    double priceToUse = Convert.ToDouble(sanPhamCT.GiaGiam) > 0 ? Convert.ToDouble(sanPhamCT.GiaGiam) : sanPhamCT.Gia;
                    item.TongTien = item.SoLuong * priceToUse;
                }
            }

            await _hoaDonChiTietService.UpdateAsync(hoaDonChiTietList);
            return Json(new { success = true });
        }

        public async Task<IActionResult> XacNhanThanhToan(Guid idHoaDon, Guid? idKhachHang, double? soTienKhachDua)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });
            }

            var NVIdString = HttpContext.Session.GetString("IdNhanVien");
            if (string.IsNullOrEmpty(NVIdString) || !Guid.TryParse(NVIdString, out Guid NVID))
            {
                return Unauthorized(new { message = "Nhân viên không tồn tại trong phiên làm việc." });
            }

            var TenNV = HttpContext.Session.GetString("NhanVienName");
            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
            if (hoaDonChiTietList.Count == 0)
            {
                TempData["ErrorMessage"] = "Danh sách sản phẩm không được để trống.";
                return RedirectToAction("Index");
            }

            // Fetch all necessary product details in parallel
            var sanPhamCTTasks = hoaDonChiTietList.Select(hoaDonChiTiet =>
                _sanPhamCTService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet)).ToList();

            var sanPhamCTList = await Task.WhenAll(sanPhamCTTasks);

            double tongTienHang = 0;

            // Fetch all additional details in parallel
            var sanPhamTasks = hoaDonChiTietList.Select(hoaDonChiTiet =>
                _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet)).ToList();

            var mauSacTasks = hoaDonChiTietList.Select(hoaDonChiTiet =>
                _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(hoaDonChiTiet.IdSanPhamChiTiet)).ToList();

            var kichCoTasks = hoaDonChiTietList.Select(hoaDonChiTiet =>
                _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(hoaDonChiTiet.IdSanPhamChiTiet)).ToList();

            var (sanPhamList, mauSacList, kichCoList) = (await Task.WhenAll(sanPhamTasks), await Task.WhenAll(mauSacTasks), await Task.WhenAll(kichCoTasks));

            for (int i = 0; i < hoaDonChiTietList.Count; i++)
            {
                var hoaDonChiTiet = hoaDonChiTietList[i];
                var sanPhamCT = sanPhamCTList[i];
                var sanPham = sanPhamList[i];
                var mauSac = mauSacList[i];
                var kichCo = kichCoList[i];

                // Process each item
                if (sanPhamCT != null)
                {
                    var kichCoTen = string.Join(", ", kichCo.Select(kc => kc.TenKichCo));
                    var mauSacTen = string.Join(", ", mauSac.Select(ms => ms.TenMauSac));

                    if (sanPham == null || sanPhamCT.KichHoat == 0 || sanPham.KichHoat == 0)
                    {
                        TempData["ErrorMessage"] = $"Sản phẩm {sanPham?.TenSanPham} ({mauSacTen} + {kichCoTen}) không còn hoạt động.";
                        return RedirectToAction("Index");
                    }

                    if (sanPhamCT.SoLuong < hoaDonChiTiet.SoLuong)
                    {
                        TempData["ErrorMessage"] = $"Không đủ số lượng cho sản phẩm {sanPham.TenSanPham} ({mauSacTen} + {kichCoTen}).";
                        return RedirectToAction("Index");
                    }

                    double priceToUse = hoaDonChiTiet.TienGiam > 0 ? hoaDonChiTiet.TienGiam : hoaDonChiTiet.DonGia;
                    double productPriceToCompare = Convert.ToDouble(sanPhamCT.GiaGiam) > 0 ? Convert.ToDouble(sanPhamCT.GiaGiam) : sanPhamCT.Gia;

                    if (priceToUse != productPriceToCompare)
                    {
                        hoaDonChiTiet.TienGiam = Convert.ToDouble(sanPhamCT.GiaGiam);
                        hoaDonChiTiet.TongTien = Math.Round(hoaDonChiTiet.SoLuong * sanPhamCT.Gia, 2);

                        await _hoaDonChiTietService.UpdateAsync(new List<HoaDonChiTiet> { hoaDonChiTiet });
                        TempData["ErrorMessage"] = $"Khuyến mãi của sản phẩm '{sanPham.TenSanPham}' ({mauSacTen} + {kichCoTen}) đã được cập nhật.";
                        return RedirectToAction("Index");
                    }

                    double thanhTien = hoaDonChiTiet.SoLuong * priceToUse;
                    tongTienHang += thanhTien;

                    sanPhamCT.SoLuong -= hoaDonChiTiet.SoLuong;
                    await _sanPhamCTService.Update(sanPhamCT);
                }
            }

            var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
            if (hoaDon == null)
            {
                return NotFound(new { message = "Hóa đơn không tồn tại." });
            }

            // Set customer details
            if (idKhachHang.HasValue)
            {
                var khachHang = await _khachHangService.GetIdKhachHang(idKhachHang.Value);
                hoaDon.NguoiNhan = khachHang?.HoTen ?? "Khách Lẻ";
                hoaDon.SoDienThoaiNguoiNhan = khachHang?.SoDienThoai ?? null;
            }
            else
            {
                hoaDon.NguoiNhan = "Khách Lẻ";
            }

            hoaDon.TienGiam = 0;
            hoaDon.TongTienDonHang = tongTienHang;
            hoaDon.TongTienHoaDon = tongTienHang;
            hoaDon.TrangThai = "Hoàn Thành";

            try
            {
                await _hoaDonService.UpdateAsync(hoaDon);

                var lichSu = new LichSuHoaDon
                {
                    IdLichSuHoaDon = Guid.NewGuid(),
                    ThaoTac = hoaDon.TrangThai,
                    NgayTao = DateTime.Now,
                    NguoiThaoTac = "Nhân Viên",
                    TrangThai = "1",
                    IdHoaDon = hoaDon.IdHoaDon,
                };

                await _lichSuHoaDonService.AddAsync(lichSu);

                var tienThua = soTienKhachDua.Value - hoaDon.TongTienHoaDon;

                var lichSuThanhToan = new LichSuThanhToan
                {
                    IdLichSuThanhToan = Guid.NewGuid(),
                    SoTien = soTienKhachDua.Value,
                    TienThua = tienThua,
                    NgayTao = DateTime.Now,
                    LoaiGiaoDich = "Thanh Toán",
                    Pttt = "Tiền mặt",
                    NguoiThaoTac = TenNV,
                    TrangThai = "Đã thanh toán",
                    IdHoaDon = hoaDon.IdHoaDon,
                    IdNhanVien = NVID
                };

                await _lichSuThanhToanService.AddAsync(lichSuThanhToan);

                // Pass idKhachHang to the GenerateInvoiceHtmlFromTemplate method
                var htmlContent = await GenerateInvoiceHtmlFromTemplate(hoaDon, hoaDonChiTietList, idKhachHang);
                var pdfBytes = GeneratePdfFromHtml(htmlContent);

                var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TempFiles");
                if (!Directory.Exists(tempFilePath))
                {
                    Directory.CreateDirectory(tempFilePath);
                }

                var fileName = $"HoaDon_{hoaDon.IdHoaDon}.pdf";
                var fullPath = Path.Combine(tempFilePath, fileName);
                await System.IO.File.WriteAllBytesAsync(fullPath, pdfBytes);
                var fileUrl = $"/TempFiles/{fileName}";

                TempData["SuccessMessage"] = "Thanh toán thành công! Bạn có muốn in hóa đơn không?";
                TempData["FileUrl"] = fileUrl;
                return RedirectToAction("ShowInvoiceMessage");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xác nhận thanh toán." });
            }
        }
        public IActionResult ShowInvoiceMessage()
        {
            return View();
        }

        private async Task<string> GenerateInvoiceHtmlFromTemplate(HoaDon hoaDon, List<HoaDonChiTiet> hoaDonChiTietList, Guid? idKhachHang)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Areas", "Admin", "Templates", "InvoiceTemplate.html");

            if (!System.IO.File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Template file not found: {templatePath}");
            }

            string htmlTemplate = await System.IO.File.ReadAllTextAsync(templatePath);
            string chiTietHoaDonHtml = "";

            string tenNhanVien = HttpContext.Session.GetString("NhanVienName") ?? "Nhân Viên";
            string tenKhachHang = "Khách Lẻ";

            if (idKhachHang.HasValue)
            {
                var khachHang = await _khachHangService.GetIdKhachHang(idKhachHang.Value);
                if (khachHang != null)
                {
                    tenKhachHang = khachHang.HoTen;
                }
            }

            // Prepare to fetch all product details in parallel
            var productTasks = hoaDonChiTietList.Select(async chiTiet =>
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(chiTiet.IdSanPhamChiTiet);
                if (sanPhamCT != null)
                {
                    var idSanPham = sanPhamCT.IdSanPham;
                    var sanPham = await _sanPhamService.GetSanPhamById(idSanPham);

                    // Fetch colors and sizes in parallel
                    var mauSacTask = _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(chiTiet.IdSanPhamChiTiet);
                    var kichCoTask = _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(chiTiet.IdSanPhamChiTiet);

                    var mauSacList = await mauSacTask;
                    var kichCoList = await kichCoTask;

                    // Determine color and size
                    string tenMauSac = mauSacList?.Count > 0 ? string.Join(",", mauSacList.Select(m => m.TenMauSac)) : "Không xác định";
                    string kichCo = kichCoList?.Count > 0 ? string.Join(",", kichCoList.Select(m => m.TenKichCo)) : "Không xác định";

                    // Get the price, considering any discounts
                    double donGia = sanPhamCT.Gia; // Assume this is the base price

                    // Check if there is a discount and if it is greater than zero
                    if (sanPhamCT.GiaGiam.HasValue && sanPhamCT.GiaGiam.Value > 0)
                    {
                        donGia = sanPhamCT.GiaGiam.Value; // Use the discounted price if valid
                    }

                    return $@"
<tr>
    <td>{sanPham.TenSanPham}</td>  
    <td>{tenMauSac}</td>
    <td>{kichCo}</td>
    <td>{chiTiet.SoLuong}</td>
    <td>{donGia.ToString("N0")} VND</td>
    <td>{(chiTiet.SoLuong * donGia).ToString("N0")} VND</td>
</tr>";
                }
                return string.Empty;
            });


            var results = await Task.WhenAll(productTasks);
            chiTietHoaDonHtml = string.Join(string.Empty, results);

            // Generate final HTML
            string finalHtml = htmlTemplate
                .Replace("{{MaDon}}", hoaDon.MaDon.ToString())
                .Replace("{{NgayTao}}", hoaDon.NgayTao.ToString("dd/MM/yyyy HH:mm"))
                .Replace("{{TenNhanVien}}", tenNhanVien)
                .Replace("{{TenKhachHang}}", tenKhachHang)
                .Replace("{{ChiTietHoaDon}}", chiTietHoaDonHtml)
                .Replace("{{TongTien}}", hoaDon.TongTienHoaDon.ToString("N0"));

            return finalHtml;
        }

        private byte[] GeneratePdfFromHtml(string htmlContent)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    DPI = 300
                }
            };

            doc.Objects.Add(new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = "utf-8"
                }
            });

            return _converter.Convert(doc);
        }

        [HttpGet]
        public async Task<ActionResult> ThanhToanCK(Guid idHoaDon, Guid? idKhachHang)
        {
            var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
            if (hoaDon == null)
            {
                return NotFound(new { message = "Hóa đơn không tồn tại." });
            }

            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
            if (hoaDonChiTietList == null)
            {
                return NotFound(new { message = "Không tìm thấy chi tiết hóa đơn." });
            }

            var sanPhamChiTiets = new List<SanPhamChiTietViewModel>();
            foreach (var chiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(chiTiet.IdSanPhamChiTiet);
                if (sanPhamCT != null)
                {
                    // Lấy giá giảm nếu có
                    double priceToUse = chiTiet.TienGiam > 0 ? chiTiet.TienGiam : sanPhamCT.Gia;

                    sanPhamChiTiets.Add(new SanPhamChiTietViewModel
                    {
                        IdSanPhamChiTiet = sanPhamCT.IdSanPhamChiTiet,
                        MaSanPham = sanPhamCT.MaSp,
                        Quantity = chiTiet.SoLuong,
                        Price = priceToUse,  // Sử dụng giá đã được điều chỉnh
                    });
                }
            }

            var model = new ThanhToanCKViewModel
            {
                SanPhamChiTiets = sanPhamChiTiets,
                IdHoaDon = idHoaDon,
                MaDon = hoaDon.MaDon,
                IdKhachHang = idKhachHang ?? Guid.Empty
            };
            return PartialView("~/Areas/Admin/Views/BanHangTQ/ThanhToanCK.cshtml", model);
        }
        [HttpPost]
        public async Task<ActionResult> XacNhanThanhToanCK(Guid idHoaDon, Guid? idKhachHang)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            var TenNV = HttpContext.Session.GetString("NhanVienName");
            var NVIdString = HttpContext.Session.GetString("IdNhanVien");
            if (string.IsNullOrEmpty(NVIdString) || !Guid.TryParse(NVIdString, out Guid NVID))
            {
                return Json(new { success = false, message = "Nhân viên không tồn tại trong phiên làm việc." });
            }

            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
            if (hoaDonChiTietList.Count == 0)
            {
                return Json(new { success = false, message = "Danh sách sản phẩm không được để trống." });
            }

            double tongTienHang = 0;
            var sanPhamCTTasks = hoaDonChiTietList.Select(hoaDonChiTiet =>
                _sanPhamCTService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet)).ToList();

            var sanPhamCTList = await Task.WhenAll(sanPhamCTTasks);

            foreach (var (hoaDonChiTiet, sanPhamCT) in hoaDonChiTietList.Zip(sanPhamCTList, (detail, product) => (detail, product)))
            {
                if (sanPhamCT != null)
                {
                    // Check activation status
                    var sanPham = await _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);
                    var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(hoaDonChiTiet.IdSanPhamChiTiet);
                    var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(hoaDonChiTiet.IdSanPhamChiTiet);
                    var kichCoTen = string.Join(", ", kichCoList.Select(kc => kc.TenKichCo));
                    var mauSacTen = string.Join(", ", mauSacList.Select(ms => ms.TenMauSac));
                    if (sanPham == null || sanPhamCT.KichHoat == 0 || sanPham.KichHoat == 0)
                    {
                        return Json(new { success = false, message = $"Sản phẩm {sanPham?.TenSanPham}  ({mauSacTen} + {kichCoTen}) không còn hoạt động." });
                    }

                    if (sanPhamCT.SoLuong < hoaDonChiTiet.SoLuong)
                    {
                        return Json(new { success = false, message = $"Không đủ số lượng cho sản phẩm {sanPham.TenSanPham}  ({mauSacTen} + {kichCoTen})." });
                    }

                    // Check if the discounted price matches
                    double priceToUse = Math.Round(hoaDonChiTiet.TienGiam > 0 ? hoaDonChiTiet.TienGiam : hoaDonChiTiet.DonGia, 2);

                    // Determine the correct price to compare based on whether a discount exists
                    double productPriceToCompare = Convert.ToDouble(sanPhamCT.GiaGiam) > 0 ? Convert.ToDouble(sanPhamCT.GiaGiam) : sanPhamCT.Gia;

                    // Check if the prices match
                    if (priceToUse != productPriceToCompare)
                    {
                        hoaDonChiTiet.TienGiam = Convert.ToDouble(sanPhamCT.GiaGiam);
                        hoaDonChiTiet.TongTien = Math.Round(hoaDonChiTiet.SoLuong * sanPhamCT.Gia, 2);

                        await _hoaDonChiTietService.UpdateAsync(new List<HoaDonChiTiet> { hoaDonChiTiet });

                        // Lưu thông báo lỗi vào TempData
                        TempData["ErrorMessage"] = $"Khuyến mãi của sản phẩm '{sanPham.TenSanPham}' ({mauSacTen} + {kichCoTen}) đã được cập nhật.";

                        // Điều hướng đến trang Index
                        return Json(new { success = true, redirectUrl = Url.Action("Index") });
                    }

                    double thanhTien = hoaDonChiTiet.SoLuong * priceToUse;
                    tongTienHang += thanhTien;

                    sanPhamCT.SoLuong -= hoaDonChiTiet.SoLuong;
                    await _sanPhamCTService.Update(sanPhamCT);
                }
            }

            var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
            if (hoaDon == null)
            {
                return Json(new { success = false, message = "Hóa đơn không tồn tại." });
            }

            // Update customer information
            if (idKhachHang.HasValue)
            {
                var khachHang = await _khachHangService.GetIdKhachHang(idKhachHang.Value);
                hoaDon.NguoiNhan = khachHang?.HoTen ?? "Khách Lẻ";
                hoaDon.SoDienThoaiNguoiNhan = khachHang?.SoDienThoai ?? string.Empty;
            }
            else
            {
                hoaDon.NguoiNhan = "Khách Lẻ";
            }

            hoaDon.TienGiam = 0;
            hoaDon.TongTienDonHang = tongTienHang;
            hoaDon.TongTienHoaDon = tongTienHang;
            hoaDon.TrangThai = "Hoàn Thành";

            try
            {
                await _hoaDonService.UpdateAsync(hoaDon);

                var lichSu = new LichSuHoaDon
                {
                    IdLichSuHoaDon = Guid.NewGuid(),
                    ThaoTac = hoaDon.TrangThai,
                    NgayTao = DateTime.Now,
                    NguoiThaoTac = "Nhân Viên",
                    TrangThai = "1",
                    IdHoaDon = hoaDon.IdHoaDon,
                };
                await _lichSuHoaDonService.AddAsync(lichSu);

                var lichSuThanhToan = new LichSuThanhToan
                {
                    IdLichSuThanhToan = Guid.NewGuid(),
                    SoTien = tongTienHang,
                    TienThua = 0,
                    NgayTao = DateTime.Now,
                    LoaiGiaoDich = "Thanh Toán",
                    Pttt = "Chuyển Khoản",
                    NguoiThaoTac = TenNV,
                    TrangThai = "Đã thanh toán",
                    IdHoaDon = hoaDon.IdHoaDon,
                    IdNhanVien = NVID
                };

                await _lichSuThanhToanService.AddAsync(lichSuThanhToan);

                var htmlContent = await GenerateInvoiceHtmlFromTemplate(hoaDon, hoaDonChiTietList, idKhachHang);
                var pdfBytes = GeneratePdfFromHtml(htmlContent);

                var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "TempFiles");
                if (!Directory.Exists(tempFilePath))
                {
                    Directory.CreateDirectory(tempFilePath);
                }

                var fileName = $"HoaDon_{hoaDon.IdHoaDon}.pdf";
                var fullPath = Path.Combine(tempFilePath, fileName);
                await System.IO.File.WriteAllBytesAsync(fullPath, pdfBytes);
                var fileUrl = $"/TempFiles/{fileName}";

                // Store success message and invoice URL in TempData
                TempData["SuccessMessage"] = "Thanh toán thành công! Bạn có muốn in hóa đơn không?";
                TempData["FileUrl"] = fileUrl;

                return Json(new { success = true, redirectUrl = Url.Action("ShowInvoiceMessage") });
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi chi tiết vào log file hoặc hệ thống log (nếu có)
                // Ví dụ: _logger.LogError(ex, "Đã xảy ra lỗi khi xác nhận thanh toán.");

                return Json(new
                {
                    success = false,
                    message = "Khuyến mãi đã hết hạn.",
                    error = ex.Message, // Ghi rõ thông điệp lỗi
                    stackTrace = ex.StackTrace // Ghi rõ stack trace (có thể loại bỏ nếu không cần thiết cho bảo mật)
                });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> ThanhToanVNPay(Guid idHoaDon, Guid? idKhachHang)
        //{
        //    var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
        //    if (hoaDonChiTietList == null)
        //    {
        //        return NotFound(new { message = "Không tìm thấy chi tiết hóa đơn." });
        //    }

        //    double tongTienHang = 0;
        //    foreach (var hoaDonChiTiet in hoaDonChiTietList)
        //    {
        //        var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet);
        //        var sanPham = await _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);
        //        if (sanPhamCT != null)
        //        {
        //            // Check if the product is out of stock
        //            if (sanPhamCT.SoLuong == 0)
        //            {
        //                TempData["ErrorMessage"] = $"Sản phẩm {sanPham.TenSanPham} đã hết hàng.";
        //                return RedirectToAction("Index");
        //            }

        //            // Check if there is enough stock
        //            if (sanPhamCT.SoLuong < hoaDonChiTiet.SoLuong)
        //            {
        //                TempData["ErrorMessage"] = $"Không đủ số lượng cho sản phẩm {sanPham.TenSanPham}.";
        //                return RedirectToAction("Index");
        //            }

        //            double thanhTien = hoaDonChiTiet.SoLuong * hoaDonChiTiet.DonGia;
        //            tongTienHang += thanhTien;
        //        }
        //    }

        //    var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
        //    if (hoaDon == null)
        //    {
        //        return NotFound(new { message = "Không tìm thấy hóa đơn." });
        //    }

        //    hoaDon.TongTienDonHang = tongTienHang;
        //    hoaDon.TongTienHoaDon = tongTienHang;
        //    hoaDon.TienGiam = 0;

        //    if (idKhachHang.HasValue)
        //    {
        //        var khachHang = await _khachHangService.GetIdKhachHang(idKhachHang.Value);
        //        if (khachHang != null)
        //        {
        //            hoaDon.NguoiNhan = khachHang.HoTen;
        //        }
        //        else
        //        {
        //            hoaDon.NguoiNhan = "Khách Lẻ"; // Default if customer not found
        //        }
        //    }
        //    else
        //    {
        //        hoaDon.NguoiNhan = "Khách Lẻ"; // Default for no customer
        //    }

        //    TempData["idHoaDon"] = idHoaDon.ToString();
        //    TempData["TongTienDonHang"] = tongTienHang.ToString();
        //    TempData["TongTienHoaDon"] = tongTienHang.ToString();

        //    var vnPay = new PaymentInformationModel()
        //    {
        //        Amount = tongTienHang,
        //        CreatDate = DateTime.Now,
        //        Description = "Thanh Toán VnPay",
        //        FullName = hoaDon.NguoiNhan, // Use the customer name
        //        OrderId = Guid.NewGuid(),
        //    };

        //    return Redirect(_vnPayServie.CreatePaymentUrl(vnPay, HttpContext));
        //}

        //public IActionResult ThanhToanLoi()
        //{
        //    return View();
        //}

        //public IActionResult ThanhToanThanhCong()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public async Task<IActionResult> ReturnVNPay()
        //{
        //    var response = _vnPayServie.PaymentExecute(Request.Query);
        //    if (response == null || response.VnPayResponseCode != "00")
        //    {
        //        TempData["Message"] = $"Lỗi Thanh Toán: {response.VnPayResponseCode}";
        //        return RedirectToAction("ThanhToanLoi");
        //    }

        //    // Retrieve idHoaDon from TempData
        //    if (TempData.TryGetValue("idHoaDon", out var idHoaDonValue) && Guid.TryParse(idHoaDonValue.ToString(), out Guid idHoaDon))
        //    {
        //        var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon);
        //        if (hoaDon == null)
        //        {
        //            return NotFound(new { message = "Không tìm thấy hóa đơn." });
        //        }

        //        // Retrieve totals from TempData and convert back to double
        //        if (TempData.TryGetValue("TongTienDonHang", out var tongTienDonHangValue) &&
        //            double.TryParse(tongTienDonHangValue.ToString(), out double tongTienDonHang))
        //        {
        //            hoaDon.TongTienDonHang = tongTienDonHang;
        //        }

        //        if (TempData.TryGetValue("TongTienHoaDon", out var tongTienHoaDonValue) &&
        //            double.TryParse(tongTienHoaDonValue.ToString(), out double tongTienHoaDon))
        //        {
        //            hoaDon.TongTienHoaDon = tongTienHoaDon;
        //        }

        //        // Retrieve customer information from TempData
        //        if (TempData.TryGetValue("IdKhachHang", out var idKhachHangValue) &&
        //            Guid.TryParse(idKhachHangValue.ToString(), out Guid idKhachHang))
        //        {
        //            hoaDon.IdKhachHang = idKhachHang;
        //        }

        //        if (TempData.TryGetValue("NguoiNhan", out var nguoiNhanValue))
        //        {
        //            hoaDon.NguoiNhan = nguoiNhanValue.ToString();
        //        }

        //        // Set any additional fields
        //        hoaDon.TienGiam = 0;
        //        hoaDon.TrangThai = "Hoàn Thành";
        //        await _hoaDonService.UpdateAsync(hoaDon);

        //        var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
        //        foreach (var chiTiet in hoaDonChiTietList)
        //        {
        //            var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(chiTiet.IdSanPhamChiTiet);
        //            if (sanPhamCT != null)
        //            {
        //                sanPhamCT.SoLuong -= chiTiet.SoLuong;
        //                await _sanPhamCTService.Update(sanPhamCT);
        //            }
        //        }

        //        // Create a new history entry for the invoice
        //        var lichSu = new LichSuHoaDon
        //        {
        //            IdLichSuHoaDon = Guid.NewGuid(),
        //            ThaoTac = hoaDon.TrangThai,
        //            NgayTao = DateTime.Now,
        //            NguoiThaoTac = "Nhân Viên",
        //            TrangThai = "1",
        //            IdHoaDon = hoaDon.IdHoaDon,
        //        };

        //        await _lichSuHoaDonService.AddAsync(lichSu);

        //        // Directly create a payment history entry
        //        var TenNv = HttpContext.Session.GetString("NhanVienName");
        //        var NVIdString = HttpContext.Session.GetString("IdNhanVien");

        //        if (string.IsNullOrEmpty(NVIdString) || !Guid.TryParse(NVIdString, out Guid NVID))
        //        {
        //            // Handle unauthorized access or log the issue
        //            TempData["Message"] = "Nhân viên không tồn tại trong phiên làm việc.";
        //            return RedirectToAction("ThanhToanLoi");
        //        }

        //        var lichSuThanhToan = new LichSuThanhToan
        //        {
        //            IdLichSuThanhToan = Guid.NewGuid(),
        //            SoTien = hoaDon.TongTienHoaDon,
        //            TienThua = 0,
        //            NgayTao = DateTime.Now,
        //            LoaiGiaoDich = "Thanh Toán VnPay",
        //            Pttt = "VnPay",
        //            NguoiThaoTac = TenNv,
        //            TrangThai = "Đã thanh toán",
        //            IdHoaDon = hoaDon.IdHoaDon,
        //            IdNhanVien = NVID
        //        };

        //        await _lichSuThanhToanService.AddAsync(lichSuThanhToan);

        //        TempData["Message"] = "Thanh toán VnPay Thành công";
        //        return RedirectToAction("ThanhToanThanhCong");
        //    }

        //    return NotFound(new { message = "Không tìm thấy hóa đơn." });
        //}

        [HttpGet]
        public async Task<IActionResult> SearchCustomer(string search, Guid IdHoadon)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Invalid search criteria.");
            }

            var customer = await _khachHangService.GetCustomerByPhoneOrEmailAsync(search);
            if (customer == null)
            {
                return Ok(new
                {
                    customerName = (string)null,
                    idKhachHang = (Guid?)null,
                    idHoaDon = IdHoadon
                });
            }
            return Ok(new
            {
                customerName = customer.HoTen,
                idKhachHang = customer.IdKhachHang,
                idHoaDon = IdHoadon
            });
        }
    }

}