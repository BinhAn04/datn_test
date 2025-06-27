using AppData.Model;

namespace APPMVC.IService
{
    public interface IDeGiayService
    {
        Task<List<DeGiay>> GetDeGiay(string? name);
        Task<DeGiay> GetDeGiayById(Guid id);
        Task Create(DeGiay deGiay);
        Task Update(DeGiay deGiay);
        Task Delete(Guid id);
    }
}
