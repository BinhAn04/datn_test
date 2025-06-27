using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class KichCoService : IKichCoService
    {
        private readonly IKichCoRepo _repository;

        public KichCoService(IKichCoRepo repository)
        {
            _repository = repository;
        }

        public bool Create(KichCo kichCo)
        {
            return _repository.Create(kichCo);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public KichCo GetKichCoById(Guid id)
        {
            return _repository.GetKichCoById(id);
        }

        // Phương thức GetKichCo (Bất đồng bộ)
        public List<KichCo> GetKichCo(string? name)
        {
            return _repository.GetKichCo(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(KichCo kichCo)
        {
            return _repository.Update(kichCo);
        }
        public async Task<List<KichCo>> GetKichCoBySanPhamId(Guid sanPhamId)
        {
            return await _repository.GetKichCoBySanPhamId(sanPhamId);
        }

        public async Task<List<KichCo>> GetKichCoByIdsAsync(List<Guid> kichCoIds)
        {
            return await _repository.GetKichCoByIdsAsync(kichCoIds);
        }
    }
}