using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IKieuDangRepo
    {
        List<KieuDang> GetKieuDang(string? name);
        KieuDang GetKieuDangById(Guid id);
        bool Create(KieuDang kieuDang);
        bool Update(KieuDang kieuDang);
        bool Delete(Guid id);
    }
}
