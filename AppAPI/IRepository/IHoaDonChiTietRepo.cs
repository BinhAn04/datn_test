using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IHoaDonChiTietRepo
    {
        Task<List<HoaDonChiTiet>> GetAllAsync();
        Task<HoaDonChiTiet> GetByIdAsync(Guid id);
        Task AddAsync(List<HoaDonChiTiet> hoaDonChiTietList);
        Task UpdateAsync(List<HoaDonChiTiet> hoaDonChiTietList);
        Task DeleteAsync(Guid id);

        Task<List<HoaDonChiTiet>> GetByIdHoaDonAsync(Guid idHoaDon);

        Task<double> GetTotalQuantityBySanPhamChiTietIdAsync(Guid sanPhamChiTietId, Guid hDCTId);

        Task<HoaDonChiTiet> GetByIdAndProduct(Guid idHoaDon, Guid idSanPhamChiTiet);
    }
}
