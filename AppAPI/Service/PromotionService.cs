using AppAPI.IRepository;
using AppAPI.IService;
using AppAPI.Repository;
using AppData.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepo _repository;

        public PromotionService(IPromotionRepo repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Promotion>> GetPromotionsAsync()
        {
            try
            {
                return await _repository.GetPromotionsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServicePromotion.GetPromotionsAsync: {ex.Message}");
                return new List<Promotion>();
            }
        }

        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {
            try
            {
                return await _repository.GetPromotionByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServicePromotion.GetPromotionByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateAsync(Promotion promotion)
        {
            try
            {
                return await _repository.CreateAsync(promotion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServicePromotion.CreateAsync: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Promotion promotion)
        {
            try
            {
                return await _repository.UpdateAsync(promotion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServicePromotion.UpdateAsync: {ex.Message}");
                return false;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ServicePromotion.DeleteAsync: {ex.Message}");
                return false;
            }
        }
    }
}