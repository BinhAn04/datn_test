using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class DeGiayService : IDeGiayService
    {
        private readonly IDeGiayRepo _repository;

        public DeGiayService(IDeGiayRepo repository)
        {
            _repository = repository;
        }

        public bool Create(DeGiay deGiay)
        {
            return _repository.Create(deGiay);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public DeGiay GetDeGiayById(Guid id)
        {
            return _repository.GetDeGiayById(id);
        }

        // Phương thức GetDeGiay (Bất đồng bộ)
        public List<DeGiay> GetDeGiay(string? name)
        {
            return _repository.GetDeGiay(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(DeGiay deGiay)
        {
            return _repository.Update(deGiay);
        }
    }
}