using AppData.Model;

namespace APPMVC.IService
{
    public interface IThuongHieuService
    {
        Task<List<ThuongHieu>> GetThuongHieu(string? name);
        Task<ThuongHieu> GetThuongHieuById(Guid id);
        Task Create(ThuongHieu thuongHieu);
        Task Update(ThuongHieu thuongHieu);
        Task Delete(Guid id);
    }
}