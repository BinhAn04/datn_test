using AppData.Model;

namespace AppAPI.IService
{
	public interface IDiaChiService
	{
		Task<List<DiaChi>> GetByIdKh(Guid idKhachHang);
		Task<List<DiaChi>> GetAll();
		Task<DiaChi> GetByIdAsync(Guid idDiaChi);
		Task<DiaChi> AddAsync(DiaChi diaChi);
		Task<DiaChi> UpdateAsync(DiaChi diaChi);
		Task<bool> DeleteAsync(Guid idDiaChi);
		Task<List<Province>> GetProvincesAsync();
		Task<List<District>> GetDistrictsAsync(int provinceId);
		Task<List<Ward>> GetWardsAsync(int districtId);
		//Check số lượng thêm cho khách hàng và địa chỉ mặc định
		Task<int> GetAddressCountByCustomerId(Guid customerId);
		Task<bool> HasDefaultAddressAsync(Guid customerId);
		Task<DiaChi> GetDefaultAddressByCustomerIdAsync(Guid customerId);


    }
}
