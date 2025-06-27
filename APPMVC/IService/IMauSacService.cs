using AppData.Model;

namespace APPMVC.IService
{
    public interface IMauSacService
    {
        Task<List<MauSac>> GetMauSac(string? name);
        Task<MauSac> GetMauSacById(Guid id);
        Task Create(MauSac mauSac);
        Task Update(MauSac mauSac);
        Task Delete(Guid id);
        Task<List<MauSac>> GetMauSacBySanPhamId(Guid sanPhamId);
        Task<List<MauSac>> GetMauSacByIdsAsync(List<Guid> mauSacIds);
    }
}