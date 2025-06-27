using AppData.Model;

namespace AppAPI.IService
{
    public interface ILichSuThanhToanService
    {
        Task<List<LichSuThanhToan>> GetAllAsync();
        Task<LichSuThanhToan> GetByIdAsync(Guid id);
        Task AddAsync(LichSuThanhToan lichSuThanhToan);
        Task UpdateAsync(LichSuThanhToan lichSuThanhToan);
        Task DeleteAsync(Guid id);
        Task<List<LichSuThanhToan>> GetByIdHoaDonAsync(Guid idHoaDon);
    }
}
