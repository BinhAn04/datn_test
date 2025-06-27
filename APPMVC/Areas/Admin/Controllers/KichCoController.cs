using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KichCoController : Controller
    {
        private readonly IKichCoService _services;

        public KichCoController(IKichCoService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;

            try
            {
                List<KichCo> kichCos = await _services.GetKichCo(name);
                var pagedKichCos = kichCos.ToPagedList(page, pageSize);
                return View(pagedKichCos);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                TempData["Error"] = "Không thể tải danh sách kích cỡ: " + ex.Message;
                return View(new List<KichCo>().ToPagedList(1, 5));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string TenKichCo)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(TenKichCo))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên kích cỡ không được để trống"
                    });
                }

                // Kiểm tra trùng tên (nếu cần)
                var existingKichCos = await _services.GetKichCo(TenKichCo);
                if (existingKichCos != null && existingKichCos.Any(k =>
                    k.TenKichCo.Trim().ToLower() == TenKichCo.Trim().ToLower()))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên kích cỡ đã tồn tại"
                    });
                }

                var kichCo = new KichCo
                {
                    IdKichCo = Guid.NewGuid(),
                    TenKichCo = TenKichCo,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin", // Lấy từ session
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                };

                await _services.Create(kichCo);

                return Json(new
                {
                    success = true,
                    id = kichCo.IdKichCo.ToString(),
                    tenKichCo = kichCo.TenKichCo,
                    kichHoat = kichCo.KichHoat,
                    ngayTao = kichCo.NgayTao.ToString("dd/MM/yyyy")
                });
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi kết nối API
                return Json(new
                {
                    success = false,
                    message = "Lỗi kết nối: " + ex.Message
                });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(KichCo kichCo)
        {
            try
            {
                // Validation chi tiết hơn
                if (kichCo == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ"
                    });
                }

                if (string.IsNullOrWhiteSpace(kichCo.TenKichCo))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên kích cỡ không được để trống"
                    });
                }

                // Kiểm tra trùng tên
                var existingKichCos = await _services.GetKichCo(null);
                if (existingKichCos.Any(k =>
                    k.TenKichCo.Trim().ToLower() == kichCo.TenKichCo.Trim().ToLower() &&
                    k.IdKichCo != kichCo.IdKichCo))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên kích cỡ đã tồn tại"
                    });
                }

                // Lấy đối tượng hiện tại để cập nhật
                var existingKichCo = await _services.GetKichCoById(kichCo.IdKichCo);
                if (existingKichCo == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không tìm thấy kích cỡ"
                    });
                }

                // Cập nhật từng trường
                existingKichCo.TenKichCo = kichCo.TenKichCo;
                existingKichCo.KichHoat = kichCo.KichHoat;
                existingKichCo.NgayCapNhat = DateTime.Now;
                existingKichCo.NguoiCapNhat = "Admin"; // Nên lấy từ session thực tế

                await _services.Update(existingKichCo);

                return Json(new
                {
                    success = true,
                    id = existingKichCo.IdKichCo.ToString(),
                    tenKichCo = existingKichCo.TenKichCo,
                    kichHoat = existingKichCo.KichHoat
                });
            }
            catch (Exception ex)
            {
                // Log lỗi ở đây nếu cần
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra: " + ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _services.Delete(id);
                return Json(new
                {
                    success = true
                });
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi kết nối API
                return Json(new
                {
                    success = false,
                    message = "Lỗi kết nối: " + ex.Message
                });
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra: " + ex.Message
                });
            }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var kichCo = await _services.GetKichCoById(id);
                if (kichCo == null)
                {
                    return NotFound();
                }
                return View(kichCo);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải chi tiết: " + ex.Message;
                return RedirectToAction(nameof(Getall));
            }
        }
    }
}
