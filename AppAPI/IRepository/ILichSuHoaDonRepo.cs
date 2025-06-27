using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ILichSuHoaDonRepo
    {
        Task<List<LichSuHoaDon>> GetAllAsync();
        Task<LichSuHoaDon> GetByIdAsync(Guid id);
        Task AddAsync(LichSuHoaDon lichSuHoaDon);
        Task UpdateAsync(LichSuHoaDon lichSuHoaDon);
        Task DeleteAsync(Guid id);
        Task<List<LichSuHoaDon>> GetByIdHoaDonAsync(Guid idHoaDon);
    }
}
