using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class HoaDonRepo : IHoaDonRepo
    {
        private readonly AppDbcontext _context;

        public HoaDonRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<HoaDon>> GetAllAsync()  
        {
            return await _context.hoaDons.ToListAsync();
        }

        public async Task<HoaDon> GetByIdAsync(Guid id)
        {
            return await _context.hoaDons.FindAsync(id);
        }

        public async Task AddAsync(HoaDon hoaDon)
        {
            if (hoaDon == null)
            {
                throw new ArgumentNullException(nameof(hoaDon), "HoaDon cannot be null");
            }

            try
            {
                await _context.hoaDons.AddAsync(hoaDon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message ?? "No inner exception";
                throw new Exception($"An error occurred while adding the HoaDon: {innerException}", ex);
            }
        }

        public async Task<bool> UpdateAsync(HoaDon hoaDon)
        {
            if (hoaDon == null || hoaDon.IdHoaDon == Guid.Empty)
            {
                throw new ArgumentException("Invalid HoaDon data.");
            }

            try
            {
                var khachHangExists = await _context.khachHangs.AnyAsync(kh => kh.IdKhachHang == hoaDon.IdKhachHang);

                if (!khachHangExists)
                {
                    var newKhachHang = new KhachHang
                    {
                        IdKhachHang = hoaDon.IdKhachHang,
                    };
                    _context.khachHangs.Add(newKhachHang);
                    await _context.SaveChangesAsync();
                }

                // Tìm hóa đơn hiện tại
                var existingHoaDon = await _context.hoaDons.FindAsync(hoaDon.IdHoaDon);

                if (existingHoaDon != null)
                {
                    // Cập nhật thông tin hóa đơn
                    existingHoaDon.MaDon = hoaDon.MaDon;
                    existingHoaDon.NguoiNhan = hoaDon.NguoiNhan;
                    existingHoaDon.SoDienThoaiNguoiNhan = hoaDon.SoDienThoaiNguoiNhan;
                    existingHoaDon.DiaChiGiaoHang = hoaDon.DiaChiGiaoHang;
                    existingHoaDon.LoaiHoaDon = hoaDon.LoaiHoaDon;
                    existingHoaDon.GhiChu = hoaDon.GhiChu;
                    existingHoaDon.TienShip = hoaDon.TienShip;
                    existingHoaDon.TienGiam = hoaDon.TienGiam;
                    existingHoaDon.TongTienDonHang = hoaDon.TongTienDonHang;
                    existingHoaDon.TongTienHoaDon = hoaDon.TongTienHoaDon;
                    existingHoaDon.NgayTao = hoaDon.NgayTao;
                    existingHoaDon.NguoiTao = hoaDon.NguoiTao;
                    existingHoaDon.KichHoat = hoaDon.KichHoat;
                    existingHoaDon.TrangThai = hoaDon.TrangThai;
                    existingHoaDon.IdKhachHang = hoaDon.IdKhachHang; 
                    existingHoaDon.IdNhanVien = hoaDon.IdNhanVien;

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Concurrency error updating HoaDon: {ex.Message}");
                throw new Exception("Lỗi khi cập nhật hóa đơn: Dữ liệu đã thay đổi, hãy thử lại.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database update error: {ex.Message}");
                throw new Exception("Lỗi khi cập nhật hóa đơn: " + ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating HoaDon: {ex.Message}");
                throw new Exception("Đã xảy ra lỗi khi cập nhật hóa đơn.");
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var hoaDon = await GetByIdAsync(id);
            if (hoaDon != null)
            {
                _context.hoaDons.Remove(hoaDon);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<HoaDon> GetByOrderNumberAsync(string orderNumber)
        {
            return await _context.hoaDons
                .FirstOrDefaultAsync(hd => hd.MaDon == orderNumber);
        }

        public async Task<List<HoaDon>> GetHoaDonsByCustomerIdAsync(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("Invalid customer ID.", nameof(customerId));
            }

            return await _context.hoaDons
                .Where(hoaDon => hoaDon.IdKhachHang == customerId)
                .ToListAsync();
        }

        public async Task<HoaDon> GetByMaDonAsync(string maDon)
        {
            return await _context.hoaDons.FirstOrDefaultAsync(h => h.MaDon == maDon);
        }
    }
}
