using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Repository
{
    public class PromotionRepo : IPromotionRepo
    {
        private readonly AppDbcontext _context;

        public PromotionRepo(AppDbcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            try
            {
                return await _context.promotions.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PromotionRepo.GetPromotionsAsync: {ex.Message}");
                return new List<Promotion>();
            }
        }

        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {

            return await _context.promotions.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Promotion promotion)
        {
            try
            {
                _context.promotions.Add(promotion);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PromotionRepo.CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Promotion promotion)
        {
            try
            {
                var promotionUpdate = await _context.promotions.FindAsync(promotion.IdPromotion);
                if (promotionUpdate != null)
                {
                    _context.Entry(promotionUpdate).CurrentValues.SetValues(promotion);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PromotionRepo.UpdateAsync: {ex.Message}");
                throw; // Re-throw the exception to be caught by the service
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var promotion = await _context.promotions.FindAsync(id);
                if (promotion != null)
                {
                    _context.promotions.Remove(promotion);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PromotionRepo.DeleteAsync: {ex.Message}");
                return false;
            }
        }
    }
}