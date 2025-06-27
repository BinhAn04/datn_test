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
using AppData.ViewModel;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PromotionController : Controller
    {
        private readonly IPromotionService _promotionService;
        private readonly ILogger<PromotionController> _logger;
        private readonly ISanPhamService _sanPhamService;
        private readonly ISanPhamChiTietService _sanPhamChiTietService;
        private readonly IMauSacService _mauSacService;
        private readonly IKichCoService _kichCoService;
        private readonly ISanPhamChiTietMauSacService _sanPhamChiTietMauSacService;
        private readonly ISanPhamChiTietKichCoService _sanPhamChiTietKichCoService;
        private readonly IHinhAnhService _hinhAnhService;
        private readonly IChatLieuService _chatLieuService;
        private readonly IDeGiayService _deGiayService;
        private readonly IDanhMucService _danhMucService;
        private readonly IThuongHieuService _thuongHieuService;
        private readonly IKieuDangService _kieuDangService;
        private readonly IPromotionSanPhamChiTietService _promotionSanPhamChiTietService;

        public PromotionController(IPromotionService promotionService, ILogger<PromotionController> logger, ISanPhamService sanPhamService, ISanPhamChiTietService sanPhamChiTietService, IMauSacService mauSacService, IKichCoService kichCoService, ISanPhamChiTietMauSacService sanPhamChiTietMauSacService, ISanPhamChiTietKichCoService sanPhamChiTietKichCoService, IHinhAnhService hinhAnhService, IDeGiayService deGiayService,
            IDanhMucService danhMucService,
            IThuongHieuService thuongHieuService,
            IChatLieuService chatLieuService,
            IKieuDangService kieuDangService,
            IPromotionSanPhamChiTietService promotionSanPhamChiTietService)
        {
            _promotionService = promotionService ?? throw new ArgumentNullException(nameof(promotionService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sanPhamService = sanPhamService;
            _sanPhamChiTietService = sanPhamChiTietService;
            _mauSacService = mauSacService;
            _kichCoService = kichCoService;
            _sanPhamChiTietMauSacService = sanPhamChiTietMauSacService;
            _sanPhamChiTietKichCoService = sanPhamChiTietKichCoService;
            _hinhAnhService = hinhAnhService;
            _deGiayService = deGiayService;
            _danhMucService = danhMucService;
            _thuongHieuService = thuongHieuService;
            _chatLieuService = chatLieuService;
            _kieuDangService = kieuDangService;
            _promotionSanPhamChiTietService = promotionSanPhamChiTietService;
        }
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
                List<Promotion> promotions = await _promotionService.GetPromotionsAsync();
                _logger.LogInformation($"Retrieved {promotions.Count} promotions");
                var sapXep = promotions.OrderByDescending(x => x.NgayTao).ToList();

                var pagedPromotions = sapXep.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Tính tổng số trang
                var totalPromotions = sapXep.Count();
                var totalPages = (int)Math.Ceiling(totalPromotions / (double)pageSize);

                // Truyền dữ liệu phân trang vào View
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(pagedPromotions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching promotion list");
                return View(new List<Promotion>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new PromotionViewModel
            {
                SanPhams = await GetProducts(),
                NgayBatDau = DateTime.Now,
                NgayKetThuc = DateTime.Now.AddHours(1),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromotionViewModel model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                model.SanPhams = await GetProducts();
                return View(model);
            }

            try
            {
                var promotion = model.Promotion;
                promotion.NgayBatDau = model.NgayBatDau;
                promotion.NgayKetThuc = model.NgayKetThuc;
                promotion.IdPromotion = Guid.NewGuid();
                promotion.NgayTao = DateTime.Now;

                // Validate the dates
                if (promotion.NgayBatDau >= promotion.NgayKetThuc)
                {
                    TempData["ErrorMessage"] = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.";
                    model.SanPhams = await GetProducts();
                    return View(model);
                }

                bool isPromotionStatusTwo = false;
                var currentDateTime = DateTime.Now;

                foreach (var idSanPhamChiTiet in model.SelectedSanPhamChiTietIds)
                {
                    var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(idSanPhamChiTiet);
                    var sanPham = await _sanPhamChiTietService.GetSanPhamByIdSanPhamChiTietAsync(idSanPhamChiTiet);

                    if (sanPhamChiTiet == null || sanPhamChiTiet.KichHoat != 1)
                    {
                        TempData["ErrorMessage"] = $"Sản phẩm '{sanPham?.TenSanPham}' không hoạt động. Không thể thêm khuyến mãi.";
                        model.SanPhams = await GetProducts();
                        return View(model);
                    }

                    if (sanPham == null || sanPham.KichHoat != 1)
                    {
                        TempData["ErrorMessage"] = $"Sản phẩm '{sanPham?.TenSanPham}' không hoạt động. Không thể thêm khuyến mãi.";
                        model.SanPhams = await GetProducts();
                        return View(model);
                    }

                    // Get existing promotion ID
                    var existingPromotionIdNullable = await _promotionSanPhamChiTietService.GetPromotionsBySanPhamChiTietIdAsync(idSanPhamChiTiet);

                    // Check if there is an existing promotion
                    if (existingPromotionIdNullable.HasValue && existingPromotionIdNullable.Value != Guid.Empty)
                    {
                        var existingPromotion = await _promotionService.GetPromotionByIdAsync(existingPromotionIdNullable.Value);

                        // Check the status of the existing promotion
                        if (existingPromotion != null && existingPromotion.TrangThai == 1)
                        {
                            // Check promotion time
                            if (existingPromotion.NgayBatDau < promotion.NgayKetThuc &&
                                existingPromotion.NgayKetThuc > promotion.NgayBatDau)
                            {
                                TempData["ErrorMessage"] = $"Sản phẩm '{sanPham.TenSanPham}' đang trong khuyến mãi '{existingPromotion.TenPromotion}' từ {existingPromotion.NgayBatDau:yyyy-MM-dd} đến {existingPromotion.NgayKetThuc:yyyy-MM-dd}.";
                                model.SanPhams = await GetProducts();
                                return View(model);
                            }

                            // Check if the new promotion's start and end dates are greater than the existing one
                            if (promotion.NgayBatDau > existingPromotion.NgayKetThuc && promotion.NgayKetThuc > existingPromotion.NgayKetThuc)
                            {
                                isPromotionStatusTwo = true;
                            }
                        }
                    }
                }

                // Create the promotion
                promotion.PromotionSanPhamChiTiets = model.SelectedSanPhamChiTietIds
                    .Select(idSanPhamChiTiet => new PromotionSanPhamChiTiet
                    {
                        IdPromotion = promotion.IdPromotion,
                        IdSanPhamChiTiet = idSanPhamChiTiet
                    })
                    .ToList();

                // Set promotion status based on the new logic
                if (promotion.NgayBatDau > currentDateTime)
                {
                    isPromotionStatusTwo = true; // Set to status 2 if end date is greater than current time
                }

                promotion.TrangThai = isPromotionStatusTwo ? 2 : 1;

                var result = await _promotionService.CreateAsync(promotion);

                if (result)
                {
                    // Update the discounted price for each product detail only if status is not 2
                    if (!isPromotionStatusTwo)
                    {
                        foreach (var idSanPhamChiTiet in model.SelectedSanPhamChiTietIds)
                        {
                            var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(idSanPhamChiTiet);
                            if (sanPhamChiTiet != null)
                            {
                                double originalPrice = sanPhamChiTiet.Gia;
                                double discountPercentage = promotion.PhanTramGiam;

                                // Calculate discounted price
                                double discountedPrice = originalPrice * (1 - (discountPercentage / 100.0));

                                sanPhamChiTiet.GiaGiam = discountedPrice;
                                await _sanPhamChiTietService.Update(sanPhamChiTiet);
                            }
                        }
                    }

                    TempData["SuccessMessage"] = "Tạo khuyến mãi thành công.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể tạo khuyến mãi. Vui lòng thử lại.";
                    model.SanPhams = await GetProducts();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo khuyến mãi");
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                model.SanPhams = await GetProducts();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> CheckProductInPromotion(Guid productDetailId, DateTime startDate, DateTime endDate)
        {
            var existingPromotions = await _promotionService.GetPromotionsAsync();

            // Kiểm tra xem sản phẩm có trong bất kỳ khuyến mãi nào đang hoạt động không
            var isInPromotion = existingPromotions
                .Where(p => p.TrangThai == 1 && // Chỉ kiểm tra các khuyến mãi đang hoạt động
                       p.PromotionSanPhamChiTiets != null &&
                       p.PromotionSanPhamChiTiets.Any(ps => ps.IdSanPhamChiTiet == productDetailId))
                .Any(p => CheckPromotionTimeConflict(p.NgayBatDau, p.NgayKetThuc, startDate, endDate));

            return Json(isInPromotion);
        }
        private bool CheckPromotionTimeConflict(DateTime existStart, DateTime existEnd,
                                        DateTime newStart, DateTime newEnd)
        {
            // Kiểm tra xem khoảng thời gian mới có giao với khoảng thời gian hiện tại không
            return (newStart < existEnd && newEnd > existStart) ||  // Giao nhau
                    (newStart >= existStart && newStart < existEnd) ||  // Bắt đầu trong khoảng
                    (newEnd > existStart && newEnd <= existEnd) ||  // Kết thúc trong khoảng
                    (newStart <= existStart && newEnd >= existEnd);  // Bao trùm hoàn toàn
        }
        [HttpGet]
        private async Task<List<PromotionViewModel.SanPhamViewModel>> GetProducts()
        {
            var sanPhams = await _sanPhamService.GetSanPhams(null);
            var promotionSanPhams = new List<PromotionViewModel.SanPhamViewModel>();

            foreach (var sanPham in sanPhams)
            {
                var thuongHieu = await _thuongHieuService.GetThuongHieuById(sanPham.IdThuongHieu);
                var danhMuc = await _danhMucService.GetDanhMucById(sanPham.IdDanhMuc);
                var chatLieu = await _chatLieuService.GetChatLieuById(sanPham.IdChatLieu);
                var kieuDang = await _kieuDangService.GetKieuDangById(sanPham.IdKieuDang);
                var deGiay = await _deGiayService.GetDeGiayById(sanPham.IdDeGiay);

                var promotionSanPham = new PromotionViewModel.SanPhamViewModel
                {
                    IdSanPham = sanPham.IdSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    ThuongHieu = thuongHieu?.TenThuongHieu,
                    DanhMuc = danhMuc?.TenDanhMuc,
                    ChatLieu = chatLieu?.TenChatLieu,
                    KieuDang = kieuDang?.TenKieuDang,
                    DeGiay = deGiay?.TenDeGiay
                };

                promotionSanPhams.Add(promotionSanPham);
            }

            return promotionSanPhams;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(string sanPhamIds)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(sanPhamIds))
            {
                return BadRequest("Invalid product IDs."); // Return 400 if the IDs are invalid
            }

            // Split the incoming IDs and parse them
            var idList = sanPhamIds.Split(',')
                                    .Select(id => Guid.TryParse(id.Trim(), out var parsedId) ? parsedId : Guid.Empty)
                                    .ToList();

            // Check for any invalid IDs
            if (idList.All(id => id == Guid.Empty))
            {
                return BadRequest("No valid product IDs provided."); // Return 400 if all IDs are invalid
            }

            var sanPhamChiTietViewModels = new List<PromotionViewModel.SanPhamChiTietViewModel>();

            foreach (var sanPhamId in idList)
            {
                if (sanPhamId == Guid.Empty) continue;

                // Fetch the product by ID
                var sanPham = await _sanPhamService.GetSanPhamById(sanPhamId);
                if (sanPham == null) continue;

                var sanPhamChiTietList = await _sanPhamChiTietService.GetSanPhamChiTietBySanPhamId(sanPhamId);

                if (sanPhamChiTietList != null && sanPhamChiTietList.Any())
                {
                    foreach (var chiTiet in sanPhamChiTietList)
                    {
                        if (chiTiet != null)
                        {
                            var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(chiTiet.IdSanPhamChiTiet);
                            var mauSacTenList = mauSacList?.Select(ms => ms?.TenMauSac).ToList() ?? new List<string>();

                            var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(chiTiet.IdSanPhamChiTiet);
                            var kichCoTenList = kichCoList?.Select(kc => kc?.TenKichCo).ToList() ?? new List<string>();

                            var hinhAnhs = await _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(chiTiet.IdSanPhamChiTiet);

                            sanPhamChiTietViewModels.Add(new PromotionViewModel.SanPhamChiTietViewModel
                            {
                                IdSanPhamChiTiet = chiTiet.IdSanPhamChiTiet,
                                MaSP = chiTiet.MaSp,
                                ProductName = sanPham.TenSanPham,
                                Quantity = chiTiet.SoLuong,
                                Price = chiTiet.Gia,
                                HinhAnhs = hinhAnhs,
                                MauSac = mauSacTenList,
                                KichCo = kichCoTenList,
                            });
                        }
                    }
                }
            }

            return Json(new { ChiTietSanPhams = sanPhamChiTietViewModels });
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id cannot be empty");
            }

            try
            {
                var promotion = await _promotionService.GetPromotionByIdAsync(id);
                if (promotion != null)
                {
                    // Lấy danh sách sản phẩm chi tiết liên quan đến khuyến mãi
                    var promotionSanPhamChiTiets = await _promotionSanPhamChiTietService.GetPromotionSanPhamChiTietsByPromotionIdAsync(id);
                    foreach (var promotionSanPhamChiTiet in promotionSanPhamChiTiets)
                    {
                        var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(promotionSanPhamChiTiet.IdSanPhamChiTiet);
                        if (sanPhamChiTiet != null)
                        {
                            // Đặt giá giảm về 0
                            sanPhamChiTiet.GiaGiam = 0;
                            await _sanPhamChiTietService.Update(sanPhamChiTiet);
                        }
                    }

                    // Xóa khuyến mãi
                    var result = await _promotionService.DeleteAsync(id);
                    if (result)
                    {
                        _logger.LogInformation($"Successfully deleted promotion with Id: {id}");
                        TempData["SuccessMessage"] = "Promotion deleted successfully.";
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to delete promotion with Id: {id}");
                        TempData["ErrorMessage"] = "Failed to delete the promotion.";
                    }
                }
                else
                {
                    _logger.LogWarning($"Attempted to delete non-existent promotion with Id: {id}");
                    TempData["ErrorMessage"] = "Promotion not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting promotion with Id: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the promotion.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id cannot be empty");
            }

            try
            {
                var promotion = await _promotionService.GetPromotionByIdAsync(id);
                if (promotion == null)
                {
                    _logger.LogWarning($"Promotion not found for Id: {id}");
                    return NotFound("Promotion not found.");
                }

                // Tạo một thể hiện của PromotionViewModel
                var promotionViewModel = new PromotionViewModel
                {
                    Promotion = promotion,
                    NgayBatDau = promotion.NgayBatDau, // Giả sử đây là thuộc tính trong Promotion
                    NgayKetThuc = promotion.NgayKetThuc, // Giả sử đây là thuộc tính trong Promotion
                    NgayTao = promotion.NgayTao

                };

                // Lấy danh sách sản phẩm chi tiết liên quan đến khuyến mãi
                promotionViewModel.SanPhamChiTiets = new List<PromotionViewModel.SanPhamChiTietViewModel>();
                var promotionSanPhamChiTiets = await _promotionSanPhamChiTietService.GetPromotionSanPhamChiTietsByPromotionIdAsync(id);

                // Lấy thông tin chi tiết sản phẩm cho từng sản phẩm trong khuyến mãi
                foreach (var item in promotionSanPhamChiTiets)
                {
                    // Lấy chi tiết sản phẩm
                    var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(item.IdSanPhamChiTiet);
                    if (sanPhamChiTiet == null) continue;

                    // Lấy sản phẩm tương ứng
                    var sanPham = await _sanPhamService.GetSanPhamById(sanPhamChiTiet.IdSanPham);
                    if (sanPham == null) continue;

                    // Lấy màu sắc, kích cỡ và hình ảnh
                    var mauSacList = await _sanPhamChiTietMauSacService.GetMauSacIdsBySanPhamChiTietId(sanPhamChiTiet.IdSanPhamChiTiet);
                    var mauSacTenList = mauSacList?.Select(ms => ms?.TenMauSac).ToList() ?? new List<string>();

                    var kichCoList = await _sanPhamChiTietKichCoService.GetKichCoIdsBySanPhamChiTietId(sanPhamChiTiet.IdSanPhamChiTiet);
                    var kichCoTenList = kichCoList?.Select(kc => kc?.TenKichCo).ToList() ?? new List<string>();

                    var hinhAnhs = await _hinhAnhService.GetHinhAnhsBySanPhamChiTietId(sanPhamChiTiet.IdSanPhamChiTiet);

                    promotionViewModel.SanPhamChiTiets.Add(new PromotionViewModel.SanPhamChiTietViewModel
                    {
                        IdSanPhamChiTiet = sanPhamChiTiet.IdSanPhamChiTiet,
                        MaSP = sanPhamChiTiet.MaSp,
                        ProductName = sanPham?.TenSanPham,
                        Quantity = sanPhamChiTiet.SoLuong,
                        Price = sanPhamChiTiet.Gia,
                        HinhAnhs = hinhAnhs,
                        MauSac = mauSacTenList,
                        KichCo = kichCoTenList,
                    });
                }

                ViewBag.TrangThaiList = new SelectList(new[]
                {
            new { Value = 0, Text = "Dừng Hoạt Động" },
            new { Value = 1, Text = "Hoạt Động" },
            new { Value = 2, Text = "Chờ Hoạt Động" }
        }, "Value", "Text", promotion.TrangThai);

                return View(promotionViewModel); // Trả về PromotionViewModel
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching promotion with Id: {id}");
                return StatusCode(500, "An error occurred while retrieving the promotion.");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PromotionViewModel model)
        {
            if (id != model.Promotion.IdPromotion)
            {
                return BadRequest("Id mismatch");
            }

            if (!ModelState.IsValid)
            {
                model.SanPhams = await GetProducts();
                return View(model);
            }

            try
            {
                var promotion = model.Promotion;

                // Validate date range
                if (promotion.NgayBatDau >= promotion.NgayKetThuc)
                {
                    TempData["ErrorMessage"] = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.";
                    model.SanPhams = await GetProducts();
                    return View(model);
                }

                var currentDateTime = DateTime.Now;

                // Check state transition requirements
                if (promotion.TrangThai == 2 && promotion.NgayBatDau <= currentDateTime)
                {
                    TempData["ErrorMessage"] = "Không thể chuyển sang trạng thái 'Chờ Hoạt Động' khi thời gian bắt đầu không lớn hơn thời gian hiện tại.";
                    model.SanPhams = await GetProducts();
                    return View(model);
                }

                // Get promotion details linked to this promotion
                var promotionSanPhamChiTiets = await _promotionSanPhamChiTietService.GetPromotionSanPhamChiTietsByPromotionIdAsync(promotion.IdPromotion);

                // Check for active promotions linked to the same products
                foreach (var detail in promotionSanPhamChiTiets)
                {
                    var existingPromotionIdNullable = await _promotionSanPhamChiTietService.GetPromotionsBySanPhamChiTietIdAsync(detail.IdSanPhamChiTiet);
                    if (existingPromotionIdNullable.HasValue && existingPromotionIdNullable.Value != Guid.Empty)
                    {
                        var activePromotion = await _promotionService.GetPromotionByIdAsync(existingPromotionIdNullable.Value);

                        if (activePromotion != null && activePromotion.IdPromotion != promotion.IdPromotion)
                        {
                            // Check for time overlap
                            if (activePromotion.NgayBatDau < promotion.NgayKetThuc &&
                                activePromotion.NgayKetThuc > promotion.NgayBatDau)
                            {
                                TempData["ErrorMessage"] = "Không thể kích hoạt hoặc chuyển sang trạng thái 'Chờ Hoạt Động' vì có sản phẩm đang hoạt động trong khoảng thời gian này.";
                                model.SanPhams = await GetProducts();
                                return View(model);
                            }
                        }
                    }
                }

                // Update discount prices for related products based on promotion status
                foreach (var promotionSanPhamChiTiet in promotionSanPhamChiTiets)
                {
                    var sanPhamChiTiet = await _sanPhamChiTietService.GetSanPhamChiTietById(promotionSanPhamChiTiet.IdSanPhamChiTiet);
                    if (sanPhamChiTiet != null)
                    {
                        if (promotion.TrangThai == 0)
                        {
                            sanPhamChiTiet.GiaGiam = 0;
                        }
                        else if (promotion.TrangThai == 1)
                        {
                            // Calculate discount if status is 1
                            double originalPrice = sanPhamChiTiet.Gia;
                            double discountPercentage = promotion.PhanTramGiam;
                            sanPhamChiTiet.GiaGiam = originalPrice * (1 - (discountPercentage / 100.0));
                        }

                        await _sanPhamChiTietService.Update(sanPhamChiTiet);
                    }
                }

                // Update the promotion in the database
                var result = await _promotionService.UpdateAsync(promotion);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Không thể cập nhật khuyến mãi. Vui lòng thử lại.";
                    model.SanPhams = await GetProducts();
                    return View(model);
                }

                TempData["SuccessMessage"] = "Khuyến mãi đã được cập nhật thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Đã xảy ra lỗi: {ex.Message}";
                model.SanPhams = await GetProducts();
                return View(model);
            }
        }
    }
}
