using Microsoft.AspNetCore.Mvc;
using APPMVC.IService;
using AppData.Model;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace APPMVC.Controllers
{
    [Area("Admin")]
    public class HinhAnhController : Controller
    {
        private readonly IHinhAnhService _hinhAnhService;
        private readonly ILogger<HinhAnhController> _logger;

        public HinhAnhController(IHinhAnhService hinhAnhService, ILogger<HinhAnhController> logger)
        {
            _hinhAnhService = hinhAnhService ?? throw new ArgumentNullException(nameof(hinhAnhService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var hinhAnhs = await _hinhAnhService.GetHinhAnhsAsync();
                return View(hinhAnhs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching HinhAnh list");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách hình ảnh.";
                return View(new List<HinhAnh>());
            }
        }

        public IActionResult Upload()
        {
            // Display any messages from TempData if they exist
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Upload request started - File name: {file?.FileName}, File size: {file?.Length} bytes");

                // Log the content type for debugging
                _logger.LogInformation($"Received file: {file.FileName}, ContentType: {file.ContentType}");

                var allowedTypes = new List<string> { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(file.ContentType))
                {
                    ModelState.AddModelError("file", "The selected file type is not supported.");
                    return View();
                }

                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("Upload failed: File is null or empty");
                    TempData["ErrorMessage"] = "Vui lòng chọn file để tải lên.";
                    return View();
                }

                if (!file.ContentType.StartsWith("image/"))
                {
                    _logger.LogWarning($"Upload failed: Invalid file type - {file.ContentType}");
                    TempData["ErrorMessage"] = "File được chọn không phải là hình ảnh hợp lệ.";
                    return View();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    _logger.LogInformation($"File successfully read into memory - Size: {fileBytes.Length} bytes");

                    var hinhAnh = new HinhAnh
                    {
                        IdHinhAnh = Guid.NewGuid(),
                        LoaiFileHinhAnh = file.ContentType,
                        DataHinhAnh = fileBytes,
                        TrangThai = 1
                    };

                    _logger.LogInformation($"Created HinhAnh object - Id: {hinhAnh.IdHinhAnh}, ContentType: {hinhAnh.LoaiFileHinhAnh}");

                    try
                    {
                        var result = await _hinhAnhService.UploadAsync(hinhAnh);
                        _logger.LogInformation($"UploadAsync result: {result}");

                        if (result)
                        {
                            TempData["SuccessMessage"] = "Tải lên hình ảnh thành công!";
                            return RedirectToAction(nameof(Index)); // Redirect to Index on success
                        }
                        else
                        {
                            _logger.LogWarning("UploadAsync returned false");
                            TempData["ErrorMessage"] = "Không thể tải lên hình ảnh. Vui lòng thử lại sau.";
                        }
                    }
                    catch (Exception uploadEx)
                    {
                        _logger.LogError(uploadEx, "Error in UploadAsync service call");
                        TempData["ErrorMessage"] = $"Lỗi khi tải lên: {uploadEx.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in Upload action");
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return View();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var hinhAnh = await _hinhAnhService.GetHinhAnhByIdAsync(id);
                if (hinhAnh != null)
                {
                    var result = await _hinhAnhService.DeleteAsync(id);
                    if (result)
                    {
                        _logger.LogInformation($"Successfully deleted image with Id: {id}");
                        TempData["SuccessMessage"] = "Image deleted successfully.";
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to delete image with Id: {id}");
                        TempData["ErrorMessage"] = "Failed to delete the image.";
                    }
                }
                else
                {
                    _logger.LogWarning($"Attempted to delete non-existent image with Id: {id}");
                    TempData["ErrorMessage"] = "Image not found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting image with Id: {id}");
                TempData["ErrorMessage"] = "An error occurred while deleting the image.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetHinhAnhById(Guid id)
        {
            try
            {
                var hinhAnh = await _hinhAnhService.GetHinhAnhByIdAsync(id);
                if (hinhAnh == null)
                {
                    _logger.LogWarning($"Image not found for Id: {id}");
                    return NotFound("Image not found.");
                }
                return File(hinhAnh.DataHinhAnh, hinhAnh.LoaiFileHinhAnh);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching image with Id: {id}");
                return StatusCode(500, "An error occurred while retrieving the image.");
            }
        }
    }
}