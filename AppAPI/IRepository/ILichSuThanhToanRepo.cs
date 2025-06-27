using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ILichSuThanhToanRepo
    {
        Task<List<LichSuThanhToan>> GetAllAsync();
        Task<LichSuThanhToan> GetByIdAsync(Guid id);
        Task AddAsync(LichSuThanhToan lichSuThanhToan);
        Task UpdateAsync(LichSuThanhToan lichSuThanhToan);
        Task DeleteAsync(Guid id);
        Task<List<LichSuThanhToan>> GetByIdHoaDonAsync(Guid idHoaDon);
    }
}
