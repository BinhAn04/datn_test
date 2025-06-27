using AppData.Model;

namespace AppAPI.IService
{
    public interface IPromotionSanPhamChiTietService
    {
        Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietAsync();

        Task<PromotionSanPhamChiTiet> GetPromotionByIdAsync(Guid id);

        Task<bool> CreateAsync(PromotionSanPhamChiTiet promotion);

        Task<bool> UpdateAsync(PromotionSanPhamChiTiet promotion);
        Task<bool> DeleteAsync(Guid id);
        Task<Guid?> GetPromotionsBySanPhamChiTietIdAsync(Guid sanPhamChiTietId);
        Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietsByPromotionIdAsync(Guid promotionId);
    }
}
