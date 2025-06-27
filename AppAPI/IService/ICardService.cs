using AppData.Model;

namespace AppAPI.IService
{
    public interface ICardService
    {
        Task<bool> Create(GioHang gioHang);
        Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId);
    }
}
