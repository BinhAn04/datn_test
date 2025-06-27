using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class NhanVienRepo : INhanVienRepo
    {
        AppDbcontext _context;
        public NhanVienRepo()
        {
           
            _context = new AppDbcontext();
        }
        public async Task<NhanVien> CreateNV(NhanVien nv)
        {
            try
            {
                // Kiểm tra xem thuộc tính AnhNhanVien có giá trị không
                if (string.IsNullOrEmpty(nv.AnhNhanVien))
                {
                    nv.AnhNhanVien = "anh_a.jpg"; // Gán ảnh mặc định nếu không có ảnh
                }

                // Thêm nhân viên vào ngữ cảnh
                await _context.nhanViens.AddAsync(nv);
                await _context.SaveChangesAsync();
                return nv;
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý ngoại lệ theo cách bạn muốn
                throw new Exception("Có lỗi xảy ra khi thêm nhân viên: " + ex.Message);
            }
        }
        public async Task<NhanVien?> Login(string email, string password)
        {
            var kh = await _context.nhanViens.FirstOrDefaultAsync(kh => kh.Email == email);           
            if (kh == null)
            {
                throw new Exception("Email Không tồn tại");
            }
            //Kiểm tra mk
            if (kh.MatKhau != password)
            {
                throw new Exception("Mật Khẩu Không Chính Xác");
            }

            //Hợp lệ trả về kh
            return kh;
        }
        public async Task DeleteDM(Guid id)
        {
           var nhanVien = await _context.nhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.nhanViens.Remove(nhanVien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<NhanVien>> GetAllNhaVien()
        {
            return await _context.nhanViens.ToListAsync();
        }
        public async Task<NhanVien> GetByEmail(string email)
        {
            return await _context.nhanViens.FirstOrDefaultAsync(kh => kh.Email == email);
        }

        public async Task<NhanVien> GetBySDT(string sdT)
        {
            return await _context.nhanViens.FirstOrDefaultAsync(kh => kh.SoDienThoai == sdT);
        }
        public async Task<NhanVien> GetIdNhanVien(Guid id)
        {
            return await _context.nhanViens.FindAsync(id);
        }

        public async Task<NhanVien> UpdateNV(NhanVien nv)
        {
            _context.nhanViens.Attach(nv);
            _context.Entry(nv).Property(x => x.TenNhanVien).IsModified = true;
            _context.Entry(nv).Property(x => x.SoDienThoai).IsModified = true;
            _context.Entry(nv).Property(x => x.Email).IsModified = true;
            _context.Entry(nv).Property(x => x.AnhNhanVien).IsModified = true;
            _context.Entry(nv).Property(x => x.DiaChi).IsModified = true;
            _context.Entry(nv).Property(x => x.IdchucVu).IsModified = true;
            _context.Entry(nv).Property(x => x.TrangThai).IsModified = true;
            
            await _context.SaveChangesAsync();
            return nv;

        }
        public async Task<NhanVien> UpdateThongTin(NhanVien nv)
        {
            _context.nhanViens.Attach(nv);
            _context.Entry(nv).Property(x => x.TenNhanVien).IsModified = true;
            _context.Entry(nv).Property(x => x.SoDienThoai).IsModified = true;
            _context.Entry(nv).Property(x => x.Email).IsModified = true;
            _context.Entry(nv).Property(x => x.AnhNhanVien).IsModified = true;
            _context.Entry(nv).Property(x => x.DiaChi).IsModified = true;
            _context.Entry(nv).Property(x => x.TrangThai).IsModified = true;

            await _context.SaveChangesAsync();
            return nv;
        }
        public async Task<bool> DoiMK(Guid idNhanVien, string newPassWord)
        {
            var nhanVien = await _context.nhanViens.FindAsync(idNhanVien);
            if (nhanVien == null) return false;
            nhanVien.MatKhau = newPassWord;
            _context.nhanViens.Update(nhanVien);
            await _context.SaveChangesAsync();
            return true;     
        }
        public  async Task<bool> ResetPass(string email, string newPassword)
        {
            var nhanVien =  _context.nhanViens.FirstOrDefault(e => e.Email == email);
            if(nhanVien == null) return false ;
            nhanVien.MatKhau = newPassword;
            _context.nhanViens.Update(nhanVien) ;
            await _context.SaveChangesAsync();
            return true;
        }
        //Check SDT và Email
        public async Task<bool> CheckSDT(string soDienThoai)
        {
            return await _context.nhanViens.AnyAsync(x => x.SoDienThoai == soDienThoai);
        }
        public async Task<bool> CheckMail(string mail)
        {
            return await _context.nhanViens.AnyAsync(x => x.Email == mail);
        }
        //Tìm Kiếm 
        public async Task<List<NhanVien>> SearchNhanVien(string? name)
        {
            var query = _context.nhanViens.AsQueryable();

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(nv => nv.TenNhanVien.Contains(name));
            }
            return await query.ToListAsync();
        }
    }
}
