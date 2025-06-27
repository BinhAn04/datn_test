using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IDeGiayRepo
    {
        List<DeGiay> GetDeGiay(string? name);
        DeGiay GetDeGiayById(Guid id);
        bool Create(DeGiay deGiay);
        bool Update(DeGiay deGiay);
        bool Delete(Guid id);
    }
}
