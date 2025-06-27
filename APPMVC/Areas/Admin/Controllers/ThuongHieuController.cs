using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuongHieuController : Controller
    {
        public IThuongHieuService _services;

        public ThuongHieuController(IThuongHieuService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            List<ThuongHieu> timten = await _services.GetThuongHieu(name);
            if (timten != null)
            {
                var pagedThuongHieus = timten.ToPagedList(page, pageSize);
                return View(pagedThuongHieus);
            }
            else
            {
                List<ThuongHieu> thuongHieus = await _services.GetThuongHieu(name);
                var pagedThuongHieus = thuongHieus.ToPagedList(page, pageSize);
                return View(pagedThuongHieus);
            }
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ThuongHieu thuongHieu)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenThuongHieu"))
                {
                    var error = ModelState["TenThuongHieu"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return RedirectToAction("Getall");
            }
            try
            {
                await _services.Create(thuongHieu);
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
            var thuongHieu = await _services.GetThuongHieuById(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ThuongHieu thuongHieu)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenThuongHieu"))
                {
                    var error = ModelState["TenThuongHieu"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return View(thuongHieu);
            }
            try
            {
                await _services.Update(thuongHieu);
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
            var thuongHieu = await _services.GetThuongHieuById(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }
    }
}