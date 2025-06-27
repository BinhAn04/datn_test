using AppData.Model;

namespace APPMVC.IService
{
    public interface IKichCoService
    {
        Task<List<KichCo>> GetKichCo(string? name);
        Task<KichCo> GetKichCoById(Guid id);
        Task Create(KichCo kichCo);
        Task Update(KichCo kichCo);
        Task Delete(Guid id);
        Task<List<KichCo>> GetKichCoBySanPhamId(Guid sanPhamId);

        Task<List<KichCo>> GetKichCoByIdsAsync(List<Guid> kichCoIds);
    }
}
