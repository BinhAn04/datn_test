using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class HoaDonChiTietRepo : IHoaDonChiTietRepo
    {
        private readonly AppDbcontext _context;

        public HoaDonChiTietRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<HoaDonChiTiet>> GetAllAsync()
        {
            return await _context.hoaDonChiTiets.ToListAsync();
        }

        public async Task<HoaDonChiTiet> GetByIdAsync(Guid id)
        {
            return await _context.hoaDonChiTiets.FindAsync(id);
        }

        public async Task AddAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            if (hoaDonChiTietList == null || !hoaDonChiTietList.Any())
            {
                throw new ArgumentException("The list of HoaDonChiTiet cannot be null or empty.", nameof(hoaDonChiTietList));
            }

            try
            {
                // Add the list of HoaDonChiTiet to the context
                await _context.hoaDonChiTiets.AddRangeAsync(hoaDonChiTietList);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Database update error: {ex.Message}");
                // Optionally, throw the exception to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that may occur
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw; // Rethrow to maintain the stack trace
            }
        }

        public async Task UpdateAsync(List<HoaDonChiTiet> hoaDonChiTietList)
        {
            _context.hoaDonChiTiets.UpdateRange(hoaDonChiTietList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var hoaDonChiTiet = await GetByIdAsync(id);
            if (hoaDonChiTiet != null)
            {
                _context.hoaDonChiTiets.Remove(hoaDonChiTiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<HoaDonChiTiet>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            return await _context.hoaDonChiTiets
                .Where(h => h.IdHoaDon == idHoaDon) 
                .ToListAsync();
        }

        public async Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid hDCTId)
        {
            var cartDetails = await _context.hoaDonChiTiets
               .Where(c => c.IdHoaDonChiTiet == hDCTId && c.IdSanPhamChiTiet == sanPhamChiTietId)
               .ToListAsync();

            return cartDetails.Sum(c => c.SoLuong);
        }


        public async Task<HoaDonChiTiet> GetByIdAndProduct(Guid idHoaDon, Guid idSanPhamChiTiet)
        {
            return await _context.hoaDonChiTiets
                .FirstOrDefaultAsync(h => h.IdHoaDon == idHoaDon && h.IdSanPhamChiTiet == idSanPhamChiTiet);
        }
    }
}