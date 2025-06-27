using AppData.Model;

namespace APPMVC.IService
{
    public interface ICardService
    {
        Task Create(GioHang gioHang);
        Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId);
    }
}
