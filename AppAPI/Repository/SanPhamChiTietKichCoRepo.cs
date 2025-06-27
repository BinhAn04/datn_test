using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore; // Đảm bảo bạn đã thêm namespace này
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Thêm namespace này để sử dụng Task

namespace AppAPI.Repository
{
    public class SanPhamChiTietKichCoRepo : ISanPhamChiTietKichCoRepo
    {
        private readonly AppDbcontext _context;

        public SanPhamChiTietKichCoRepo(AppDbcontext context)
        {
            _context = context;
        }

        // Sửa đổi phương thức này để sử dụng async/await
        public async Task<List<SanPhamChiTietKichCo>> GetSanPhamChiTietKichCoAsync()
        {
            return await _context.sanPhamChiTietKichCos
                .Include(s => s.SanPhamChiTiet)
                .Include(k => k.KichCo)
                .ToListAsync();
        }

        public async Task<SanPhamChiTietKichCo> GetKichCoByIdAsync(Guid id)
        {
            return await _context.sanPhamChiTietKichCos.FirstOrDefaultAsync(x => x.IdSanPhamChiTiet == id); 
        }

        public async Task<bool> CreateAsync(SanPhamChiTietKichCo kichCo)
        {
            await _context.sanPhamChiTietKichCos.AddAsync(kichCo); // Sử dụng AddAsync để thêm không đồng bộ
            return await _context.SaveChangesAsync() > 0; // Returns true if changes were saved
        }

        public async Task<bool> UpdateAsync(SanPhamChiTietKichCo kichCo)
        {
            _context.sanPhamChiTietKichCos.Update(kichCo);
            return await _context.SaveChangesAsync() > 0; // Returns true if changes were saved
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var kichCo = await _context.sanPhamChiTietKichCos.FindAsync(id); // Sử dụng FindAsync để tìm kiếm không đồng bộ
            if (kichCo != null)
            {
                _context.sanPhamChiTietKichCos.Remove(kichCo);
                return await _context.SaveChangesAsync() > 0; // Returns true if changes were saved
            }
            return false; // Return false if the item was not found
        }

        public async Task<List<KichCo>> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {

            return await _context.sanPhamChiTietKichCos
                .Where(x => x.IdSanPhamChiTiet == sanPhamChiTietId )
                .Select(x => x.KichCo)
                .ToListAsync();


        }

        public async Task<List<Guid>> GetSanPhamChiTietIdsByKichCoIdAsync(Guid kichCoId)
        {
            try
            {
                // Tìm tất cả các sản phẩm chi tiết có màu sắc tương ứng
                var kichCoList = _context.sanPhamChiTietKichCos
                    .Where(m => m.IdKichCo == kichCoId)
                    .ToList();

                if (kichCoList == null || kichCoList.Count == 0)
                {
                    // Thay vì ném ngoại lệ, có thể trả về danh sách rỗng
                    return  new List<Guid>(); // Trả về danh sách rỗng nếu không tìm thấy
                }

                // Trả về danh sách IdSanPhamChiTiet
                return kichCoList.Select(m => m.IdSanPhamChiTiet).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Error occurred while fetching SanPhamChiTiet IDs: {ex.Message}");
                return new List<Guid>(); // Trả về danh sách rỗng hoặc có thể ném lại ngoại lệ tùy thuộc vào cách xử lý của bạn
            }
        }
    }
}