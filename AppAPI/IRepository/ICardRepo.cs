using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ICardRepo
    {
        Task<bool> Create(GioHang gioHang);
        Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId);
    }
}
