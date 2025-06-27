using AppData.Model;
using APPMVC.IService;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APPMVC.Service
{
	public class DiaChiService : IDiaChiService
	{
		HttpClient _httpClient;
		public DiaChiService()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7198");
		}

		public async Task<bool> AddAsync(DiaChi diaChi)
		{
			var response = await _httpClient.PostAsJsonAsync($"/api/DiaChi/them", diaChi);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteAsync(Guid idDiaChi)
		{
			var response = await _httpClient.DeleteAsync($"/api/DiaChi/Xoa?idDiaChi={idDiaChi}");
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAsync(Guid idDiaChi, DiaChi diaChi)
		{
			var response = await _httpClient.PutAsJsonAsync($"/api/DiaChi/Sua?id={idDiaChi}", diaChi);
			return response.IsSuccessStatusCode;
		}

		public async Task<List<DiaChi>> GetAll()
		{
			return await _httpClient.GetFromJsonAsync<List<DiaChi>>("/api/DiaChi/GetAll");
		}

		public async Task<List<DiaChi>> GetAllAsync(Guid? idKhachHang)
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/GetByIdKh?idKhacHang={idKhachHang}");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			return System.Text.Json.JsonSerializer.Deserialize<List<DiaChi>>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}

		public async Task<DiaChi> GetByIdAsync(Guid idDiaChi)
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/GetByIdAsync?id={idDiaChi}");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			return System.Text.Json.JsonSerializer.Deserialize<DiaChi>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}

		public async Task<List<District>> GetDistrictsAsync(int provinceId)
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/GetDistricts/{provinceId}");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<District>>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true // Đảm bảo không phân biệt chữ hoa thường
			});
		}

		public async Task<List<Province>> GetProvincesAsync()
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/GetProvinces");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Province>>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});
		}

		public async Task<List<Ward>> GetWardsAsync(int districtId)
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/GetWards/{districtId}");
			response.EnsureSuccessStatusCode();
			var responseBody = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<List<Ward>>(responseBody, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true // Đảm bảo không phân biệt chữ hoa thường
			});
		}
		public async Task<int> GetAddressCountByCustomerId(Guid customerId)
		{
			var response = await _httpClient.GetAsync($"api/DiaChi/GetAddressCountByCustomerId/{customerId}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return int.Parse(content);

		}
		public async Task<bool> HasDefaultAddressAsync(Guid customerId)
		{
			var response = await _httpClient.GetAsync($"/api/DiaChi/HasDefaultAddressAsync/{customerId}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return bool.Parse(content);
		}

        public async Task<List<DiaChi>> GetByIdKh(Guid idKhachHang)
        {
            var response = await _httpClient.GetAsync($"/api/DiaChi/GetByIdKh/{idKhachHang}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<DiaChi>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<DiaChi> GetDefaultAddressByCustomerIdAsync(Guid? customerId)
        {
            var response = await _httpClient.GetAsync($"/api/DiaChi/GetDefaultAddress/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<DiaChi>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return null; 
        }
    }
}
