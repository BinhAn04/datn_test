using AppData.Model;
using APPMVC.IService;
using System.Net.Http;
using System.Text.Json;

namespace APPMVC.Service
{
    public class DiaChiHdService : IDiaChiHDService
    {
        HttpClient _httpClient;
        public DiaChiHdService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }
        public async Task<bool> AddAsync(DiaChiHoaDon diaChi)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/DiaChiHd/them", diaChi);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid idDiaChi)
        {
            var response = await _httpClient.DeleteAsync($"/api/DiaChiHd/Xoa?idDiaChi={idDiaChi}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<DiaChiHoaDon>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<List<DiaChiHoaDon>>("/api/DiaChiHd/GetAll");
        }

        public async Task<DiaChiHoaDon> GetByIdAsync(Guid idDiaChi)
        {
            var response = await _httpClient.GetAsync($"/api/DiaChiHd/GetByIdAsync?id={idDiaChi}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<DiaChiHoaDon>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<District>> GetDistrictsAsync(int provinceId)
        {
            var response = await _httpClient.GetAsync($"/api/DiaChiHd/GetDistricts/{provinceId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<District>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Đảm bảo không phân biệt chữ hoa thường
            });
        }

        public async Task<List<Province>> GetProvincesAsync()
        {
            var response = await _httpClient.GetAsync($"/api/DiaChiHd/GetProvinces");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Province>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Ward>> GetWardsAsync(int districtId)
        {
            var response = await _httpClient.GetAsync($"/api/DiaChiHd/GetWards/{districtId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Ward>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Đảm bảo không phân biệt chữ hoa thường
            });
        }

        public async Task<bool> UpdateAsync(Guid idDiaChi, DiaChiHoaDon diaChi)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/DiaChiHd/Sua?id={idDiaChi}", diaChi);
            return response.IsSuccessStatusCode;
        }
    }
}
