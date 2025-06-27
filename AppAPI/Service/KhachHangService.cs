using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;

namespace AppAPI.Service
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepo _repo;
        public KhachHangService(IKhachHangRepo repo)
        {
            _repo = repo;
        }
        public async Task<KhachHangDTO> AddKhachHang(KhachHang kh)
        {
            var existingCustomer = await _repo.GetByEmail(kh.Email);
            if (existingCustomer != null)
            {
                throw new Exception("Mail đã tồn tại");
            }
            return await _repo.CreateKH(kh);
        }
        public async Task<KhachHang?> Login(string email,string password)
        {
            return await _repo.Login(email, password);
        }
        public async Task DeleteKhachHang(Guid id)
        {
             await _repo.DeleteKH(id);
        }

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
           return await _repo.GetAllKhachHang();
        }

        public async Task<KhachHang> GetIdKhachHang(Guid id)
        {
            return await _repo.GetIdKhachHang(id);
        }
        public async Task<KhachHang> GetByEmail(string email)
        {
            return await _repo.GetByEmail(email);
        }
        public async Task<KhachHang> UpdateKhachHang(KhachHang kh)
        {
            return await _repo.UpdateKH(kh);
        }
        public async Task<KhachHang> UpdateKHThongTin(KhachHang kh)
        {
            return await _repo.UpdateKHThongTin(kh);
        }
        public async Task<bool> ChangePassword(Guid id, string newPassword)
        {
            return await _repo.ChangePassword(id, newPassword);
        }
        public async Task<bool> ResetPass(string email, string newPassword)
        {
            return await _repo.ResetPass(email, newPassword);
        }

        public async Task<KhachHang?> GetCustomerByPhoneOrEmailAsync(string phoneOrEmail)
        {
            return await _repo.GetCustomerByPhoneOrEmailAsync(phoneOrEmail);
        }
        public async Task<bool> CheckSDT(string soDienThoai)
        {
            return await _repo.CheckSDT(soDienThoai);
        }
        public async Task<bool> CheckMail(string mail)
        {
            return await _repo.CheckMail(mail);
        }
        public async Task<List<KhachHang>> SearchKhachHang(string? name)
        {
            return await _repo.SearchKhachHang(name);
        }
    }
}
