using AppData.Model;

namespace AppAPI.IService
{
    public interface IThuongHieuService
    {
        List<ThuongHieu> GetThuongHieu(string? name);
        ThuongHieu GetThuongHieuById(Guid id);
        bool Create(ThuongHieu thuongHieu);
        bool Update(ThuongHieu thuongHieu);
        bool Delete(Guid id);
    }
}
