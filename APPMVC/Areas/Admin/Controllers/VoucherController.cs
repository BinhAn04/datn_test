using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Model;
using APPMVC.IService;
using System.Net.WebSockets;
using Newtonsoft.Json;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private readonly IVoucherService _voucherService;
        private readonly IKhachHangService _khachHangService;
        private readonly ILogger<VoucherController> _logger;
        private readonly ILichSuSuDungVoucherService _lichSuSuDungVoucherService;

        public VoucherController(IVoucherService voucherService, IKhachHangService khachHangService, ILogger<VoucherController> logger, ILichSuSuDungVoucherService lichSuSuDungVoucherService)
        {
            _voucherService = voucherService ?? throw new ArgumentNullException(nameof(voucherService));
            _khachHangService = khachHangService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _lichSuSuDungVoucherService = lichSuSuDungVoucherService;
        }

        // GET: Admin/Voucher
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 6)
        {
            try
            {
                var sessionData = HttpContext.Session.GetString("NhanVien");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Login", "NhanVien");
                }

                List<Voucher> vouchers = await _voucherService.GetVouchersAsync();

                foreach (var voucher in vouchers)
                {
                    // Chỉ cập nhật khi cần (đến ngày kích hoạt hoặc dừng)
                    if (voucher.TrangThai != 4) // Bỏ qua nếu voucher đã bị "Dừng"
                    {
                        if (voucher.TrangThai == 1 && voucher.NgayBatDau <= DateTime.Now)
                        {
                            // Chờ kích hoạt -> Kích hoạt
                            _logger.LogInformation($"Updating voucher {voucher.VoucherId} status to 2 (Active).");
                            voucher.TrangThai = 2;
                            await _voucherService.UpdateVoucherStatusAsync(voucher, 2);
                        }
                        else if (voucher.TrangThai == 2 && voucher.NgayKetThuc < DateTime.Now)
                        {
                            // Kích hoạt -> Dừng
                            _logger.LogInformation($"Updating voucher {voucher.VoucherId} status to 4 (Expired).");
                            voucher.TrangThai = 4;
                            await _voucherService.UpdateVoucherStatusAsync(voucher, 4);
                        }
                    }
                }

                var sortedVoucher = vouchers.OrderByDescending(x => x.NgayTao).ToList();
                int totalVouchers = sortedVoucher.Count;
                var pagedVouchers = sortedVoucher
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = (int)Math.Ceiling(totalVouchers / (double)pageSize);

                return View(pagedVouchers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching voucher list");
                return View(new List<Voucher>());
            }
        }
        public async Task<IActionResult> Details(Guid id, int page = 1)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id cannot be empty");
            }

            try
            {
                var voucher = await _voucherService.GetVoucherByIdAsync(id);
                if (voucher == null)
                {
                    _logger.LogWarning($"Voucher not found for Id: {id}");
                    return NotFound("Voucher not found");
                }

                var khachHangList = await _voucherService.GetKhachHangDaNhanVoucherAsync(id);
                int pageSize = 6; // Số khách hàng mỗi trang
                var totalCustomers = khachHangList.Count; // Tổng số khách hàng
                var totalPages = (int)Math.Ceiling(totalCustomers / (double)pageSize); // Tổng số trang

                // Lấy danh sách khách hàng cho trang hiện tại
                var pagedCustomers = khachHangList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Gán dữ liệu vào ViewBag
                ViewBag.KhachHangList = pagedCustomers;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(voucher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching voucher details with Id: {id}");
                return StatusCode(500, "An error occurred while retrieving the voucher details.");
            }
        }

        // GET: Admin/Voucher/Create
        public async Task<IActionResult> Create()
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login", "NhanVien");
            }
            //const int pageSize = 9;
            //var customers = await _khachHangService.GetAllKhachHang();
            //ViewBag.KhachHang = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList(); // Lấy khách hàng cho trang hiện tại
            //ViewBag.CurrentPage = page; // Trang hiện tại
            //ViewBag.TotalPages = (int)Math.Ceiling(customers.Count / (double)pageSize); // Tổng số trang

            ViewBag.KhachHang = (await _khachHangService.GetAllKhachHang())
                    .Where(kh => kh.KichHoat == 1)
                    .ToList();

            return View();
        }

        // POST: Admin/Voucher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voucher voucher, string[] selectedKhachHangIds)
        {
            try
            {
                _logger.LogInformation("Bắt đầu tạo phiếu giảm giá.");

                // Check for selected customer IDs
                if (selectedKhachHangIds == null || selectedKhachHangIds.Length == 0)
                {
                    _logger.LogWarning("Không có ID khách hàng nào được chọn.");
                    ModelState.AddModelError("", "Vui lòng chọn ít nhất một khách hàng.");
                    return await LoadKhachHangAndReturnView(voucher);
                }

                // Convert from string[] to List<Guid>
                List<Guid> selectedKhachHangGuidIds = selectedKhachHangIds
                    .Select(id => Guid.Parse(id))
                    .ToList();

                if (voucher.NgayKetThuc <= voucher.NgayBatDau)
                {
                    _logger.LogWarning($"Khoảng ngày không hợp lệ: Ngày bắt đầu ({voucher.NgayBatDau}) sau hoặc bằng Ngày kết thúc ({voucher.NgayKetThuc}).");
                    ModelState.AddModelError("", "Ngày kết thúc phải muộn hơn ngày bắt đầu.");
                    return await LoadKhachHangAndReturnView(voucher);
                }

                if (voucher.NgayBatDau.Date > DateTime.Now.Date) // Sử dụng DateTime.Now.Date thay vì DateTime.UtcNow.Date
                {
                    _logger.LogInformation($"Setting TrangThai to 1 as NgayBatDau ({voucher.NgayBatDau}) is in the future.");
                    voucher.TrangThai = 1; // Set trạng thái là 1 (trong tương lai)
                }
                else if (voucher.NgayBatDau.Date <= DateTime.Now.Date) // Sử dụng DateTime.Now.Date thay vì DateTime.UtcNow.Date
                {
                    _logger.LogInformation($"Setting TrangThai to 2 as NgayBatDau ({voucher.NgayBatDau}) is today or in the past.");
                    voucher.TrangThai = 2; // Set trạng thái là 2 (đã bắt đầu)
                }
                if (await _voucherService.CheckMaVoucher(voucher.MaVoucher))
                {
                    _logger.LogWarning($"Mã Voucher {voucher.MaVoucher} Đã Tồn Tại");
                    ModelState.AddModelError("", "Mã Voucher Này Đã Tồn Tại.");
                    return await LoadKhachHangAndReturnView(voucher);
                }
                // Check ModelState
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        _logger.LogWarning($"ModelState Error: {error.ErrorMessage}");
                    }
                    return await LoadKhachHangAndReturnView(voucher);
                }

                // Log voucher information before sending the request
                _logger.LogInformation($"Creating voucher with details: {JsonConvert.SerializeObject(voucher)}");

                var nguoiTao = HttpContext.Session.GetString("NhanVienName");
                // Convert Voucher to VoucherDto
                var voucherDto = new VoucherDto
                {
                    VoucherId = Guid.NewGuid(), // Generate new ID
                    MaVoucher = voucher.MaVoucher,
                    MoTaVoucher = voucher.MoTaVoucher,
                    LoaiGiamGia = voucher.LoaiGiamGia,
                    GiaTriGiam = voucher.GiaTriGiam,
                    GiaTriDonHangToiThieu = voucher.GiaTriDonHangToiThieu,
                    SoTienToiDa = voucher.SoTienToiDa,
                    NgayBatDau = voucher.NgayBatDau,
                    NgayKetThuc = voucher.NgayKetThuc,
                    TrangThai = voucher.TrangThai,
                    NgayTao = DateTime.UtcNow,
                    NguoiTao = nguoiTao,
                    NgayUpdate = null,
                    NguoiUpdate = null
                };

                // Call the service to create the voucher
                var result = await _voucherService.CreateAsync(voucherDto, selectedKhachHangGuidIds);

                if (result)
                {
                    _logger.LogInformation($"Đã tạo phiếu giảm giá thành công với ID: {voucherDto.VoucherId}");
                    TempData["SuccessMessage"] = "Phiếu giảm giá đã được tạo thành công.";
                    return RedirectToAction(nameof(Index));
                }

                _logger.LogWarning("Không tạo được phiếu giảm giá.");
                ModelState.AddModelError("", "Không tạo được phiếu giảm giá.");
                return await LoadKhachHangAndReturnView(voucher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong hành động Tạo");
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return await LoadKhachHangAndReturnView(voucher);
            }
        }

        private async Task<IActionResult> LoadKhachHangAndReturnView(Voucher voucher)
        {
            // Lấy tất cả khách hàng
            var khachHangs = await _khachHangService.GetAllKhachHang();

            // Lọc khách hàng có trạng thái = 1
            var filteredKhachHangs = khachHangs
                .Where(kh => kh.KichHoat == 1)
                .ToList();

            ViewBag.KhachHang = filteredKhachHangs; 
            return View(voucher);
        }
        // GET: Admin/Voucher/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            // Ensure these values are set
            if (string.IsNullOrEmpty(voucher.NguoiTao))
            {
                voucher.NguoiTao = "Admin"; // Or whatever default value you want
            }
            if (voucher.NgayTao == default)
            {
                voucher.NgayTao = DateTime.Now;
            }
            ViewBag.TrangThaiOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Chờ kích hoạt" },
                new SelectListItem { Value = "2", Text = "Kích hoạt" },
                new SelectListItem { Value = "4", Text = "Dừng" }
            };
            ViewBag.LoaiGiamGiaOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Theo % sản phẩm" },
                new SelectListItem { Value = "2", Text = "Theo số tiền" }
            };
            return View(voucher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Voucher voucher)
        {
            try
            {
                var sessionData = HttpContext.Session.GetString("NhanVien");
                if (string.IsNullOrEmpty(sessionData))
                {
                    return RedirectToAction("Login");
                }

                var nguoiTao = HttpContext.Session.GetString("NhanVienName");

                
                if (string.IsNullOrEmpty(voucher.NguoiTao))
                {
                    voucher.NguoiTao = nguoiTao ?? "Admin"; 
                }
                if (string.IsNullOrEmpty(voucher.NguoiUpdate))
                {
                    voucher.NguoiUpdate = nguoiTao ?? "Admin";
                }
               

                if (id != voucher.VoucherId)
                {
                    return NotFound();
                }
                // Kiểm tra ngày
                if (voucher.NgayKetThuc <= voucher.NgayBatDau)
                {
                    TempData["ErrorVouCher"] = "Ngày kết thúc phải muộn hơn ngày bắt đầu.";
                    ViewBag.TrangThaiOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "Chờ kích hoạt" },
                        new SelectListItem { Value = "2", Text = "Kích hoạt" },
                        new SelectListItem { Value = "4", Text = "Dừng" }
                    };
                    ViewBag.LoaiGiamGiaOptions = new List<SelectListItem>
                    {
                        new SelectListItem { Value = "1", Text = "Theo % sản phẩm" },
                        new SelectListItem { Value = "2", Text = "Theo số tiền" }
                    };
                    return View(voucher);
                }
                //if (voucher.NgayBatDau > DateTime.Now)
                //{
                //    TempData["ErrorVouCher"] = "Không thể để trạng thái 'Kích Hoạt' khi ngày bắt đầu lớn hơn ngày hiện tại. Vui lòng kiểm tra và thử lại.";
                //    return View(voucher);
                //}
                //Kiểm tra tên
                var isDuplicate = await _voucherService.CheckMaVoucher(voucher.MaVoucher);
                if (isDuplicate)
                {
                    // Kiểm tra xem mã này có phải của voucher hiện tại không
                    var currentVoucher = await _voucherService.GetVoucherByIdAsync(id);
                    if (currentVoucher == null || currentVoucher.MaVoucher != voucher.MaVoucher)
                    {
                        TempData["ErrorVouCher"] = "Mã voucher đã tồn tại. Vui lòng chọn mã khác.";
                        ViewBag.TrangThaiOptions = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "1", Text = "Chờ kích hoạt" },
                            new SelectListItem { Value = "2", Text = "Kích hoạt" },
                            new SelectListItem { Value = "4", Text = "Dừng" }
                        };
                        ViewBag.LoaiGiamGiaOptions = new List<SelectListItem>
                        {
                            new SelectListItem { Value = "1", Text = "Theo % sản phẩm" },
                            new SelectListItem { Value = "2", Text = "Theo số tiền" }
                        };
                        return View(voucher);
                    }
                }
                if (ModelState.IsValid)
                {
                    
                    if (voucher.NgayTao == default)
                    {
                        voucher.NgayTao = DateTime.Now;
                    }

                    
                    voucher.NgayUpdate = DateTime.Now;
                    voucher.NguoiUpdate = nguoiTao;
                    
                    var result = await _voucherService.UpdateAsync(voucher);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Cập nhật voucher thành công";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    // Thu thập và log lỗi từ ModelState
                    var errors = ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .SelectMany(ms => ms.Value.Errors.Select(err => err.ErrorMessage))
                        .ToList();

                    foreach (var error in errors)
                    {
                        _logger.LogError($"Validation error: {error}");
                    }

                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật voucher. Vui lòng kiểm tra lại dữ liệu.";
                    TempData["ValidationErrors"] = errors;
                }
                ViewBag.TrangThaiOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Chờ kích hoạt" },
                    new SelectListItem { Value = "2", Text = "Kích hoạt" },
                    new SelectListItem { Value = "4", Text = "Dừng" }
                };
                ViewBag.LoaiGiamGiaOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Theo % sản phẩm" },
                    new SelectListItem { Value = "2", Text = "Theo số tiền" }
                };
                return View(voucher);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.TrangThaiOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Chờ kích hoạt" },
                    new SelectListItem { Value = "2", Text = "Kích hoạt" },
                    new SelectListItem { Value = "4", Text = "Dừng" }
                };
                ViewBag.LoaiGiamGiaOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Theo % sản phẩm" },
                    new SelectListItem { Value = "2", Text = "Theo số tiền" }
                };
                return View(voucher);
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id cannot be empty");
            }
            try
            {
                var voucher = await _voucherService.DeleteAsync(id);
                if (voucher != null)
                {
                    var result = await _voucherService.DeleteAsync(id);
                    if (!result)
                    {
                        _logger.LogInformation($"Successfully deleted voucher with Id: {id}");
                        TempData["SuccessMessage"] = "Xoá voucher thành công.";
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to delete voucher with Id: {id}");
                        TempData["ErrorMessage"] = "Failed to delete the voucher.";
                    }
                }
                else
                {
                    _logger.LogWarning($"Attempted to delete non-existent voucher with Id: {id}");
                    TempData["ErrorMessage"] = "Voucher not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while deleting voucher with Id: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the voucher.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
