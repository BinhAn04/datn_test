using AppData.Model;
using AppData.ViewModel;
using APPMVC.IService;
using APPMVC.Service;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using static AppData.ViewModel.HoaDonChiTietViewModel;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonController : Controller
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
        private readonly ILichSuThanhToanService _lichSuThanhToanService;

        public HoaDonController(ISanPhamChiTietService sanPhamChiTietService, 
            ISanPhamService sanPhamService, 
            IKhachHangService khachHangService, 
            IHoaDonChiTietService hoaDonChiTietService , 
            IHoaDonService hoaDonService , 
            INhanVienService nhanVienService, 
            ILichSuHoaDonService lichSuHoaDonService, 
            IMauSacService mauSacService,
            IKichCoService kichCoService,
            ISanPhamChiTietMauSacService sanPhamChiTietMauSacService,
            ISanPhamChiTietKichCoService sanPhamChiTietKichCoService,
            IHinhAnhService hinhAnhService,
            ILichSuThanhToanService lichSuThanhToanService)
        {
            _sanPhamCTService = sanPhamChiTietService;
            _sanPhamService = sanPhamService;
            _khachHangService = khachHangService ;
            _hoaDonChiTietService = hoaDonChiTietService;
            _hoaDonService = hoaDonService;
            _nhanVienService = nhanVienService;
            _lichSuHoaDonService = lichSuHoaDonService;
            _mauSacService = mauSacService;
            _kichCoService = kichCoService;
            _sanPhamChiTietMauSacService = sanPhamChiTietMauSacService;
            _sanPhamChiTietKichCoService = sanPhamChiTietKichCoService;
            _hinhAnhService = hinhAnhService;
            _lichSuThanhToanService = lichSuThanhToanService;
        }
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, string status = null, string search = null)
        {
            page = page < 1 ? 1 : page; 
            const int pageSize = 8;

            var hoaDons = await _hoaDonService.GetAllAsync() ?? new List<HoaDon>();

            var filteredHoaDons = hoaDons
                .Where(h => h.TrangThai != "Tạo đơn hàng")
                .ToList();

            if (!string.IsNullOrEmpty(status))
            {
                filteredHoaDons = filteredHoaDons.Where(h => h.TrangThai == status).ToList();
            }
            if (!string.IsNullOrEmpty(search))
            {
                filteredHoaDons = filteredHoaDons
                    .Where(h => h != null &&
                                 (h.MaDon != null && h.MaDon.Contains(search) ||
                                  h.NguoiNhan != null && h.NguoiNhan.Contains(search)))
                    .ToList();
            }

            var distinctStatuses = hoaDons
                .Select(h => h.TrangThai)
                .Where(s => s != "Tạo đơn hàng")
                .Distinct()
                .ToList();

            var hoaDonViewModels = new List<AppData.ViewModel.HoaDonViewModell>();

            // Tạo view models và tính tổng số lượng
            foreach (var hoaDon in filteredHoaDons)
            {
                if (hoaDon != null)
                {
                    var hoaDonChiTiets = await _hoaDonChiTietService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);
                    var totalQuantity = hoaDonChiTiets.Sum(d => d.SoLuong);

                    hoaDonViewModels.Add(new AppData.ViewModel.HoaDonViewModell
                    {
                        HoaDon = hoaDon,
                        TotalQuantity = totalQuantity,
                        HoaDonChiTiets = hoaDonChiTiets
                    });
                }
            }

            // Sắp xếp danh sách view models theo NgayTao giảm dần
            hoaDonViewModels = hoaDonViewModels
                .OrderByDescending(vm => vm.HoaDon.NgayTao)
                .ToList();

            // Phân trang danh sách đã sắp xếp
            var pagedHoaDons = hoaDonViewModels.ToPagedList(page, pageSize);

            // Thiết lập các thuộc tính view bag cho lọc trạng thái và tổng số lượng sản phẩm
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentSearch = search; // Giữ lại giá trị tìm kiếm
            ViewBag.TotalProductQuantity = hoaDonViewModels.Sum(vm => vm.TotalQuantity);
            ViewBag.Statuses = distinctStatuses;

            return View(pagedHoaDons);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(id);
            if (hoaDonChiTietList == null || !hoaDonChiTietList.Any())
            {
                return NotFound($"Hóa đơn chi tiết với ID {id} không tồn tại.");
            }

            var hoaDon = await _hoaDonService.GetByIdAsync(hoaDonChiTietList.First().IdHoaDon);
            if (hoaDon == null)
            {
                return NotFound($"Hóa đơn với ID {hoaDon.IdHoaDon} không tồn tại.");
            }

            var lichSuHoaDons = await _lichSuHoaDonService.GetByIdHoaDonAsync(id);
            var lichSuThanhToans = await _lichSuThanhToanService.GetByIdHoaDonAsync(id);
            var khachhang = await _khachHangService.GetIdKhachHang(hoaDon.IdKhachHang);

            var sanPhamChiTiets = new List<HoaDonChiTietViewModel.SanPhamChiTietViewModel>();
            foreach (var hoaDonChiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet);
                var sanPham = await _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);

                if (sanPhamCT != null)
                {
                    var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var mauSacTenList = mauSacList.Select(ms => ms.TenMauSac).ToList();

                    var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var kichCoTenList = kichCoList.Select(kc => kc.TenKichCo).ToList();

                    var hinhAnhs = await _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);

                    var giaBan = hoaDonChiTiet.DonGia;
                    var giaDaGiam = hoaDonChiTiet.TienGiam;
                    double? phanTramGiam = null;

                    // Calculate discount percentage if there is a discount
                    if (giaDaGiam > 0 && giaDaGiam < giaBan)
                    {
                        phanTramGiam = Math.Round(((giaBan - giaDaGiam) / giaBan) * 100, 2);
                    }
                    else if (giaDaGiam == 0)
                    {
                        // If no discount, use the full selling price
                        giaDaGiam = giaBan; // Set giaDaGiam to giaBan if there's no discount
                    }

                    sanPhamChiTiets.Add(new HoaDonChiTietViewModel.SanPhamChiTietViewModel
                    {
                        IdSanPhamChiTiet = sanPhamCT.IdSanPhamChiTiet,
                        Quantity = hoaDonChiTiet.SoLuong,
                        Price = giaBan,
                        ProductName = sanPham.TenSanPham,
                        MauSac = mauSacTenList,
                        KichCo = kichCoTenList,
                        HinhAnhs = hinhAnhs,
                        GiaDaGiam = giaDaGiam, // Use updated giaDaGiam

                        // Show both original price and discount information
                        OriginalPrice = giaBan, // Add this property to the view model
                        DiscountAmount = giaDaGiam < giaBan ? (giaBan - giaDaGiam) : 0, // Calculate discount amount
                        PhanTramGiam = phanTramGiam
                    });
                }
            }

            var viewModel = new HoaDonChiTietViewModel
            {
                DonGia = hoaDon.TongTienDonHang,
                GiamGia = hoaDon.TienGiam,
                PhiVanChuyen = Convert.ToDouble(hoaDon.TienShip),
                TongTien = hoaDon.TongTienDonHang + Convert.ToDouble(hoaDon.TienShip) - Convert.ToDouble(hoaDon.TienGiam),

                HoaDon = new HoaDonViewModel
                {
                    IdHoaDon = hoaDon.IdHoaDon,
                    MaDon = hoaDon.MaDon,
                    KhachHang = hoaDon.NguoiNhan,
                    TrangThai = hoaDon.TrangThai,
                    SoDienThoaiNguoiNhan = hoaDon.SoDienThoaiNguoiNhan,
                    LoaiHoaDon = hoaDon.LoaiHoaDon,
                    DiaChiGiaoHang = hoaDon.DiaChiGiaoHang
                },

                LichSuHoaDons = lichSuHoaDons.Select(lichSu => new HoaDonChiTietViewModel.LichSuHoaDonViewModel
                {
                    IdLichSuHoaDon = lichSu.IdLichSuHoaDon,
                    ThaoTac = lichSu.ThaoTac,
                    NgayTao = lichSu.NgayTao,
                    NguoiThaoTac = lichSu.NguoiThaoTac,
                    TrangThai = lichSu.TrangThai,
                    IdHoaDon = lichSu.IdHoaDon,
                    GhiChu = lichSu.GhiChu,
                }).ToList(),

                SanPhamChiTiets = sanPhamChiTiets,

                LichSuThanhToans = lichSuThanhToans.Select(payment => new LichSuThanhToanViewModel
                {
                    IdLichSuThanhToan = payment.IdLichSuThanhToan,
                    SoTien = payment.SoTien,
                    NgayThanhToan = payment.NgayTao,
                    LoaiGiaoDich = payment.LoaiGiaoDich,
                    NguoiThaoTac = payment.NguoiThaoTac,
                    HinhThucThanhToan = payment.Pttt,
                    TrangThai = payment.TrangThai,
                    IdHoaDon = payment.IdHoaDon
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HoaDonChiTietViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hoaDonChiTiet = new HoaDonChiTiet
                    {
                        IdHoaDonChiTiet = viewModel.IdHoaDonChiTiet,
                        DonGia = viewModel.DonGia,
                        SoLuong = viewModel.SoLuong,
                        TongTien = viewModel.TongTien,
                        KichHoat = viewModel.KichHoat,
                        IdHoaDon = viewModel.IdHoaDon
                    };

                    //await _hoaDonChiTietService.UpdateAsync(hoaDonChiTiet);
                    TempData["Success"] = "Cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Lỗi: {ex.Message}";
                }
            }

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInvoiceStatus(Guid id, string actionType, string reason)
        {
            var hoaDon = await _hoaDonService.GetByIdAsync(id);
            if (hoaDon == null)
            {
                return NotFound($"Hóa đơn với ID {id} không tồn tại.");
            }

            var lichSu = await _lichSuThanhToanService.GetByIdHoaDonAsync(id);
            bool hasPaymentHistory = lichSu.Any(); // Check if there is any payment history
            string previousState = hoaDon.TrangThai;

            switch (hoaDon.TrangThai)
            {
                case "Chờ Xác Nhận":
                    if (actionType == "huy" && hoaDon.TrangThai != "Đã Hủy")
                    {
                        hoaDon.TrangThai = "Đã Hủy";
                        foreach (var item in lichSu)
                        {
                            item.TrangThai = "Đã Hủy";
                            await _lichSuThanhToanService.UpdateAsync(item);
                        }
                        await UpdateProductQuantities2(hoaDon.IdHoaDon);
                    }
                    else
                    {
                        // If hasPaymentHistory is false, update quantities
                        if (!hasPaymentHistory && hoaDon.TrangThai != "Đã Giao Hàng")
                        {
                            hoaDon.TrangThai = "Chờ Giao Hàng";
                            var errorMessages = await UpdateProductQuantities(hoaDon.IdHoaDon);
                            if (errorMessages.Any())
                            {
                                TempData["ErrorMessage"] = string.Join("<br/>", errorMessages);
                                return RedirectToAction("Edit", new { id });
                            }
                        }
                        // If hasPaymentHistory is true, still set the status but skip quantity update
                        else if (hasPaymentHistory && hoaDon.TrangThai != "Đã Giao Hàng")
                        {
                            hoaDon.TrangThai = "Chờ Giao Hàng";
                        }
                    }
                    break;

                case "Chờ Giao Hàng":
                    if (actionType == "huy" && hoaDon.TrangThai != "Đã Hủy")
                    {
                        hoaDon.TrangThai = "Đã Hủy";
                        
                        await UpdateProductQuantities2(hoaDon.IdHoaDon);
                    }
                    else if (actionType == "quayLai")
                    {
                        return await RevertInvoiceStatus(id, reason, hasPaymentHistory);
                    }
                    else
                    {
                        hoaDon.TrangThai = "Đang Vận Chuyển";
                    }
                    break;

                case "Đang Vận Chuyển":
                    if (actionType == "quayLai")
                    {
                        return BadRequest("Không thể quay lại từ trạng thái 'Đang Vận Chuyển'."); // Disallow reverting
                    }
                    else
                    {
                        hoaDon.TrangThai = "Đã Giao Hàng";
                    }
                    break;

                case "Đã Giao Hàng":
                    if (actionType == "quayLai")
                    {
                        return BadRequest("Không thể quay lại từ trạng thái 'Đã Giao Hàng'.");
                    }
                    else if (hasPaymentHistory)
                    {
                        hoaDon.TrangThai = "Hoàn Thành";
                    }
                    else
                    {
                        return BadRequest("Cần xác nhận thanh toán trước khi hoàn thành hóa đơn.");
                    }
                    break;

                case "Đã Thanh Toán":
                    if (actionType == "quayLai")
                    {
                        return BadRequest("Không thể quay lại từ trạng thái 'Đã Thanh Toán'."); 
                    }
                    else
                    {
                        hoaDon.TrangThai = "Hoàn Thành";
                    }
                    break;

                default:
                    return BadRequest("Trạng thái hóa đơn không hợp lệ.");
            }

            if (hoaDon.TrangThai != previousState)
            {
                await LogHistory(hoaDon, hoaDon.TrangThai, reason);
            }

            await _hoaDonService.UpdateAsync(hoaDon);
            return RedirectToAction("Edit", new { id });
        }

        private async Task<IActionResult> RevertInvoiceStatus(Guid id, string reason, bool hasPaymentHistory)
        {
            var hoaDon = await _hoaDonService.GetByIdAsync(id);
            if (hoaDon == null)
            {
                return NotFound($"Hóa đơn với ID {id} không tồn tại.");
            }

            var lichSu = await _lichSuHoaDonService.GetByIdHoaDonAsync(id);
            if (lichSu == null || lichSu.Count < 2)
            {
                return BadRequest("Không có lịch sử trạng thái trước đó để quay lại.");
            }

            var previousState = lichSu[lichSu.Count - 2].ThaoTac;

            // Update the invoice status to the previous state
            hoaDon.TrangThai = previousState;

            // Only update product quantities if reverting to "Chờ Xác Nhận" and no payment history exists
            if (previousState == "Chờ Xác Nhận" && !hasPaymentHistory)
            {
                await UpdateProductQuantities2(hoaDon.IdHoaDon);
            }

            await LogHistory(hoaDon, $"{previousState}", reason);
            await _hoaDonService.UpdateAsync(hoaDon);

            return RedirectToAction("Edit", new { id });
        }

        private async Task LogHistory(HoaDon hoaDon, string thaoTac, string reason)
        {
            await _lichSuHoaDonService.AddAsync(new LichSuHoaDon
            {
                IdHoaDon = hoaDon.IdHoaDon,
                ThaoTac = thaoTac,
                NgayTao = DateTime.Now,
                NguoiThaoTac = "Nhân Viên",
                TrangThai = hoaDon.TrangThai,
                GhiChu = reason 
            });
        }
        private async Task<List<string>> UpdateProductQuantities(Guid idHoaDon)
        {
            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
            var errorMessages = new List<string>(); // List to collect error messages

            foreach (var chiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(chiTiet.IdSanPhamChiTiet);
                var sanPham = await _sanPhamCTService.GetSanPhamByIdSanPhamChiTietAsync(chiTiet.IdSanPhamChiTiet);
                if (sanPham == null || sanPhamCT.KichHoat == 0 || sanPham.KichHoat == 0)
                {
                    errorMessages.Add($"Sản phẩm {sanPham?.TenSanPham}({sanPhamCT.MaSp}) không còn hoạt động.");
                }
                if (sanPhamCT != null)
                {
                    if (sanPhamCT.SoLuong >= chiTiet.SoLuong)
                    {
                        sanPhamCT.SoLuong -= chiTiet.SoLuong;
                        await _sanPhamCTService.Update(sanPhamCT);
                    }
                    else
                    {
                        // Collect error message for insufficient stock
                        errorMessages.Add($"Không đủ số lượng cho sản phẩm {sanPham.TenSanPham}({sanPhamCT.MaSp}). Số lượng hiện có: {sanPhamCT.SoLuong}, yêu cầu: {chiTiet.SoLuong}.");
                    }
                }
            }

            return errorMessages; // Return the list of error messages
        }
        private async Task UpdateProductQuantities2(Guid idHoaDon)
        {
            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(idHoaDon);
            foreach (var chiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamCTService.GetSanPhamChiTietById(chiTiet.IdSanPhamChiTiet);
                if (sanPhamCT != null)
                {
                    sanPhamCT.SoLuong += chiTiet.SoLuong;
                    await _sanPhamCTService.Update(sanPhamCT);
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateLichSuThanhToan(Guid idHoaDon)
        {
            var hoaDon = await _hoaDonService.GetByIdAsync(idHoaDon); 
            var lichSuThanhToanViewModel = new HoaDonChiTietViewModel
            {
                IdHoaDon = idHoaDon,
                TongTien = hoaDon.TongTienDonHang,
            };
            return PartialView("CreateLichSuThanhToan", lichSuThanhToanViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLichSuThanhToan(LichSuThanhToanViewModel lichSuThanhToan)
        {
            string TenNv = HttpContext.Session.GetString("NhanVienName");
            var NVIdString = HttpContext.Session.GetString("IdNhanVien");

            if (string.IsNullOrEmpty(NVIdString) || !Guid.TryParse(NVIdString, out Guid NVID))
            {
                return Unauthorized(new { message = "Nhân viên không tồn tại trong phiên làm việc." });
            }
            if (ModelState.IsValid)
            {
                var lichSuThanhToanModel = new LichSuThanhToan
                {
                    IdLichSuThanhToan = Guid.NewGuid(),
                    SoTien = lichSuThanhToan.SoTien,
                    TienThua = lichSuThanhToan.TienThua,
                    NgayTao = DateTime.Now,
                    LoaiGiaoDich = "Thanh Toán",
                    Pttt = lichSuThanhToan.HinhThucThanhToan,
                    NguoiThaoTac = TenNv,
                    TrangThai =  "Đã thanh toán",
                    IdHoaDon = lichSuThanhToan.IdHoaDon,
                    IdNhanVien = NVID,
                };
               
                await _lichSuThanhToanService.AddAsync(lichSuThanhToanModel);

                var hoaDon = await _hoaDonService.GetByIdAsync(lichSuThanhToan.IdHoaDon);
                if (hoaDon != null)
                {
                    hoaDon.TrangThai = "Đã Thanh Toán"; 
                    await _hoaDonService.UpdateAsync(hoaDon); 

                    await _lichSuHoaDonService.AddAsync(new LichSuHoaDon
                    {
                        IdHoaDon = hoaDon.IdHoaDon,
                        ThaoTac = hoaDon.TrangThai,
                        NgayTao = DateTime.Now,
                        NguoiThaoTac = TenNv,
                        TrangThai = hoaDon.TrangThai
                    });
                }

                return RedirectToAction("Edit", new { id = lichSuThanhToan.IdHoaDon });
            }

            return PartialView("CreateLichSuThanhToan", lichSuThanhToan);
        }
    }
}
