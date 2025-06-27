using AppAPI.IRepository;
using AppAPI.IService;
using AppAPI.Repository;
using AppData.Model;

namespace AppAPI.Service
{
    public class CardService : ICardService
    {
        private readonly ICardRepo _repository;

        public CardService(ICardRepo repository)
        {
            _repository = repository;
        }

        public async Task<bool> Create(GioHang gioHang)
        {
            return await _repository.Create(gioHang);
        }

        public async Task<Guid> GetCartIdByCustomerIdAsync(Guid customerId)
        {
            return await _repository.GetCartIdByCustomerIdAsync(customerId);
        }
    }
}
