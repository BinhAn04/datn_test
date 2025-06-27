using AppData.Model;

namespace AppAPI.IService
{
    public interface IDeGiayService
    {
        List<DeGiay> GetDeGiay(string? name);
        DeGiay GetDeGiayById(Guid id);
        bool Create(DeGiay deGiay);
        bool Update(DeGiay deGiay);
        bool Delete(Guid id);
    }
}