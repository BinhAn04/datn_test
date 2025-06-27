using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MauSacController : Controller
    {
        private readonly IMauSacService _services;

        public MauSacController(IMauSacService services)
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
                List<MauSac> mauSacs = await _services.GetMauSac(name);
                var pagedMauSacs = mauSacs.ToPagedList(page, pageSize);
                return View(pagedMauSacs);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải danh sách màu sắc: " + ex.Message;
                return View(new List<MauSac>().ToPagedList(1, 5));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string TenMauSac)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(TenMauSac))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên màu không được để trống"
                    });
                }

                // Kiểm tra trùng tên (nếu cần)
                var existingMauSacs = await _services.GetMauSac(TenMauSac);
                if (existingMauSacs != null && existingMauSacs.Any(m =>
                    m.TenMauSac.Trim().ToLower() == TenMauSac.Trim().ToLower()))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên màu sắc đã tồn tại"
                    });
                }

                var mauSac = new MauSac
                {
                    IdMauSac = Guid.NewGuid(),
                    TenMauSac = TenMauSac,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    NguoiTao = "Admin",
                    NguoiCapNhat = "Admin",
                    KichHoat = 1
                };

                await _services.Create(mauSac);

                return Json(new
                {
                    success = true,
                    id = mauSac.IdMauSac.ToString(),
                    tenMauSac = mauSac.TenMauSac,
                    kichHoat = mauSac.KichHoat,
                    ngayTao = mauSac.NgayTao.ToString("dd/MM/yyyy")
                });
            }
            catch (HttpRequestException ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi kết nối: " + ex.Message
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Có lỗi xảy ra: " + ex.Message
                });
            }
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var mauSac = await _services.GetMauSacById(id);
            if (mauSac == null)
            {
                return NotFound();
            }
            return View(mauSac);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MauSac mauSac)
        {
            try
            {
                // Kiểm tra input
                if (mauSac == null || string.IsNullOrWhiteSpace(mauSac.TenMauSac))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ"
                    });
                }

                // Kiểm tra trùng tên
                var existingMauSacs = await _services.GetMauSac(null);
                if (existingMauSacs.Any(m =>
                    m.TenMauSac.Trim().ToLower() == mauSac.TenMauSac.Trim().ToLower() &&
                    m.IdMauSac != mauSac.IdMauSac))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Tên màu sắc đã tồn tại"
                    });
                }

                // Cập nhật thông tin
                var existingMauSac = await _services.GetMauSacById(mauSac.IdMauSac);
                if (existingMauSac == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không tìm thấy màu sắc"
                    });
                }

                // Chỉ cập nhật những trường cần thiết
                existingMauSac.TenMauSac = mauSac.TenMauSac;
                existingMauSac.KichHoat = mauSac.KichHoat;
                existingMauSac.NgayCapNhat = DateTime.Now;
                existingMauSac.NguoiCapNhat = "Admin";

                await _services.Update(existingMauSac);

                return Json(new
                {
                    success = true,
                    id = existingMauSac.IdMauSac.ToString(),
                    tenMauSac = existingMauSac.TenMauSac,
                    kichHoat = existingMauSac.KichHoat
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
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
                return Json(new
                {
                    success = false,
                    message = "Lỗi kết nối: " + ex.Message
                });
            }
            catch (Exception ex)
            {
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
                var mauSac = await _services.GetMauSacById(id);
                if (mauSac == null)
                {
                    return NotFound();
                }
                return View(mauSac);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải chi tiết: " + ex.Message;
                return RedirectToAction(nameof(Getall));
            }
        }
    }
}