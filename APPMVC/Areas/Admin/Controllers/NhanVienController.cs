using AppData.Model;
using AppData.ViewModel;
using APPMVC.IService;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using X.PagedList;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _service;
        private readonly IChucVuService chucVuService;
        private readonly IMemoryCache _memoryCache;
        
        public NhanVienController(INhanVienService service, IChucVuService _cvser, IMemoryCache memoryCache)
        {
            _service = service;
            chucVuService = _cvser;
            _memoryCache = memoryCache;
        }
        // GET: NhanVienController
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            page = page < 1 ? 1 : page; 
            int pageSize = 5;

            var nhanVienList = await _service.GetAllNhaVien();

            var chucVuList = await chucVuService.GetAllChucVu(); 

            var nhanVienViewModels = nhanVienList.Select(nv => new NhanVienViewModel
            {
                IdNhanVien = nv.IdNhanVien,
                TenNhanVien = nv.TenNhanVien,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                TenChucVu = chucVuList.FirstOrDefault(c => c.IdChucVu == nv.IdchucVu)?.TenChucVu,
                AnhNhanVien = nv.AnhNhanVien,
                KichHoat = nv.KichHoat,
                TrangThai = nv.TrangThai,
                NgayTao = nv.NgayTao,
                NgayCapNhat = nv.NgayCapNhat
            }).ToList();

            // Thực hiện phân trang
            var pagedNhanViens = nhanVienViewModels.ToPagedList(page, pageSize);

            return View(pagedNhanViens);
        }
        [HttpGet]
        public async Task<IActionResult> SearchNhanVien(string? name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 5;

            // Gọi dịch vụ để tìm kiếm nhân viên
            var nhanVienList = await _service.SearchNhanVien(name);

            // Lấy danh sách chức vụ
            var chucVuList = await chucVuService.GetAllChucVu();

            // Chuyển đổi sang ViewModel
            var nhanVienViewModels = nhanVienList.Select(nv => new NhanVienViewModel
            {
                IdNhanVien = nv.IdNhanVien,
                TenNhanVien = nv.TenNhanVien,
                SoDienThoai = nv.SoDienThoai,
                Email = nv.Email,
                DiaChi = nv.DiaChi,
                TenChucVu = chucVuList.FirstOrDefault(c => c.IdChucVu == nv.IdchucVu)?.TenChucVu,
                AnhNhanVien = nv.AnhNhanVien,
                KichHoat = nv.KichHoat,
                TrangThai = nv.TrangThai,
                NgayTao = nv.NgayTao,
                NgayCapNhat = nv.NgayCapNhat
            }).ToList();

            // Phân trang kết quả tìm kiếm
            var pagedNhanViens = nhanVienViewModels.ToPagedList(page, pageSize);

            return View("Index", pagedNhanViens); // Hiển thị kết quả tìm kiếm trên trang Index
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NhanVienController/Create
        public async Task<IActionResult> Create()
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");

            }
            var chucVuList = await chucVuService.GetAllChucVu();
            ViewBag.ChucVu = chucVuList;

            NhanVien nv = new NhanVien()
            {
                IdNhanVien = Guid.NewGuid(),
                AuthProvider = "Trống",
                NgayCapNhat = DateTime.Now,
                NgayTao = DateTime.Now,
                NguoiCapNhat = "admin",
                NguoiTao = "admin",
            };

            return View(nv);
        }


        [HttpPost]
        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");

            }
            var phoneRegex = new Regex(@"^\d{10}$");
            if (!phoneRegex.IsMatch(nhanVien.SoDienThoai))
            {
                ModelState.AddModelError("", "Đăng ký thất bại! Số điện thoại phải có 10 chữ số.");
                var chucVuList = await chucVuService.GetAllChucVu();
                ViewBag.ChucVu = chucVuList;
                return View(nhanVien);
            }

            // Kiểm tra định dạng email
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(nhanVien.Email))
            {
                ModelState.AddModelError("", "Đăng ký thất bại! Email không đúng định dạng.");
                var chucVuList = await chucVuService.GetAllChucVu();
                ViewBag.ChucVu = chucVuList;
                return View(nhanVien);
            }
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Đăng ký thất bại! Vui lòng nhập đầy đủ thông tin";
                var chucVuList = await chucVuService.GetAllChucVu();
                ViewBag.ChucVu = chucVuList;
                return View(nhanVien);

            }
            // Kiểm tra số điện thoại và email
            if (await _service.CheckSDT(nhanVien.SoDienThoai))
            {
                ModelState.AddModelError("", "Đăng ký thất bại! Số điện thoại đã tồn tại.");
                var chucVuList = await chucVuService.GetAllChucVu();
                ViewBag.ChucVu = chucVuList;
                return View(nhanVien);
            }

            if (await _service.CheckMail(nhanVien.Email))
            {
                ModelState.AddModelError("", "Đăng ký thất bại! Email này đã tồn tại.");
                var chucVuList = await chucVuService.GetAllChucVu();
                ViewBag.ChucVu = chucVuList;
                return View(nhanVien);
            }

           
            nhanVien.MatKhau = GenerateRandomPassword(8);
             await _service.CreateNV(nhanVien);
             return RedirectToAction("Index");
        }
       

        public async Task<IActionResult> Edit(Guid id)
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");

            }
            var nhanVien = await _service.GetIdNhanVien(id);
                if (nhanVien == null)
                {
                    return NotFound();
                }

                List<ChucVu> chucVus = await chucVuService.GetAllChucVu();

                var viewModel = new NhanVienViewModel
                {
                    NhanVien = nhanVien,
                    ChucVuList = chucVus
                };

                return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NhanVienViewModel model, IFormFile? imgFile)
        {
            if (ModelState.IsValid)
            {
                
                var existingNV = await _service.GetIdNhanVien(model.NhanVien.IdNhanVien);

                if (existingNV == null)
                {
                    ModelState.AddModelError("", "Nhân viên không tồn tại.");
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }
                var phoneRegex = new Regex(@"^\d{10}$");
                if (!phoneRegex.IsMatch(model.NhanVien.SoDienThoai))
                {
                    ModelState.AddModelError("", "Số điện thoại phải có 10 chữ số.");
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }

                var isPhoneExists = await _service.CheckSDT(model.NhanVien.SoDienThoai);
                if (isPhoneExists && model.NhanVien.SoDienThoai != existingNV.SoDienThoai)
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại.");
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }

                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(model.NhanVien.Email))
                {
                    ModelState.AddModelError("", "Email không đúng định dạng.");
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }

                var isMailExists = await _service.CheckMail(model.NhanVien.Email);
                if(isMailExists && model.NhanVien.Email != existingNV.Email)
                {
                    ModelState.AddModelError("", "Email đã tồn tại.");
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }
                
                if (imgFile != null && imgFile.Length > 0)
                {
                    var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var ext = Path.GetExtension(imgFile.FileName).ToLowerInvariant();

                    if (!permittedExtensions.Contains(ext))
                    {
                        ModelState.AddModelError("", "Chỉ chấp nhận các file ảnh với định dạng .jpg, .jpeg, .png, .gif.");
                        model.ChucVuList = await chucVuService.GetAllChucVu();
                        return View(model);
                    }

                    if (imgFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "File ảnh phải nhỏ hơn 5MB.");
                        model.ChucVuList = await chucVuService.GetAllChucVu();
                        return View(model);
                    }

                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Admin", "imgNV");

                   
                    if (!string.IsNullOrEmpty(existingNV.AnhNhanVien))
                    {
                        string oldImagePath = Path.Combine(uploadFolder, existingNV.AnhNhanVien);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    
                    string newImagePath = Path.Combine(uploadFolder, imgFile.FileName);

                    
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }

                    
                    model.NhanVien.AnhNhanVien = imgFile.FileName;
                }
                else
                {
                    // Nếu không có file ảnh mới, giữ nguyên giá trị cũ
                    model.NhanVien.AnhNhanVien = existingNV.AnhNhanVien;
                }

                // Cập nhật thông tin nhân viên
                try
                {
                    await _service.UpdateNV(model.NhanVien);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin nhân viên: " + ex.Message);
                    model.ChucVuList = await chucVuService.GetAllChucVu();
                    return View(model);
                }
            }
            else
            {
                // Repopulate the ChucVuList if the model state is invalid
                model.ChucVuList = await chucVuService.GetAllChucVu();
                return View(model);
            }
        }


        //Sửa Thông Tin Của Nhân Viên
        public async Task<IActionResult> EditProfile(Guid id)
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");

            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }
            var nhanVien = JsonConvert.DeserializeObject<NhanVien>(sessionData);
            id = nhanVien.IdNhanVien;
            var idNhanVien = await _service.GetIdNhanVien(id);
            if(nhanVien == null)
            {
                return RedirectToAction("Error");
            }

            // Trả về view với thông tin khách hàng
            return View(nhanVien);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(NhanVien nv, IFormFile? imgFile)
        {
            // Lấy thông tin nhân viên cũ
            var existingNV = await _service.GetIdNhanVien(nv.IdNhanVien);

            if (ModelState.IsValid)
            {
                // Kiểm tra định dạng số điện thoại
                var phoneRegex = new Regex(@"^\d{10}$"); // Giả sử số điện thoại có 10 chữ số
                if (!phoneRegex.IsMatch(nv.SoDienThoai))
                {
                    ModelState.AddModelError("SoDienThoai", "Số điện thoại phải có 10 chữ số.");
                    return View(existingNV); // Trả về thông tin cũ
                }

                // Kiểm tra tính duy nhất của số điện thoại
                var isPhoneExists = await _service.CheckSDT(nv.SoDienThoai);
                if (isPhoneExists && existingNV.SoDienThoai != nv.SoDienThoai)
                {
                    ModelState.AddModelError("SoDienThoai", "Số điện thoại đã tồn tại.");
                    return View(existingNV); // Trả về thông tin cũ
                }

                // Kiểm tra định dạng email
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(nv.Email))
                {
                    ModelState.AddModelError("Email", "Email không đúng định dạng.");
                    return View(existingNV); // Trả về thông tin cũ
                }

                // Kiểm tra tính duy nhất của email
                var isEmailExists = await _service.CheckMail(nv.Email);
                if (isEmailExists && existingNV.Email != nv.Email)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại.");
                    return View(existingNV); // Trả về thông tin cũ
                }

                // Kiểm tra xem có file ảnh mới được tải lên không
                if (imgFile != null && imgFile.Length > 0)
                {
                    // Kiểm tra loại file (chỉ chấp nhận các định dạng ảnh)
                    var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var ext = Path.GetExtension(imgFile.FileName).ToLowerInvariant();

                    // Kiểm tra phần mở rộng có hợp lệ không
                    if (!permittedExtensions.Contains(ext))
                    {
                        ModelState.AddModelError("", "Chỉ chấp nhận các file ảnh với định dạng .jpg, .jpeg, .png, .gif.");
                        return View(existingNV); // Trả về thông tin cũ
                    }

                    // Kiểm tra kích thước file (giới hạn 5MB)
                    if (imgFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "File ảnh phải nhỏ hơn 5MB.");
                        return View(existingNV); // Trả về thông tin cũ
                    }

                    // Tạo đường dẫn đến thư mục lưu trữ hình ảnh
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Admin", "imgNV");

                    // Xóa ảnh cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(existingNV.AnhNhanVien))
                    {
                        string oldImagePath = Path.Combine(uploadFolder, existingNV.AnhNhanVien);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Tạo đường dẫn đến file ảnh mới
                    string newImagePath = Path.Combine(uploadFolder, imgFile.FileName);

                    // Tạo file stream để lưu file hình ảnh
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await imgFile.CopyToAsync(stream);
                    }

                    // Gán tên file vào thuộc tính AnhNhanVien của đối tượng NhanVien
                    nv.AnhNhanVien = imgFile.FileName;
                }
                else
                {
                    // Nếu không có file ảnh mới, giữ nguyên giá trị cũ
                    nv.AnhNhanVien = existingNV.AnhNhanVien;
                }

                // Cập nhật thông tin nhân viên
                await _service.UpdateThongTin(nv);

                // Cập nhật session với thông tin mới
                var updatedNV = await _service.GetIdNhanVien(nv.IdNhanVien);
                var chucVu = await chucVuService.GetChucVuId(updatedNV.IdchucVu);
                HttpContext.Session.SetString("NhanVien", JsonConvert.SerializeObject(updatedNV));
                HttpContext.Session.SetString("AvatarUrl", updatedNV.AnhNhanVien);
                HttpContext.Session.SetString("NhanVienName", updatedNV.TenNhanVien);
                HttpContext.Session.SetString("IdNhanVien", updatedNV.IdNhanVien.ToString());
                HttpContext.Session.SetString("NhanVienRole", chucVu != null ? chucVu.Code : "Không xác định"); // Vai trò nhân viên

                return RedirectToAction("MyProfile", "NhanVien");
            }

            // Nếu model không hợp lệ, trả về thông tin hiện tại
            return View(existingNV);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteNV(id);
            return RedirectToAction("Index");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Thực hiện đăng nhập
                    var kh = await _service.LoginKH(nv.Email, nv.MatKhau);
                    if (kh != null)
                    {
                        if (kh.TrangThai == 0)  // Kiểm tra nếu trạng thái là 0
                        {
                            ModelState.AddModelError("", "Tài khoản của bạn chưa được kích hoạt.");
                            return View(nv);
                        }

                        var chucVu = await chucVuService.GetChucVuId(kh.IdchucVu);
                        // Lưu thông tin khách hàng vào session
                        HttpContext.Session.SetString("NhanVien", JsonConvert.SerializeObject(kh));
                        HttpContext.Session.SetString("AvatarUrl", kh.AnhNhanVien); 
                        HttpContext.Session.SetString("NhanVienName", kh.TenNhanVien);
                        HttpContext.Session.SetString("IdNhanVien", kh.IdNhanVien.ToString());
                        HttpContext.Session.SetString("NhanVienRole", chucVu != null ? chucVu.Code : "Không xác định"); // Vai trò nhân viên
                        return RedirectToAction("Index", "BanHangTQ");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu. Vui lòng kiểm tra lại.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(nv);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();      
            return RedirectToAction("Login");
        }

        public IActionResult DangNhapThanhCong()
        {
            // Lấy dữ liệu từ Session
            var sessionData = HttpContext.Session.GetString("NhanVienRole");

            // Trả về view với dữ liệu từ Session
            return View((object)sessionData);
        }

        // My Profile
        public async Task<IActionResult> MyProfile()
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");

            }
            var nhanVien = JsonConvert.DeserializeObject<NhanVien>(sessionData);
            if (nhanVien.IdchucVu.HasValue)
            {
                var chucVu = await chucVuService.GetIdChucVu(nhanVien.IdchucVu.Value);
                ViewBag.TenChucVu = chucVu?.TenChucVu ?? "Chức vụ không xác định";
            }
            else
            {
                ViewBag.TenChucVu = "Chức vụ không xác định";
            }


            return View(nhanVien);
        }
        public async Task<IActionResult> ChangePassword()
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");

            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(Guid id, string newPassword, string confirmPassword)
        {
            var sessionData = HttpContext.Session.GetString("NhanVien");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }

            var nhanVien = JsonConvert.DeserializeObject<NhanVien>(sessionData);

            // Check if new passwords match
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return View();
            }

            // Validate password complexity
            if (!IsValidPassword(newPassword))
            {
                ModelState.AddModelError("NewPassword", "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.");
                return View();
            }

            var result = await _service.DoiMK(nhanVien.IdNhanVien, newPassword, confirmPassword);
            if (result)
            {
                nhanVien.MatKhau = newPassword; // Ensure this is hashed
                HttpContext.Session.SetString("NhanVien", JsonConvert.SerializeObject(nhanVien));
                ViewBag.Message = "Password changed successfully";
                return RedirectToAction("MyProfile");
            }

            ModelState.AddModelError("", "Đổi mật khẩu thất bại. Vui lòng kiểm tra lại.");
            return View();
        }

        // Password validation method
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpperChar = password.Any(char.IsUpper);
            bool hasLowerChar = password.Any(char.IsLower);
            bool hasNumericChar = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => "!@#$%^&*()_+-=<>?".Contains(ch));

            return hasUpperChar && hasLowerChar && hasNumericChar && hasSpecialChar;
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Vui lòng nhập email.");
                return View();
            }

            var result = await _service.SendVerificationCode(email);

            if (result)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1) // Đặt thời gian hết hạn cho cookie
                };
                Response.Cookies.Append("Email", email, cookieOptions);
                return RedirectToAction("VerifyCode");
            }

            // Nếu gửi mã xác thực không thành công, lưu email vào cookie
            var errorCookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1) // Đặt thời gian hết hạn cho cookie
            };
            Response.Cookies.Append("Email", email, errorCookieOptions);

            ModelState.AddModelError("", "Không thể gửi mã xác thực. Vui lòng thử lại.");
            ViewBag.Email = Request.Cookies["Email"]; // Lấy email từ Cookie nếu có
            return View();
        }
        public IActionResult VerifyCode()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyCode(string code, string email)
        {
           var verifycationCode = await _service.GetVerificationCodeFromRedisAsync(email);
            var mmail = Request.Cookies["Email"];
            if (code == verifycationCode)
            {
                mmail = email;
                return RedirectToAction("ResetPassword");
            }
            ModelState.AddModelError("", "Mã xác thực không chính xác hoặc đã hết hạn");
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RestPassword(string email, string newPassword, string confirmPassword)
        {
            // Check if new passwords match
            if (newPassword != confirmPassword)
            {
                TempData["Error33"] = "Mật khẩu không khớp! Vui lòng thử lại";
                return RedirectToAction("ResetPassword", "NhanVien");
            }

            // Validate password complexity
            if (!IsValidPassword(newPassword))
            {
                TempData["Error33"] = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt. Vui lòng thử lại";
                return RedirectToAction("ResetPassword", "NhanVien");
            }

            var result = await _service.RestPassword(email, newPassword, confirmPassword);
            if (result)
            {
                TempData["DoiMK"] = "Đổi mật khẩu thành công.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Đổi mật khẩu thất bại. Vui lòng thử lại.");
            return View();
        }
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
