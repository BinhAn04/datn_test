using AppData.Model;

namespace APPMVC.IService
{
    public interface ISanPhamChiTietMauSacService
    {
        Task<List<SanPhamChiTietMauSac>> GetAllMauSac();

        Task<List<Guid>> GetSanPhamChiTietIdsByMauSacId(Guid mauSacId);

        // Create a new Mau Sac
        Task Create(SanPhamChiTietMauSac sanPhamChiTietMauSac);

        // Update an existing Mau Sac
        Task Update(SanPhamChiTietMauSac sanPhamChiTietMauSac);

        // Delete a Mau Sac by ID
        Task Delete(Guid id);

        Task<List<MauSac>> GetMauSacIdsBySanPhamChiTietId(Guid sanPhamChiTietId);
    }
}
