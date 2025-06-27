using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Service
{
	public class DiaChiService : IDiaChiService
	{
		private readonly IDiaChiRepo _repository;
		public DiaChiService(IDiaChiRepo repo)
		{
			_repository = repo;
		}
		public Task<List<DiaChi>> GetAll()
		{
			return _repository.GetAll();
		}

		public Task<List<DiaChi>> GetByIdKh(Guid idKhachHang)
		{
			return _repository.GetByIdKh(idKhachHang);
		}
		public Task<DiaChi> GetByIdAsync(Guid idDiaChi)
		{
			return _repository.GetByIdAsync(idDiaChi);
		}

		public Task<DiaChi> AddAsync(DiaChi diaChi)
		{
			return _repository.AddAsync(diaChi);
		}
		public Task<DiaChi> UpdateAsync(DiaChi diaChi)
		{
			return _repository.UpdateAsync(diaChi);
		}
		public Task<bool> DeleteAsync(Guid idDiaChi)
		{
			return _repository.DeleteAsync(idDiaChi);
		}
		public Task<List<Province>> GetProvincesAsync()
		{
			return _repository.GetProvincesAsync();
		}
		public Task<List<District>> GetDistrictsAsync(int provinceId)
		{
			return _repository.GetDistrictsAsync(provinceId);
		}
		public Task<List<Ward>> GetWardsAsync(int districtId)
		{
			return _repository.GetWardsAsync(districtId);
		}
		public Task<int> GetAddressCountByCustomerId(Guid customerId)
		{
			return _repository.GetAddressCountByCustomerId(customerId);
		}
		public Task<bool> HasDefaultAddressAsync(Guid customerId)
		{
			return _repository.HasDefaultAddressAsync(customerId);
		}

        public Task<DiaChi> GetDefaultAddressByCustomerIdAsync(Guid customerId)
        {
			return _repository.GetDefaultAddressByCustomerIdAsync(customerId);
        }
    }
}
