using AppData.Model;

namespace AppAPI.IService
{
    public interface IDanhMucService
    {
        List<DanhMuc> GetDanhMuc(string? name);
        DanhMuc GetDanhMucById(Guid id);
        bool Create(DanhMuc danhMuc);
        bool Update(DanhMuc danhMuc);
        bool Delete(Guid id);
    }
}
