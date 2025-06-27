using AppData.Model;
using AppData;
using AppAPI.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AppAPI.Repository
{
    public class SanPhamChiTietRepo : ISanPhamChiTietRepo
    {
        private readonly AppDbcontext _context;

        public SanPhamChiTietRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(SanPhamChiTiet sanPhamChitiet)
        {
            try
            {
                _context.sanPhamChiTiets.Add(sanPhamChitiet);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {


            var sanPhamChiTiet = await _context.sanPhamChiTiets.FindAsync(id);
            if (sanPhamChiTiet != null)
            {
                _context.sanPhamChiTiets.Remove(sanPhamChiTiet);
                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Không tìm thấy sản phẩm chi tiết
        }

        public SanPhamChiTiet GetSanPhamChitietById(Guid id)
        {
            var sanPhamChitiet = _context.sanPhamChiTiets.Find(id);
            return sanPhamChitiet;
        }

        public List<SanPhamChiTiet> GetSanPhamChiTiet()
        {
            var list = _context.sanPhamChiTiets.ToList();
            return list;
        }
        public async Task<bool> Update(SanPhamChiTiet sanPhamChiTiet)
        {
            // Tìm sản phẩm chi tiết trong cơ sở dữ liệu
            var sanPhamChitietUpdate = await _context.sanPhamChiTiets.FindAsync(sanPhamChiTiet.IdSanPhamChiTiet);

            if (sanPhamChitietUpdate != null)
            {
                // Cập nhật các thuộc tính của sản phẩm chi tiết
                sanPhamChitietUpdate.Gia = sanPhamChiTiet.Gia;
                sanPhamChitietUpdate.SoLuong = sanPhamChiTiet.SoLuong;
                sanPhamChitietUpdate.NgayCapNhat = sanPhamChiTiet.NgayCapNhat;
                sanPhamChitietUpdate.NgayTao = sanPhamChiTiet.NgayTao;
                sanPhamChitietUpdate.NguoiCapNhat = sanPhamChiTiet.NguoiCapNhat;
                sanPhamChitietUpdate.NguoiTao = sanPhamChiTiet.NguoiTao;
                sanPhamChitietUpdate.XuatXu = sanPhamChiTiet.XuatXu;
                sanPhamChitietUpdate.GiaGiam = sanPhamChiTiet.GiaGiam;
                sanPhamChitietUpdate.KichHoat = sanPhamChiTiet.KichHoat == 1 ? 1 : 0;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                return true; // Trả về true nếu cập nhật thành công
            }

            return false; // Trả về false nếu không tìm thấy sản phẩm chi tiết
        }

        public async Task<List<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            return await _context.sanPhamChiTiets
                 .Where(s => s.IdSanPham == sanPhamId)
                 .ToListAsync();
        }
        public async Task<SanPhamChiTiet> GetIdSanPhamChiTietByFilter(Guid idSanPham, Guid idKichCo, Guid idMauSac)
        {
            var query = await (from spct in _context.sanPhamChiTiets
                               join spctms in _context.sanPhamChiTietMausacs on spct.IdSanPhamChiTiet equals spctms.IdSanPhamChiTiet
                               join spctkc in _context.sanPhamChiTietKichCos on spct.IdSanPhamChiTiet equals spctkc.IdSanPhamChiTiet
                               where spct.IdSanPham == idSanPham
                                     && spctms.IdMauSac == idMauSac
                                     && spctkc.IdKichCo == idKichCo
                               select spct).FirstOrDefaultAsync();

            return query;
        }

        public async Task<SanPhamDto> GetSanPhamByIdSanPhamChiTietAsync(Guid idSanPhamChiTiet)
        {
            // Kiểm tra xem idSanPhamChiTiet có hợp lệ không
            if (idSanPhamChiTiet == Guid.Empty)
            {
                throw new ArgumentException("Invalid product detail ID.", nameof(idSanPhamChiTiet));
            }

            try
            {
                // Tìm sản phẩm chi tiết
                var sanPhamChiTiet = await _context.sanPhamChiTiets
                    .FirstOrDefaultAsync(spct => spct.IdSanPhamChiTiet == idSanPhamChiTiet);

                // Kiểm tra nếu sanPhamChiTiet không null
                if (sanPhamChiTiet == null)
                {
                    return null; // Không tìm thấy sản phẩm chi tiết
                }

                // Tìm sản phẩm tương ứng dựa trên IdSanPham
                var sanPham = await _context.sanPhams
                    .FirstOrDefaultAsync(sp => sp.IdSanPham == sanPhamChiTiet.IdSanPham);

                // Nếu sản phẩm không tìm thấy, trả về null
                if (sanPham == null)
                {
                    return null; // Không tìm thấy sản phẩm
                }

                // Chuyển đổi sản phẩm sang DTO
                var sanPhamDto = new SanPhamDto
                {
                    IdSanPham = sanPham.IdSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    KichHoat = sanPham.KichHoat
                    // Map các thuộc tính khác nếu cần
                };

                return sanPhamDto; // Trả về DTO sản phẩm
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("A database error occurred while retrieving the product.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving the product.", ex);
            }
        }

        public async Task<SanPhamChiTietDto> GetByIdHoaDonChiTietAsync(Guid id)
        {
            var hoaDonChiTiet = await _context.hoaDonChiTiets
                .FirstOrDefaultAsync(h => h.IdHoaDonChiTiet == id); 

            if (hoaDonChiTiet == null)
            {
                return null; 
            }

            var sanPhamChiTiet = await _context.sanPhamChiTiets
                .FirstOrDefaultAsync(spct => spct.IdSanPhamChiTiet == hoaDonChiTiet.IdSanPhamChiTiet);

            var sanPhamChiTietDto = new SanPhamChiTietDto
            {
                IdSanPhamChiTiet = sanPhamChiTiet.IdSanPhamChiTiet,
                Gia = sanPhamChiTiet.Gia,
                SoLuong = sanPhamChiTiet.SoLuong,
                CoHienThi = sanPhamChiTiet.CoHienThi,
                NgayCapNhat = sanPhamChiTiet.NgayCapNhat,
                GioiTinh = sanPhamChiTiet.GioiTinh,
                XuatXu = sanPhamChiTiet.XuatXu,
                NgayTao = sanPhamChiTiet.NgayTao,
                NguoiCapNhat = sanPhamChiTiet.NguoiCapNhat,
                NguoiTao = sanPhamChiTiet.NguoiTao,
                KichHoat = sanPhamChiTiet.KichHoat
            };

            return sanPhamChiTietDto;
        }

        public async Task<SanPhamChiTiet> GetByProductCodeAsync(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                throw new ArgumentException("Product code cannot be null or empty.", nameof(productCode));
            }

            return await _context.sanPhamChiTiets
                .FirstOrDefaultAsync(spct => spct.MaSp == productCode);
        }
    }
}
