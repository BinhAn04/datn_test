using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeGiayController : Controller
    {
        public IDeGiayService _services;

        public DeGiayController(IDeGiayService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;

            List<DeGiay> timten = await _services.GetDeGiay(name);

            if (timten != null && timten.Any())
            {
                // Sắp xếp timten theo ngày tạo từ trên xuống dưới
                timten = timten.OrderByDescending(dg => dg.NgayTao).ToList();
                var pagedDeGiays = timten.ToPagedList(page, pageSize);
                return View(pagedDeGiays);
            }
            else
            {
                List<DeGiay> deGiays = await _services.GetDeGiay(name);

                // Sắp xếp deGiays theo ngày tạo từ trên xuống dưới
                deGiays = deGiays.OrderByDescending(dg => dg.NgayTao).ToList();
                var pagedDeGiays = deGiays.ToPagedList(page, pageSize);
                return View(pagedDeGiays);
            }
        }
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeGiay deGiay)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenDeGiay"))
                {
                    var error = ModelState["TenDeGiay"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return RedirectToAction("Getall");
            }
            try
            {
                await _services.Create(deGiay);
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
            var deGiay = await _services.GetDeGiayById(id);
            if (deGiay == null)
            {
                return NotFound();
            }
            return View(deGiay);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DeGiay deGiay)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenDeGiay"))
                {
                    var error = ModelState["TenDeGiay"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return View(deGiay);
            }
            try
            {
                await _services.Update(deGiay);
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
            var deGiay = await _services.GetDeGiayById(id);
            if (deGiay == null)
            {
                return NotFound();
            }
            return View(deGiay);
        }
    }
}