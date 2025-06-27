using AppData.Model;

namespace AppAPI.IService
{
    public interface ISanPhamChiTietMauSacService
    {
        List<SanPhamChiTietMauSac> GetSanPhamChiTietMauSac();
        List<Guid> GetSanPhamChiTietIdsByMauSacId(Guid mauSacId);

        // Create a new Mau Sac
        bool Create(SanPhamChiTietMauSac mauSac);

        // Update an existing Mau Sac
        bool Update(SanPhamChiTietMauSac mauSac);

        // Delete a Mau Sac by ID
        bool Delete(Guid id);

        Task<List<MauSac>> GetMauSacIdsBySanPhamChiTietId(Guid sanPhamChiTietId);

    }
}
