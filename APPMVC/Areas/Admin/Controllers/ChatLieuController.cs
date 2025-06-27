using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChatLieuController : Controller
    {
        public IChatLieuService _services;

        public ChatLieuController(IChatLieuService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Getall(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;
            List<ChatLieu> timten = await _services.GetChatLieu(name);
            if (timten != null)
            {
                var pagedChatLieux = timten.ToPagedList(page, pageSize);
                return View(pagedChatLieux);
            }
            else
            {
                List<ChatLieu> chatLieux = await _services.GetChatLieu(name);
                var pagedChatLieux = chatLieux.ToPagedList(page, pageSize);
                return View(pagedChatLieux);
            }
        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatLieu chatLieu)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("TenChatLieu"))
                {
                    var error = ModelState["TenChatLieu"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return RedirectToAction("Getall");
            }
            try
            {
                await _services.Create(chatLieu);
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
            var chatLieu = await _services.GetChatLieuById(id);
            if (chatLieu == null)
            {
                return NotFound();
            }
            return View(chatLieu);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ChatLieu chatLieu)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (ModelState.ContainsKey("TenChatLieu"))
                {
                    var error = ModelState["TenChatLieu"].Errors.FirstOrDefault();
                    if (error != null)
                    {
                        TempData["Error"] = error.ErrorMessage;
                    }
                }
                return View(chatLieu);
            }
            try
            {
                await _services.Update(chatLieu);
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
            var chatLieu = await _services.GetChatLieuById(id);
            if (chatLieu == null)
            {
                return NotFound();
            }
            return View(chatLieu);
        }
    }
}