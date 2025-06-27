using AppData.Model;
using APPMVC.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APPMVC.Areas.Client.Controllers
{
    [Area("Client")]
    public class KhachHangController : Controller
    {
        
        private readonly IKhachHangService _service;
        private readonly IDiaChiService _dcService;
        private readonly ICardService _cardService;
        public KhachHangController(IKhachHangService service, IDiaChiService diaChiService, ICardService cardService)
        {
            _service = service;
            _dcService = diaChiService;
            _cardService = cardService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Register(KhachHang kh = null)
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values
                                        .SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                TempData["Error"] = "Đăng ký thất bại! " + string.Join(" ", allErrors);
                return RedirectToAction("Login");
            }

            // Validate password (assuming kh contains a Password property)
            if (!IsValidPassword(kh.MatKhau))
            {
                TempData["Error"] = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.";
                return RedirectToAction("Login");
            }

            try
            {
                var checkSdt = await _service.CheckSDT(kh.SoDienThoai);
                if (checkSdt)
                {
                    TempData["Error"] = "Đăng ký thất bại! Số điện thoại đã tồn tại";
                    return RedirectToAction("Login");
                }

                var checkEmail = await _service.CheckMail(kh.Email);
                if (checkEmail)
                {
                    TempData["Error"] = "Đăng ký thất bại! Email này đã tồn tại";
                    return RedirectToAction("Login");
                }

                await _service.AddKhachHang(kh);
                TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Đăng ký thất bại! Không được bỏ trống thông tin";
                return RedirectToAction("Login");
            }
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

        public IActionResult DangKyThanhCong()
        {
            return Content("OKroi");
        }
        public IActionResult DangNhapThanhCong()
        {
            // Lấy dữ liệu từ Session
            var sessionData = HttpContext.Session.GetString("KhachHang");

            // Trả về view với dữ liệu từ Session
            return View((object)sessionData);
        }
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(KhachHang khach)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Thực hiện đăng nhập
					var kh = await _service.LoginKH(khach.Email, khach.MatKhau);
					if (kh != null)
					{
                        if (kh.KichHoat == 0)  // Kiểm tra nếu trạng thái là 0
                        {
                            TempData["Error"] = ("Tài khoản của bạn đã bị khóa.");
                            return View(khach);
                        }
                        // Lưu thông tin khách hàng vào session
                        HttpContext.Session.SetString("KhachHang", JsonConvert.SerializeObject(kh));
						HttpContext.Session.SetString("TenKhachHang", kh.HoTen);
						HttpContext.Session.SetString("IdKhachHang", kh.IdKhachHang.ToString()); // Chuyển đổi sang chuỗi
						return RedirectToAction("Index", "HomeClient", new { area = "Client" }); // Chuyển hướng đến trang Index
					}
					else
					{
						ModelState.AddModelError("", "Đăng nhập thất bại. Kiểm tra lại email hoặc mật khẩu.");
					}
				}
				catch (Exception ex)
				{
					TempData["Error"] = "Đăng nhập thất bại vui lòng kiểm tra lại thông tin của bạn";
					return RedirectToAction("Login");
				}
			}

			var errors = ModelState.Values.SelectMany(v => v.Errors);
			foreach (var error in errors)
			{
				Console.WriteLine(error.ErrorMessage);
			}
			return View(khach);
		}


		public IActionResult ThongTinKhachHang()
        {
            var sessionData = HttpContext.Session.GetString("KhachHang");

            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }
            var khachHang = JsonConvert.DeserializeObject<KhachHang>(sessionData);

            return View(khachHang);
        }
        public async Task<IActionResult> Edit()
		{
			// Lấy thông tin KhachHang từ session
			var sessionData = HttpContext.Session.GetString("KhachHang");

			if (string.IsNullOrEmpty(sessionData))
			{
				// Nếu session trống, điều hướng về trang đăng nhập
				return RedirectToAction("Login");
			}

			// Deserialize sessionData để chuyển thành đối tượng KhachHang
			var khachHang = JsonConvert.DeserializeObject<KhachHang>(sessionData);
           

			// Trả về view với thông tin khách hàng
			return View(khachHang);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(KhachHang khachHang)
		{
            
			if (ModelState.IsValid)
			{
				try
				{
					await _service.UpdateKHThongTin(khachHang);
					HttpContext.Session.SetString("KhachHang", JsonConvert.SerializeObject(khachHang));
					HttpContext.Session.SetString("TenKhachHang", khachHang.HoTen);
					//HttpContext.Session.SetString("IdKhachHang", JsonConvert.SerializeObject(khachHang.IdKhachHang));
					return RedirectToAction("ThongTinKhachHang");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return View(khachHang);
		}
        // Doi MK
        public async Task<IActionResult> ChangePassword()
        {
            var sessionData = HttpContext.Session.GetString("KhachHang");

            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(Guid id, string newPassword, string confirmPassword)
        {
            var sessionData = HttpContext.Session.GetString("KhachHang");
            if (string.IsNullOrEmpty(sessionData))
            {
                return RedirectToAction("Login");
            }

            var khachHang = JsonConvert.DeserializeObject<KhachHang>(sessionData);

            // Check if new passwords match
            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Mật khẩu xác nhận không khớp.";
                return View();
            }

            // Validate password complexity
            if (!IsValidPassword(newPassword))
            {
                TempData["Error"] = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt.";
                return View();
            }

            var result = await _service.ChangePassword(khachHang.IdKhachHang, newPassword, confirmPassword);
            if (result)
            {
                khachHang.MatKhau = newPassword; // Ensure this is hashed before storing
                HttpContext.Session.SetString("KhachHang", JsonConvert.SerializeObject(khachHang));
                TempData["DoiMK"] = "Đổi Mật Khẩu Thành Công";
                return RedirectToAction("ThongTinKhachHang");
            }

            TempData["Error"] = "Đổi mật khẩu thất bại. Vui lòng kiểm tra lại.";
            return View();
        }

        // Password validation method
      
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
        public async Task<IActionResult> ResetPassword(string email, string newPassword, string confirmPassword)
        {
            // Check if new passwords match
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp! Vui lòng thử lại");
                return View();
            }

            // Validate password complexity
            if (!IsValidPassword(newPassword))
            {
                ModelState.AddModelError("", "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt. Vui lòng thử lại");
                return View();
            }

            var result = await _service.ResetPassword(email, newPassword, confirmPassword);
            if (result)
            {

                TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Đổi mật khẩu thất bại. Vui lòng thử lại.";
            return RedirectToAction("Login", "KhachHang");
        }

        public IActionResult Logout()
		{
			HttpContext.Session.Clear();

			TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công.";

			return RedirectToAction("Login");
		}

        //Lấy Địa Chỉ
	}
}
