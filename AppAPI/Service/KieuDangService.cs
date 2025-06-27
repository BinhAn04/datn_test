using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class KieuDangService : IKieuDangService
    {
        private readonly IKieuDangRepo _repository;

        public KieuDangService(IKieuDangRepo repository)
        {
            _repository = repository;
        }

        public bool Create(KieuDang kieuDang)
        {
            return _repository.Create(kieuDang);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public KieuDang GetKieuDangById(Guid id)
        {
            return _repository.GetKieuDangById(id);
        }

        // Phương thức GetKieuDang (Bất đồng bộ)
        public List<KieuDang> GetKieuDang(string? name)
        {
            return _repository.GetKieuDang(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(KieuDang kieuDang)
        {
            return _repository.Update(kieuDang);
        }
    }
}