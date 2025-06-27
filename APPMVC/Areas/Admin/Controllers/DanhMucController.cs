using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucController : Controller
    {
        public IDanhMucService _services;

        public DanhMucController(IDanhMucService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            List<DanhMuc> timten = await _services.GetDanhMuc(name);
            if (timten != null)
            {
                var pagedDanhMucs = timten.ToPagedList(page, pageSize);
                return View(pagedDanhMucs);
            }
            else
            {
                List<DanhMuc> danhMucs = await _services.GetDanhMuc(name);
                var pagedDanhMucs = danhMucs.ToPagedList(page, pageSize);
                return View(pagedDanhMucs);
            }
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenDanhMuc"))
                {
                    var error = ModelState["TenDanhMuc"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return RedirectToAction("Getall");
            }
            try
            {
                await _services.Create(danhMuc);
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
            var danhMuc = await _services.GetDanhMucById(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenDanhMuc"))
                {
                    var error = ModelState["TenDanhMuc"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return View(danhMuc);
            }
            try
            {
                await _services.Update(danhMuc);
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
            var danhMuc = await _services.GetDanhMucById(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return View(danhMuc);
        }
    }
}