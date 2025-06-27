using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class DanhMucService : IDanhMucService
    {
        private readonly IDanhMucRepo _repository;

        public DanhMucService(IDanhMucRepo repository)
        {
            _repository = repository;
        }

        public bool Create(DanhMuc danhMuc)
        {
            return _repository.Create(danhMuc);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public DanhMuc GetDanhMucById(Guid id)
        {
            return _repository.GetDanhMucById(id);
        }

        // Phương thức GetDanhMuc (Bất đồng bộ)
        public List<DanhMuc> GetDanhMuc(string? name)
        {
            return _repository.GetDanhMuc(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(DanhMuc danhMuc)
        {
            return _repository.Update(danhMuc);
        }
    }
}