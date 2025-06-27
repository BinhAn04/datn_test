using AppData.Model;
using System.ComponentModel.DataAnnotations;

namespace AppAPI.IRepository
{
    public interface IKhachHangRepo
    {
        Task<List<KhachHang>> GetAllKhachHang();
        Task<KhachHang> GetIdKhachHang(Guid id);
        Task<KhachHangDTO> CreateKH(KhachHang kh);
        Task<KhachHang> UpdateKH(KhachHang kh);
        Task<KhachHang> UpdateKHThongTin(KhachHang kh);
        Task<bool> ChangePassword(Guid id, string newPassword);
        Task<bool> ResetPass(string email, string newPassword);
        Task DeleteKH(Guid id);
        Task<KhachHang> GetByEmail(string email);
        Task<KhachHang> GetBySDT(string sdT);
        Task<KhachHang?> Login(string email, string password);
        Task<KhachHang?> GetCustomerByPhoneOrEmailAsync(string phoneOrEmail);
        Task<bool> CheckSDT(string soDienThoai);
        Task<bool> CheckMail(string mail);
        Task<List<KhachHang>> SearchKhachHang(string? name);
    }
}
public class KhachHangDTO
{
    [Required(ErrorMessage = "Không Được Để Trống")]
    public string HoTen { get; set; }

    [RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
    public string SoDienThoai { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Không Đúng Định Dạng")]
    public string Email { get; set; }

    public string MatKhau { get; set; }
}