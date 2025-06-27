using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IThuongHieuRepo
    {
        List<ThuongHieu> GetThuongHieu(string? name);
        ThuongHieu GetThuongHieuById(Guid id);
        bool Create(ThuongHieu thuongHieu);
        bool Update(ThuongHieu thuongHieu);
        bool Delete(Guid id);
    }
}