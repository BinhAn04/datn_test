using AppData.Model;

namespace APPMVC.IService
{
    public interface IKieuDangService
    {
        Task<List<KieuDang>> GetKieuDang(string? name);
        Task<KieuDang> GetKieuDangById(Guid id);
        Task Create(KieuDang kieuDang);
        Task Update(KieuDang kieuDang);
        Task Delete(Guid id);
    }
}