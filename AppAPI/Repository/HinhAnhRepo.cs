using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Repository
{
    public class HinhAnhRepo : IHinhAnhRepo
    {
        private readonly AppDbcontext _context;

        public HinhAnhRepo(AppDbcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> UploadAsync(HinhAnh hinhAnh)
        {
            if (hinhAnh == null)
                throw new ArgumentNullException(nameof(hinhAnh));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (hinhAnh.IdHinhAnh == Guid.Empty)
                {
                    hinhAnh.IdHinhAnh = Guid.NewGuid();
                }
                _context.hinhAnh.Add(hinhAnh);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Consider logging the exception here
                Console.WriteLine($"Error while saving the image: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var hinhAnh = await _context.hinhAnh.FindAsync(id);
                if (hinhAnh != null)
                {
                    _context.hinhAnh.Remove(hinhAnh);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                Console.WriteLine($"Error while deleting the image: {ex.Message}");
                return false;
            }
        }

        public async Task<HinhAnh> GetHinhAnhByIdAsync(Guid id)
        {
            return await _context.hinhAnh.FindAsync(id);
        }

        public async Task<List<HinhAnh>> GetHinhAnhsAsync()
        {
            return await _context.hinhAnh.ToListAsync();
        }

        public async Task<List<HinhAnh>> GetHinhAnhsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            if (sanPhamChiTietId == Guid.Empty)
            {
                Console.WriteLine("Invalid SanPhamChiTietId: Guid.Empty");
                return new List<HinhAnh>();
            }

            var hinhAnhs = await _context.hinhAnh
                .Where(h => h.IdSanPhamChiTiet == sanPhamChiTietId)
                .ToListAsync();

            return hinhAnhs;
        }
    }
}