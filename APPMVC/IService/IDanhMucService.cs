using AppData.Model;

namespace APPMVC.IService
{
    public interface IDanhMucService
    {
        Task<List<DanhMuc>> GetDanhMuc(string? name);
        Task<DanhMuc> GetDanhMucById(Guid id);
        Task Create(DanhMuc danhMuc);
        Task Update(DanhMuc danhMuc);
        Task Delete(Guid id);
    }
}