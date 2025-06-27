using AppData.Model;

namespace APPMVC.IService
{
    public interface INhanVienService
    {
        Task<List<NhanVien>> GetAllNhaVien();
        Task<NhanVien> GetIdNhanVien(Guid id);
        Task<NhanVien?> LoginKH(string email, string password);
        Task CreateNV(NhanVien nv);
        Task UpdateNV(NhanVien nv);
        Task UpdateThongTin(NhanVien nv);
        Task DeleteNV(Guid id);
        Task<bool> DoiMK(Guid id, string newPassword, string confirmPassword);
        Task<bool> RestPassword(string email, string newPassword, string confirmPassword);
        Task<bool> SendVerificationCode(string email);
        Task<string> GetVerificationCodeFromRedisAsync(string email);
        Task<bool> CheckSDT(string soDienThoai);
        Task<bool> CheckMail(string mail);
        Task<List<NhanVien>> SearchNhanVien(string? name);

    }
}
