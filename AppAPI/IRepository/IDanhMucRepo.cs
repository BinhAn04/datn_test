using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IDanhMucRepo
    {
        List<DanhMuc> GetDanhMuc(string? name);
        DanhMuc GetDanhMucById(Guid id);
        bool Create(DanhMuc danhMuc);
        bool Update(DanhMuc danhMuc);
        bool Delete(Guid id);
    }
}