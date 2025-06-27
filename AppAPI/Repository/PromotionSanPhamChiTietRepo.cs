using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAPI.IRepository;
using AppData;

namespace AppAPI.Repository
{
    public class PromotionSanPhamChiTietRepo : IPromotionSanPhamChiTietRepo
    {
        private readonly AppDbcontext _context; // Assuming AppDbContext is your EF Core DbContext

        public PromotionSanPhamChiTietRepo(AppDbcontext context)
        {
            _context = context;
        }

        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietAsync()
        {
            return await _context.promotionSanPhamChiTiets.ToListAsync();
        }

        public async Task<PromotionSanPhamChiTiet> GetPromotionByIdAsync(Guid id)
        {
            return await _context.promotionSanPhamChiTiets.FindAsync(id);
        }

        public async Task<bool> CreateAsync(PromotionSanPhamChiTiet promotion)
        {
            await _context.promotionSanPhamChiTiets.AddAsync(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(PromotionSanPhamChiTiet promotion)
        {
            _context.promotionSanPhamChiTiets.Update(promotion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var promotion = await _context.promotionSanPhamChiTiets.FindAsync(id);
            if (promotion != null)
            {
                _context.promotionSanPhamChiTiets.Remove(promotion);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<Guid?> GetPromotionsBySanPhamChiTietIdAsync(Guid sanPhamChiTietId)
        {
            return await _context.promotionSanPhamChiTiets
                .Where(p => p.IdSanPhamChiTiet == sanPhamChiTietId && p.Promotion.TrangThai == 1)
                .Select(p => p.Promotion.IdPromotion)
                .FirstOrDefaultAsync();
        }
        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietsByPromotionIdAsync(Guid promotionId)
        {
            // Kiểm tra xem ID khuyến mãi có hợp lệ không
            if (promotionId == Guid.Empty)
            {
                throw new ArgumentException("Promotion ID cannot be empty", nameof(promotionId));
            }

            // Truy vấn để lấy danh sách sản phẩm chi tiết cho khuyến mãi
            var promotionSanPhamChiTiets = await _context.promotionSanPhamChiTiets
                .Where(ps => ps.IdPromotion == promotionId)
                .ToListAsync();

            return promotionSanPhamChiTiets;
        }
    }
}