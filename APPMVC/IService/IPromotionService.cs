using AppData.Model;

namespace APPMVC.IService
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetPromotionsAsync();
        Task<Promotion> GetPromotionByIdAsync(Guid id);
        Task<bool> CreateAsync(Promotion promotion);
        Task<bool> UpdateAsync(Promotion promotion);
        Task<bool> DeleteAsync(Guid id);
    }
}
