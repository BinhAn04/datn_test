using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
	public class GioHangChiTietRepo : IGioHangChiTietRepo
	{
		private readonly AppDbcontext _context;

		public GioHangChiTietRepo(AppDbcontext context)
		{
			_context = context;
		}

		public async Task<List<GioHangChiTiet>> GetAllAsync()
		{
			return await _context.gioHangChiTiets.ToListAsync();
		}

		public async Task<GioHangChiTiet> GetByIdAsync(Guid id)
		{
			return await _context.gioHangChiTiets.FindAsync(id);
		}

		public async Task AddAsync(GioHangChiTiet gioHangChiTiet)
		{
			await _context.gioHangChiTiets.AddAsync(gioHangChiTiet);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(GioHangChiTiet gioHangChiTiet)
		{
			_context.gioHangChiTiets.Update(gioHangChiTiet);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var gioHangChiTiet = await GetByIdAsync(id);
			if (gioHangChiTiet != null)
			{
				_context.gioHangChiTiets.Remove(gioHangChiTiet);
				await _context.SaveChangesAsync();
			}
		}

        public async Task<List<GioHangChiTiet>> GetByGioHangIdAsync(Guid gioHangId)
        {
            return await _context.gioHangChiTiets
            .Where(x => x.IdGioHang == gioHangId) 
            .ToListAsync();
        }

        public async Task RemoveItemsFromCartAsync(Guid cartId, List<Guid> productDetailIds)
        {
            // Kiểm tra xem danh sách productDetailIds có hợp lệ không
            if (productDetailIds == null || !productDetailIds.Any())
            {
                // Trả về nếu không có ID nào
                return;
            }

            // Lấy danh sách các sản phẩm chi tiết cần xóa
            var itemsToRemove = await _context.gioHangChiTiets
                .Where(x => x.IdGioHang == cartId && productDetailIds.Contains(x.IdSanPhamChiTiet))
                .ToListAsync();

            // Chỉ xóa nếu có sản phẩm nào được tìm thấy
            if (itemsToRemove.Count > 0)
            {
                _context.gioHangChiTiets.RemoveRange(itemsToRemove);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid cartId)
        {
            var cartDetails = await _context.gioHangChiTiets
			   .Where(c => c.IdGioHang == cartId && c.IdSanPhamChiTiet == sanPhamChiTietId)
			   .ToListAsync();

            return cartDetails.Sum(c => c.SoLuong);
        }

        public async Task<GioHangChiTiet> GetByProductIdAndCartIdAsync(Guid sanPhamChiTietId, Guid cartId)
        {
            return await _context.gioHangChiTiets
                .FirstOrDefaultAsync(x => x.IdSanPhamChiTiet == sanPhamChiTietId && x.IdGioHang == cartId);
        }

        public async Task<List<GioHangChiTiet>> GetByIdsAsync(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<GioHangChiTiet>(); 
            }

            return await _context.gioHangChiTiets
                .Where(gioHangChiTiet => ids.Contains(gioHangChiTiet.IdGioHangChiTiet))
                .ToListAsync();
        }
    }
}
