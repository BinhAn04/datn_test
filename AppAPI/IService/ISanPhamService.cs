using AppData.Model;

namespace AppAPI.IService
{
    public interface ISanPhamService
    {
        Task<IEnumerable<SanPham>> GetSanPhamAsync(string? name); // Changed List<SanPham> to IEnumerable<SanPham>
        Task<SanPham?> GetSanPhamByIdAsync(Guid id);
        Task<bool> CreateAsync(SanPham sanPham);
        Task<bool> UpdateAsync(SanPham sanPham);
        Task<bool> DeleteAsync(Guid id);

        Task<ThuongHieu?> GetThuongHieuBySanPhamIdAsync(Guid sanPhamId);

        Task<IEnumerable<SanPham>> GetSanPhamClientAsync(string? name);
        Task<List<SanPham>> GetSanPhamByCategory(Guid idDanhMuc);
    }
}
