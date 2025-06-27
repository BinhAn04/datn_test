using AppData.Model;

namespace APPMVC.IService
{
    public interface IDiaChiHDService
    {
        Task<List<DiaChiHoaDon>> GetAll();
        Task<DiaChiHoaDon> GetByIdAsync(Guid idDiaChi);
        Task<bool> AddAsync(DiaChiHoaDon diaChi);
        Task<bool> UpdateAsync(Guid idDiaChi, DiaChiHoaDon diaChi);
        Task<bool> DeleteAsync(Guid idDiaChi);
        Task<List<Province>> GetProvincesAsync();
        Task<List<District>> GetDistrictsAsync(int provinceId);
        Task<List<Ward>> GetWardsAsync(int districtId);
    }
}
