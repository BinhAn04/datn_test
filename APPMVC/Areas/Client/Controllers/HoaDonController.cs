using AppData.Model;
using AppData.ViewModel;
using APPMVC.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AppData.ViewModel.HoaDonChiTietViewModel;

namespace APPMVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class HoaDonController : Controller
    {
        public IHoaDonService _hoaDonService;
        public IHoaDonChiTietService _hoaDonChiTietService;
        public ILichSuHoaDonService _lichSuHoaDonService;
        public ISanPhamChiTietService _sanPhamChiTietService;
        public IKhachHangService _khachHangService;
        private readonly IMauSacService _mauSacService;
        private readonly IKichCoService _kichCoService;
        private readonly ISanPhamChiTietMauSacService _sanPhamChiTietMauSacService;
        private readonly ISanPhamChiTietKichCoService _sanPhamChiTietKichCoService;
        private readonly IHinhAnhService _hinhAnhService;
        private readonly ILichSuThanhToanService _lichSuThanhToanService;
        public HoaDonController(IHoaDonService hoaDonService, IHoaDonChiTietService hoaDonChiTietService , ILichSuHoaDonService lichSuHoaDonService, ISanPhamChiTietService sanPhamChiTietService, IKhachHangService khachHangService, IMauSacService mauSacService,
            IKichCoService kichCoService,
            ISanPhamChiTietMauSacService sanPhamChiTietMauSacService,
            ISanPhamChiTietKichCoService sanPhamChiTietKichCoService,
            IHinhAnhService hinhAnhService,
            ILichSuThanhToanService lichSuThanhToanService) 
        {
            _hoaDonService = hoaDonService;
            _hoaDonChiTietService = hoaDonChiTietService;
            _lichSuHoaDonService = lichSuHoaDonService;
            _sanPhamChiTietService = sanPhamChiTietService;
            _khachHangService = khachHangService;
            _mauSacService = mauSacService;
            _kichCoService = kichCoService;
            _sanPhamChiTietMauSacService = sanPhamChiTietMauSacService;
            _sanPhamChiTietKichCoService = sanPhamChiTietKichCoService;
            _hinhAnhService = hinhAnhService;
            _lichSuThanhToanService = lichSuThanhToanService;
        }
        public async Task<IActionResult> Index()
        {
            var customerIdString = HttpContext.Session.GetString("IdKhachHang");
            if (string.IsNullOrEmpty(customerIdString) || !Guid.TryParse(customerIdString, out Guid customerId))
            {
                return Unauthorized(new { message = "Customer not found in session." });
            }
            var hoaDons = await _hoaDonService.GetHoaDonsByCustomerIdAsync(customerId);
            var sortedHoaDons = hoaDons.OrderByDescending(hd => hd.NgayTao).ToList();

            return View(sortedHoaDons);
        }
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
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

            var lichSuHoaDons = await _lichSuHoaDonService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);
            var lichSuThanhToans = await _lichSuThanhToanService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);
            var khachhang = await _khachHangService.GetIdKhachHang(hoaDon.IdKhachHang);

            var sanPhamChiTiets = new List<HoaDonChiTietViewModel.SanPhamChiTietViewModel>();
            foreach (var hoaDonChiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamChiTietService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet);
                var sanPham = await _sanPhamChiTietService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);

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
                        GiaDaGiam = giaDaGiam,
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
        [HttpGet]
        public async Task<IActionResult> GetHoaDonDetails()
        {
            return View(new HoaDonChiTietViewModel()); // Prepare an empty model for the initial view
        }

        [HttpPost]
        public async Task<IActionResult> GetHoaDonDetails(string maHoaDon)
        {
            if (string.IsNullOrEmpty(maHoaDon))
            {
                ModelState.AddModelError("", "Mã hóa đơn không được để trống.");
                return View(new HoaDonChiTietViewModel()); // Return the view with an empty model
            }

            // Fetch the invoice using the provided code
            var hoaDon = await _hoaDonService.GetByMaDonAsync(maHoaDon);
            if (hoaDon == null)
            {
                ModelState.AddModelError("", "Không tìm thấy hóa đơn.");
                return View(new HoaDonChiTietViewModel()); // Return the view with an empty model
            }

            // Retrieve detailed information about the invoice
            var hoaDonChiTietList = await _hoaDonChiTietService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);
            if (hoaDonChiTietList == null || !hoaDonChiTietList.Any())
            {
                ModelState.AddModelError("", $"Không có hóa đơn chi tiết nào cho ID {hoaDon.IdHoaDon}.");
                return View(new HoaDonChiTietViewModel()); // Return the view with an empty model
            }

            // Fetch history and payment information related to the invoice
            var lichSuHoaDons = await _lichSuHoaDonService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);
            var lichSuThanhToans = await _lichSuThanhToanService.GetByIdHoaDonAsync(hoaDon.IdHoaDon);

            // Prepare the detailed product list
            var sanPhamChiTiets = new List<HoaDonChiTietViewModel.SanPhamChiTietViewModel>();
            foreach (var hoaDonChiTiet in hoaDonChiTietList)
            {
                var sanPhamCT = await _sanPhamChiTietService.GetSanPhamChiTietById(hoaDonChiTiet.IdSanPhamChiTiet);
                var sanPham = await _sanPhamChiTietService.GetSanPhamByIdSanPhamChiTietAsync(hoaDonChiTiet.IdSanPhamChiTiet);

                if (sanPhamCT != null)
                {
                    var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var mauSacTenList = mauSacList.Select(ms => ms.TenMauSac).ToList();

                    var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);
                    var kichCoTenList = kichCoList.Select(kc => kc.TenKichCo).ToList();

                    var hinhAnhs = await _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(sanPhamCT.IdSanPhamChiTiet);

                    var giaBan = sanPhamCT.Gia;
                    var giaDaGiam = sanPhamCT.GiaGiam;
                    double? phanTramGiam = null;

                    if (giaDaGiam.HasValue && giaDaGiam < giaBan)
                    {
                        phanTramGiam = Math.Round(((giaBan - giaDaGiam.Value) / giaBan) * 100, 2);
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
                        GiaDaGiam = giaDaGiam,
                        PhanTramGiam = phanTramGiam
                    });
                }
            }

            // Prepare the detailed view model
            var detailViewModel = new HoaDonChiTietViewModel
            {
                DonGia = hoaDon.TongTienDonHang,
                GiamGia = hoaDon.TienGiam,
                PhiVanChuyen = Convert.ToDouble(hoaDon.TienShip),
                TongTien = hoaDon.TongTienDonHang + Convert.ToDouble(hoaDon.TienShip),
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
                SanPhamChiTiets = sanPhamChiTiets, // Add populated product details
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

            return View(detailViewModel); // Return the populated view model
        }
    }
}
