using AppData.Model;

namespace AppAPI.IService
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetAllKhachHang();
        Task<KhachHang> GetIdKhachHang(Guid id);

        Task<KhachHangDTO> AddKhachHang(KhachHang kh);
        Task<KhachHang> UpdateKhachHang(KhachHang kh);
        Task<KhachHang> UpdateKHThongTin(KhachHang kh);
        Task<bool> ChangePassword(Guid id, string newPassword);
        Task<bool> ResetPass(string email, string newPassword);
        Task<KhachHang> GetByEmail(string email);
        Task<KhachHang?> Login(string email, string password);   
        Task DeleteKhachHang(Guid id);
        Task<KhachHang?> GetCustomerByPhoneOrEmailAsync(string phoneOrEmail);
        //Check
        Task<bool> CheckSDT(string soDienThoai);
        Task<bool> CheckMail(string mail);
        Task<List<KhachHang>> SearchKhachHang(string? name);

    }
}
