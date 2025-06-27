using AppData.Model;

namespace APPMVC.IService
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllKhachHang();
        Task<KhachHang> GetIdKhachHang(Guid? id);
        Task<KhachHang?> LoginKH(string email, string password);
        Task AddKhachHang(KhachHang kh);
        Task UpdateKhachHang(KhachHang kh);
        Task UpdateKHThongTin(KhachHang kh);
        Task<bool> ChangePassword(Guid? id, string newPassword, string confirmPassword);
        Task DeleteKhachHang(Guid id);
        Task<bool> ResetPassword(string email, string newPassword, string confirmPassword);
        Task<bool> SendVerificationCode(string email);
        Task<string> GetVerificationCodeFromRedisAsync(string email);

        Task<KhachHang?> GetCustomerByPhoneOrEmailAsync(string phoneOrEmail);
        //Check SDT voi Mail
        Task<bool> CheckSDT(string soDienThoai);
        Task<bool> CheckMail(string mail);
        Task<List<KhachHang>> SearchKhachHang(string? name);
    }
}
