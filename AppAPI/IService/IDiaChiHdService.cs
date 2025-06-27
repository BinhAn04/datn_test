using AppData.Model;

namespace AppAPI.IService
{
    public interface IDiaChiHdService
    {
        Task<List<DiaChiHoaDon>> GetAll();
        Task<DiaChiHoaDon> GetByIdAsync(Guid idDiaChi);
        Task<DiaChiHoaDon> AddAsync(DiaChiHoaDon diaChi);
        Task<DiaChiHoaDon> UpdateAsync(DiaChiHoaDon diaChi);
        Task<bool> DeleteAsync(Guid idDiaChi);
        Task<List<Province>> GetProvincesAsync();
        Task<List<District>> GetDistrictsAsync(int provinceId);
        Task<List<Ward>> GetWardsAsync(int districtId);
    }
}
