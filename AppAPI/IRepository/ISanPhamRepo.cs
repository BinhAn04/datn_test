using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ISanPhamRepo
    {
        Task<List<SanPham>> GetSanPhamAsync(string? name);
        Task<SanPham?> GetSanPhamByIdAsync(Guid id);
        Task<bool> CreateAsync(SanPham sanPham);
        Task<bool> UpdateAsync(SanPham sanPham);
        Task<bool> DeleteAsync(Guid id);
        Task<ThuongHieu?> GetThuongHieuBySanPhamIdAsync(Guid sanPhamId);
        Task<List<SanPham>> GetSanPhamClientAsync(string? name);
        Task<List<SanPham>> GetSanPhamByCategory(Guid idDanhMuc);
    }
}
