using AppAPI.IRepository;
using AppData.Model;
using AppData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAPI.Repository
{
    public class SanPhamRepo : ISanPhamRepo
    {
        private readonly AppDbcontext _context;

        public SanPhamRepo(AppDbcontext context)
        {
            _context = context;
        }

        // Phương thức để tạo sản phẩm
        public async Task<bool> CreateAsync(SanPham sanPham)
        {
            try
            {
                // Kiểm tra và gán thực thể liên quan nếu chúng đã tồn tại trong cơ sở dữ liệu
                if (sanPham.ChatLieu != null)
                {
                    var existingChatLieu = await _context.chatLieus
                        .FirstOrDefaultAsync(cl => cl.IdChatLieu == sanPham.ChatLieu.IdChatLieu);
                    if (existingChatLieu != null)
                    {
                        sanPham.ChatLieu = existingChatLieu;
                    }
                }

                if (sanPham.DanhMuc != null)
                {
                    var existingDanhMuc = await _context.danhMuc
                        .FirstOrDefaultAsync(dm => dm.IdDanhMuc == sanPham.DanhMuc.IdDanhMuc);
                    if (existingDanhMuc != null)
                    {
                        sanPham.DanhMuc = existingDanhMuc;
                    }
                }

                if (sanPham.DeGiay != null)
                {
                    var existingDeGiay = await _context.deGiay
                        .FirstOrDefaultAsync(dg => dg.IdDeGiay == sanPham.DeGiay.IdDeGiay);
                    if (existingDeGiay != null)
                    {
                        sanPham.DeGiay = existingDeGiay;
                    }
                }

                if (sanPham.KieuDang != null)
                {
                    var existingKieuDang = await _context.kieuDangs
                        .FirstOrDefaultAsync(kd => kd.IdKieuDang == sanPham.KieuDang.IdKieuDang);
                    if (existingKieuDang != null)
                    {
                        sanPham.KieuDang = existingKieuDang;
                    }
                }

                if (sanPham.ThuongHieu != null)
                {
                    var existingThuongHieu = await _context.thuongHieus
                        .FirstOrDefaultAsync(th => th.IdThuongHieu == sanPham.ThuongHieu.IdThuongHieu);
                    if (existingThuongHieu != null)
                    {
                        sanPham.ThuongHieu = existingThuongHieu;
                    }
                }

                await _context.sanPhams.AddAsync(sanPham);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Lỗi cập nhật cơ sở dữ liệu: {dbEx.InnerException?.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var sanPham = await _context.sanPhams.FindAsync(id);
                if (sanPham != null)
                {
                    _context.sanPhams.Remove(sanPham);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error while deleting the product: " + ex.Message);
            }
        }

        public async Task<List<SanPham>> GetSanPhamAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.sanPhams.ToListAsync();
            }
            else
            {
                return await _context.sanPhams
                    .Where(m => m.TenSanPham.Contains(name))
                    .ToListAsync();
            }
        }

        public async Task<List<SanPham>> GetSanPhamClientAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.sanPhams
                    .Where(sp => sp.KichHoat == 1)
                    .ToListAsync();
            }
            else
            {
                return await _context.sanPhams
                    .Where(sp => sp.KichHoat == 1 && sp.TenSanPham.Contains(name))
                    .ToListAsync();
            }
        }

        public async Task<bool> UpdateAsync(SanPham sanPham)
        {
            var sanPhamUpdate = await _context.sanPhams.FindAsync(sanPham.IdSanPham);
            if (sanPhamUpdate != null)
            {
                sanPhamUpdate.TenSanPham = sanPham.TenSanPham;
                sanPhamUpdate.NgayCapNhat = sanPham.NgayCapNhat;
                sanPhamUpdate.NgayTao = sanPham.NgayTao;
                sanPhamUpdate.NguoiCapNhat = sanPham.NguoiCapNhat;
                sanPhamUpdate.NguoiTao = sanPham.NguoiTao;
                sanPhamUpdate.KichHoat = sanPham.KichHoat == 1 ? 1 : 0;
                sanPhamUpdate.MoTa = sanPham.MoTa;
                sanPhamUpdate.IdThuongHieu = sanPham.IdThuongHieu;
                sanPhamUpdate.IdChatLieu = sanPham.IdChatLieu;
                sanPhamUpdate.IdDanhMuc = sanPham.IdDanhMuc;
                sanPhamUpdate.IdDeGiay = sanPham.IdDeGiay;
                sanPhamUpdate.IdKieuDang = sanPham.IdKieuDang;

                _context.sanPhams.Update(sanPhamUpdate);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<SanPham?> GetSanPhamByIdAsync(Guid id)
        {
            try
            {
                // Kiểm tra ID có hợp lệ không
                if (id == Guid.Empty)
                {
                    throw new ArgumentException("ID cannot be empty.", nameof(id));
                }

                // Tìm sản phẩm theo ID
                var sanPham = await _context.sanPhams
                    //.Include(sp => sp.ThuongHieu)
                    //.Include(sp => sp.ChatLieu)
                    //.Include(sp => sp.DanhMuc)
                    //.Include(sp => sp.DeGiay)
                    //.Include(sp => sp.KieuDang)
                    .FirstOrDefaultAsync(s => s.IdSanPham == id);

                // Kiểm tra nếu không tìm thấy sản phẩm
                if (sanPham == null)
                {
                    Console.WriteLine($"SanPham with ID {id} not found.");
                    // Bạn có thể ném ra một ngoại lệ tùy chỉnh hoặc trả về một giá trị thông báo
                }

                return sanPham;
            }
            catch (Exception ex)
            {
                // Ghi lại thông tin lỗi
                Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                return null;
            }
        }
        public async Task<ThuongHieu?> GetThuongHieuBySanPhamIdAsync(Guid sanPhamId)
        {
            var sanPham = await _context.sanPhams
           .Include(sp => sp.ThuongHieu) // Bao gồm thông tin thương hiệu
           .FirstOrDefaultAsync(sp => sp.IdSanPham == sanPhamId);

            // Nếu sản phẩm không tồn tại, trả về null
            if (sanPham == null)
            {
                return null;
            }

            // Trả về thương hiệu của sản phẩm
            return sanPham.ThuongHieu;
        }
        public async Task<List<SanPham>> GetSanPhamByCategory(Guid idDanhMuc)
        {
            return await _context.sanPhams.Where(sp => sp.IdDanhMuc == idDanhMuc).ToListAsync();
        }
    }
}