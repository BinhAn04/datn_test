using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KieuDangController : Controller
    {
        public IKieuDangService _services;

        public KieuDangController(IKieuDangService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            List<KieuDang> timten = await _services.GetKieuDang(name);
            if (timten != null)
            {
                var pagedKieuDangs = timten.ToPagedList(page, pageSize);
                return View(pagedKieuDangs);
            }
            else
            {
                List<KieuDang> kieuDangs = await _services.GetKieuDang(name);
                var pagedKieuDangs = kieuDangs.ToPagedList(page, pageSize);
                return View(pagedKieuDangs);
            }
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(KieuDang kieuDang)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenKieuDang"))
                {
                    var error = ModelState["TenKieuDang"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return RedirectToAction("Getall");
            }
            try
            {
                await _services.Create(kieuDang);
                TempData["Success"] = "Thêm mới thành công";
            }
            catch (Exception)
            {
                TempData["Error"] = "Thêm mới thất bại";
            }
            return RedirectToAction("Getall");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var kieuDang = await _services.GetKieuDangById(id);
            if (kieuDang == null)
            {
                return NotFound();
            }
            return View(kieuDang);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(KieuDang kieuDang)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenKieuDang"))
                {
                    var error = ModelState["TenKieuDang"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return View(kieuDang);
            }
            try
            {
                await _services.Update(kieuDang);
                TempData["Success"] = "Cập nhật thành công";
            }
            catch (Exception)
            {
                TempData["Error"] = "Cập nhật thất bại";
            }
            return RedirectToAction("Getall");
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _services.Delete(id);
                TempData["Success"] = "Xóa thành công";
            }
            catch (Exception)
            {
                TempData["Error"] = "Xóa thất bại";
            }
            return RedirectToAction("Getall");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var kieuDang = await _services.GetKieuDangById(id);
            if (kieuDang == null)
            {
                return NotFound();
            }
            return View(kieuDang);
        }
    }
}