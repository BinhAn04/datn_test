using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class MauSacService : IMauSacService
    {
        private readonly IMauSacRepo _repository;

        public MauSacService(IMauSacRepo repository)
        {
            _repository = repository;
        }

        public bool Create(MauSac mauSac)
        {
            return _repository.Create(mauSac);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public MauSac GetMauSacById(Guid id)
        {
            return _repository.GetMauSacById(id);
        }

        // Phương thức GetMauSac (Bất đồng bộ)
        public List<MauSac> GetMauSac(string? name)
        {
            return _repository.GetMauSac(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(MauSac mauSac)
        {
            return _repository.Update(mauSac);
        }
        public async Task<List<MauSac>> GetMauSacBySanPhamId(Guid sanPhamId)
        {
            return await _repository.GetMauSacBySanPhamId(sanPhamId);
        }

        public async Task<List<MauSac>> GetMauSacByIdsAsync(List<Guid> mauSacIds)
        {
            return await _repository.GetMauSacByIdsAsync(mauSacIds);
        }
    }
}