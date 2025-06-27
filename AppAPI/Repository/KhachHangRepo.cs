using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class KhachHangRepo : IKhachHangRepo
    {
        private readonly AppDbcontext _context;
        public KhachHangRepo()
        {
            _context = new AppDbcontext();
        }
        public async Task<KhachHangDTO> CreateKH(KhachHang kh)
        {
            // Create a new KhachHang instance
            KhachHang newKh = new KhachHang
            {
                IdKhachHang = Guid.NewGuid(),
                HoTen = kh.HoTen,
                SoDienThoai = kh.SoDienThoai,
                Email = kh.Email,
                AuthProvider = "1",
                MatKhau = kh.MatKhau,
                NgayTao = DateTime.UtcNow,
                NgayCapNhat = DateTime.UtcNow,
                NguoiTao = "Client",
                NguoiCapNhat = "Client",
                KichHoat = 1
            };

            // Create and associate a new GioHang
            GioHang gioHang = new GioHang()
            {
                IdGioHang = Guid.NewGuid(),
                IdKhachHang = newKh.IdKhachHang,
            };
            newKh.GioHang = gioHang;

            // Add the new customer to the context and save changes
            _context.khachHangs.Add(newKh);
            await _context.SaveChangesAsync();

            // Map KhachHang to KhachHangDTO to return
            var khachHangDto = new KhachHangDTO
            {
                HoTen = newKh.HoTen,
                SoDienThoai = newKh.SoDienThoai,
                Email = newKh.Email,
                MatKhau = newKh.MatKhau 
            };

            return khachHangDto;
        }

        public async Task DeleteKH(Guid id)
        {
            var khachHang = await _context.khachHangs.FindAsync(id);
            if(khachHang != null )
            {
                _context.khachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<KhachHang>> GetAllKhachHang()
        {
            return await _context.khachHangs.ToListAsync();
        }

        public async Task<KhachHang> GetByEmail(string email)
        {
            return await _context.khachHangs.FirstOrDefaultAsync(kh => kh.Email == email);
        }

        public async Task<KhachHang> GetBySDT(string sdT)
        {
            return await _context.khachHangs.FirstOrDefaultAsync(kh => kh.SoDienThoai == sdT);
        }

        public async Task<KhachHang> GetIdKhachHang(Guid id)
        {
            return await _context.khachHangs.FindAsync(id);
        }

        public async Task<KhachHang> UpdateKH(KhachHang kh)
        {

            _context.Entry(kh).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return kh;
        }
        public async Task<KhachHang> UpdateKHThongTin(KhachHang kh)
        {
            _context.khachHangs.Attach(kh);
            _context.Entry(kh).Property(x => x.HoTen).IsModified = true;
            _context.Entry(kh).Property(x => x.SoDienThoai).IsModified = true;
            _context.Entry(kh).Property(x => x.Email).IsModified = true;

            await _context.SaveChangesAsync();            
            return kh;
        }
        public async Task<bool> ChangePassword(Guid id,string newPassword)
        {

            var ChangePassword = _context.khachHangs.Find(id);
            if(ChangePassword == null) return false;
            ChangePassword.MatKhau = newPassword;
            _context.khachHangs.Update(ChangePassword);
            _context.SaveChangesAsync();
            return true;    
        }
        public async Task<bool> ResetPass(string email, string newPassword)
        {
            var khachHang = _context.khachHangs.FirstOrDefault(e => e.Email == email);
            if (khachHang == null) return false;
            khachHang.MatKhau = newPassword;
            _context.khachHangs.Update(khachHang);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<KhachHang?> Login(string email, string password)
        {
            var kh = await _context.khachHangs.FirstOrDefaultAsync(kh => kh.Email == email);
            if (kh == null)
            {
                throw new Exception("Email Không tồn tại");
            }
            //Kiểm tra mk
            if(kh.MatKhau != password)
            {
                throw new Exception("Mật Khẩu Không Chính Xác");
            }
            
            //Hợp lệ trả về kh
            return kh;  
        }

        public async Task<KhachHang?> GetCustomerByPhoneOrEmailAsync(string phoneOrEmail)
        {
            var customer = await _context.khachHangs
                .FirstOrDefaultAsync(kh => kh.Email == phoneOrEmail || kh.SoDienThoai == phoneOrEmail);

            return customer;
        }
        //Check SDT và Email
        public async Task<bool> CheckSDT(string soDienThoai)
        {
            return await _context.khachHangs.AnyAsync(x => x.SoDienThoai == soDienThoai);
        }
        public async Task<bool> CheckMail(string mail)
        {
            return await _context.khachHangs.AnyAsync(x => x.Email == mail);
        }
        //Tìm Kiếm 
        public async Task<List<KhachHang>> SearchKhachHang(string? name)
        {
            var query = _context.khachHangs.AsQueryable();

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(nv => nv.HoTen.Contains(name));
            }
            return await query.ToListAsync();
        }
    }
}
