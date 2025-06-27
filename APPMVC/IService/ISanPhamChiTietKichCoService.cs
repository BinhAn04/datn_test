using AppData.Model;

namespace APPMVC.IService
{
    public interface ISanPhamChiTietKichCoService
    {
        Task<List<SanPhamChiTietKichCo>> GetAllKichCo();
        Task<SanPhamChiTietKichCo> GetKichCoById(Guid id);

        // Create a new KichCo
        Task Create(SanPhamChiTietKichCo sanPhamChiTietKichCo);

        // Update an existing KichCo
        Task Update(SanPhamChiTietKichCo sanPhamChiTietKichCo);

        // Delete a KichCo by ID
        Task Delete(Guid id);

        Task<List<KichCo>> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId);

        Task<List<Guid>> GetSanPhamChiTietIdsByKichCoId(Guid kichCoId);
    }
}
