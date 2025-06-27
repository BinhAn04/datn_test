using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChucVuController : Controller
    {
        private readonly IChucVuService _service;
        public ChucVuController(IChucVuService service)
        {
            _service = service;
        }
        // GET: ChucVuController
        public async Task<IActionResult> Index()
        {
            List<ChucVu> cv = await _service.GetAllChucVu();
            return View(cv);
        }

        // GET: ChucVuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChucVuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChucVuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChucVuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChucVuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChucVuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChucVuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
