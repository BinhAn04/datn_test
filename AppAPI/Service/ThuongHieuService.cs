using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class ThuongHieuService : IThuongHieuService
    {
        private readonly IThuongHieuRepo _repository;

        public ThuongHieuService(IThuongHieuRepo repository)
        {
            _repository = repository;
        }

        public bool Create(ThuongHieu thuongHieu)
        {
            return _repository.Create(thuongHieu);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public ThuongHieu GetThuongHieuById(Guid id)
        {
            return _repository.GetThuongHieuById(id);
        }

        // Phương thức GetThuongHieu (Bất đồng bộ)
        public List<ThuongHieu> GetThuongHieu(string? name)
        {
            return _repository.GetThuongHieu(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(ThuongHieu thuongHieu)
        {
            return _repository.Update(thuongHieu);
        }
    }
}