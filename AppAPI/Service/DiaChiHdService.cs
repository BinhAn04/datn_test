using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;

namespace AppAPI.Service
{
    public class DiaChiHdService : IDiaChiHdService
    {
        private readonly IDiaChiHdRepo _repository;
        public DiaChiHdService(IDiaChiHdRepo repo)
        {
            _repository = repo;
        }
        public Task<DiaChiHoaDon> AddAsync(DiaChiHoaDon diaChi)
        {
            return _repository.AddAsync(diaChi);
        }

        public Task<bool> DeleteAsync(Guid idDiaChi)
        {
            return _repository.DeleteAsync(idDiaChi);
        }

        public Task<List<DiaChiHoaDon>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<DiaChiHoaDon> GetByIdAsync(Guid idDiaChi)
        {
            return _repository.GetByIdAsync(idDiaChi);
        }

        public Task<List<District>> GetDistrictsAsync(int provinceId)
        {
            return _repository.GetDistrictsAsync(provinceId);
        }

        public Task<List<Province>> GetProvincesAsync()
        {
            return _repository.GetProvincesAsync();
        }

        public Task<List<Ward>> GetWardsAsync(int districtId)
        {
            return _repository.GetWardsAsync(districtId);
        }

        public Task<DiaChiHoaDon> UpdateAsync(DiaChiHoaDon diaChi)
        {
            return _repository.UpdateAsync(diaChi);
        }
    }
}
