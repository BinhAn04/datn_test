using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;

namespace AppAPI.Service
{
    public class ChucVuService : IChucVuService
    {
        private readonly IChucVuRepo _repository;
        public ChucVuService(IChucVuRepo repository)
        {
            _repository = repository;  
        }

        public async Task<ChucVu> CreateCV(ChucVu cv)
        {
            return await _repository.CreateCV(cv);
            
        }

        public async Task DeleteCV(Guid id)
        {
            await _repository.DeleteCV(id);
        }

        public async Task<List<ChucVu>> GetAllChucVu()
        {
            return await _repository.GetAllChucVu();
        }

        public async Task<ChucVu> GetIdChucVu(Guid id)
        {
           return await _repository.GetIdChucVu(id);
        }

        public async Task<ChucVu> UpdateCV(ChucVu cv)
        {
            return await _repository.UpdateCV(cv);
        }
        public async Task<ChucVu> GetChucVuId(Guid? idChucVu)
        {
            return await _repository.GetChucVuId(idChucVu);
        }
    }
}
