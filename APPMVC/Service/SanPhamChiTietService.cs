using AppData.Model;
using APPMVC.IService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace APPMVC.Service
{
    public class SanPhamChiTietService : ISanPhamChiTietService
    {
        HttpClient _httpClient;

        public SanPhamChiTietService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(SanPhamChiTiet sanPhamChiTiet)
        {
            await _httpClient.PostAsJsonAsync("api/SanPhamChiTiet/them", sanPhamChiTiet);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/SanPhamChiTiet/Xoa?id={id}");
        }

        public Task<List<SanPhamChiTiet>> GetSanPhamChiTiets()
        {
            var repo = _httpClient.GetFromJsonAsync<List<SanPhamChiTiet>>($"api/SanPhamChiTiet/getall");
            return repo;
        }

        public Task<SanPhamChiTiet> GetSanPhamChiTietById(Guid id)
        {
            var sanPhamChiTiet = _httpClient.GetFromJsonAsync<SanPhamChiTiet>($"api/SanPhamChiTiet/getbyid?id={id}");
            return sanPhamChiTiet;
        }

        public async Task Update(SanPhamChiTiet sanPhamChiTiet)
        {
            await _httpClient.PutAsJsonAsync("api/SanPhamChiTiet/Sua", sanPhamChiTiet);
        }

        public async Task<List<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId)
        {
            var response = await _httpClient.GetAsync($"api/SanPhamChiTiet/getbysanphamid?sanPhamId={sanPhamId}");

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a list of SanPhamChiTiet
                var sanPhamChiTietList = await response.Content.ReadFromJsonAsync<List<SanPhamChiTiet>>();
                return sanPhamChiTietList;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // No data found, return an empty list instead of null
                return new List<SanPhamChiTiet>();
            }
            else
            {
                // Handle other errors
                throw new HttpRequestException($"Error calling API: {response.StatusCode}");
            }
        }
        public async Task<SanPhamChiTiet> GetIdSanPhamChiTietByFilter(Guid idSanPham, Guid idKichCo, Guid idMauSac)
        {

            var response = await _httpClient.GetAsync($"api/SanPhamChiTiet/get-by-filter?idSanPham={idSanPham}&idKichCo={idKichCo}&idMauSac={idMauSac}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SanPhamChiTiet>();
            }
            else
            {
                return null;
            }
        }

        public async Task<SanPham> GetSanPhamByIdSanPhamChiTietAsync(Guid idSanPhamChiTiet)
        {
            var response = await _httpClient.GetAsync($"api/SanPhamChiTiet/getsanphambysanphamchitiet?idSanPhamChiTiet={idSanPhamChiTiet}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null; 
                }

                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error retrieving product details: {response.ReasonPhrase}. Content: {content}");
            }

            try
            {
                // Đọc nội dung JSON
                var jsonContent = await response.Content.ReadAsStringAsync();

                // Deserialize JSON thành đối tượng SanPham
                var sanPham = JsonConvert.DeserializeObject<SanPham>(jsonContent);
                return sanPham;
            }
            catch (Newtonsoft.Json.JsonException jsonEx) // Chỉ định rõ namespace
            {
                throw new Exception("Error parsing JSON response.", jsonEx);
            }
        }

        public async Task<SanPhamChiTiet> GetByIdHoaDonChiTietAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/SanPhamChiTiet/getbyhoadonchitiet?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SanPhamChiTiet>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null; // Return null if not found
            }
            else
            {
                // Handle other errors
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error calling API: {response.StatusCode}. Content: {content}");
            }
        }

        public async Task<SanPhamChiTiet> GetByProductCodeAsync(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                throw new ArgumentException("Product code cannot be null or empty.", nameof(productCode));
            }

            var response = await _httpClient.GetAsync($"api/SanPhamChiTiet/getbyproductcode?productCode={productCode}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<SanPhamChiTiet>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null; // Return null if not found
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error calling API: {response.StatusCode}. Content: {content}");
            }
        }
    }
}
// Lớp SanPham
public class SanPham1
{
    public Guid IdSanPham { get; set; }
    public string TenSanPham { get; set; }
    public List<SanPhamChiTiet> SanPhamChiTiets { get; set; }
}

// Lớp SanPhamChiTiet
public class SanPhamChiTiet1
{
    public Guid IdSanPhamChiTiet { get; set; }
    // Thêm các thuộc tính khác cho SanPhamChiTiet nếu cần
}