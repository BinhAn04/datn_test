using AppData.Model;

namespace APPMVC.IService
{
	public interface IDiaChiService
	{
        Task<List<DiaChi>> GetByIdKh(Guid idKhachHang);
        Task<List<DiaChi>> GetAll();
		Task<List<DiaChi>> GetAllAsync(Guid? idKhachHang);
		Task<DiaChi> GetByIdAsync(Guid idDiaChi);
		Task<bool> AddAsync(DiaChi diaChi);
		Task<bool> UpdateAsync(Guid idDiaChi, DiaChi diaChi);
		Task<bool> DeleteAsync(Guid idDiaChi);

		Task<List<Province>> GetProvincesAsync();
		Task<List<District>> GetDistrictsAsync(int provinceId);
		Task<List<Ward>> GetWardsAsync(int districtId);
		//Check số lượng thêm cho khách hàng và địa chỉ mặc định
		Task<int> GetAddressCountByCustomerId(Guid customerId);
		Task<bool> HasDefaultAddressAsync(Guid customerId);

        Task<DiaChi> GetDefaultAddressByCustomerIdAsync(Guid? customerId);
    }
}
