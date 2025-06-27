using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppAPI.Repository
{
    public class SanPhamChiTietMauSacRepo : ISanPhamChiTietMauSacRepo
    {
        private readonly AppDbcontext _context;

        public SanPhamChiTietMauSacRepo(AppDbcontext context)
        {
            _context = context;
        }

        public List<SanPhamChiTietMauSac> GetSanPhamChiTietMauSac()
        {
            return _context.sanPhamChiTietMausacs.ToList();
        }

        public List<Guid> GetSanPhamChiTietIdsByMauSacId(Guid mauSacId)
        {
            try
            {
                // Tìm tất cả các sản phẩm chi tiết có màu sắc tương ứng
                var mauSacList = _context.sanPhamChiTietMausacs
                    .Where(m => m.IdMauSac == mauSacId)
                    .ToList();

                if (mauSacList == null || mauSacList.Count == 0)
                {
                    // Thay vì ném ngoại lệ, có thể trả về danh sách rỗng
                    return new List<Guid>(); // Trả về danh sách rỗng nếu không tìm thấy
                }

                // Trả về danh sách IdSanPhamChiTiet
                return mauSacList.Select(m => m.IdSanPhamChiTiet).ToList();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"Error occurred while fetching SanPhamChiTiet IDs: {ex.Message}");
                return new List<Guid>(); // Trả về danh sách rỗng hoặc có thể ném lại ngoại lệ tùy thuộc vào cách xử lý của bạn
            }
        }


        public bool Create(SanPhamChiTietMauSac mauSac)
        {
            _context.sanPhamChiTietMausacs.Add(mauSac);
            return _context.SaveChanges() > 0; // Returns true if changes were saved
        }

        public bool Update(SanPhamChiTietMauSac mauSac)
        {
            _context.sanPhamChiTietMausacs.Update(mauSac);
            return _context.SaveChanges() > 0; // Returns true if changes were saved
        }

        public bool Delete(Guid id)
        {
            var mauSac = _context.sanPhamChiTietMausacs.Find(id);
            if (mauSac != null)
            {
                _context.sanPhamChiTietMausacs.Remove(mauSac);
                return _context.SaveChanges() > 0; // Returns true if changes were saved
            }
            return false; // Return false if the item was not found
        }

        public async Task<List<MauSac>> GetMauSacIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            return  await _context.sanPhamChiTietMausacs
             .Where(m => m.IdSanPhamChiTiet == sanPhamChiTietId)
             .Select(m => m.MauSac)
             .ToListAsync();          
        }
    }
}