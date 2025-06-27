using AppData.Model;

namespace AppAPI.IService
{
    public interface ILichSuHoaDonService
    {
        Task<List<LichSuHoaDon>> GetAllAsync();
        Task<LichSuHoaDon> GetByIdAsync(Guid id);
        Task AddAsync(LichSuHoaDon lichSuHoaDon);
        Task UpdateAsync(LichSuHoaDon lichSuHoaDon);
        Task DeleteAsync(Guid id);

        Task<List<LichSuHoaDon>> GetByIdHoaDonAsync(Guid idHoaDon);
    }
}
