using AppData.Model;

namespace APPMVC.IService
{
    public interface IPromotionSanPhamChiTietService
    {
        Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietAsync(Guid idSanPhamChiTiet);

        Task<PromotionSanPhamChiTiet> GetPromotionByIdAsync(Guid id);

        Task<bool> CreateAsync(PromotionSanPhamChiTiet promotion);

        Task<bool> UpdateAsync(PromotionSanPhamChiTiet promotion);
        Task<bool> DeleteAsync(Guid id);
        Task<Guid?> GetPromotionsBySanPhamChiTietIdAsync(Guid sanPhamChiTietId);

        Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietsByPromotionIdAsync(Guid promotionId);
    }
}
