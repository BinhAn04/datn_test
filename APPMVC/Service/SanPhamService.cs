using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class SanPhamService : ISanPhamService
    {
        HttpClient _httpClient;

        public SanPhamService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(SanPham sanPham)
        {
            await _httpClient.PostAsJsonAsync("api/SanPham/them", sanPham);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/SanPham/Xoa?id={id}");
        }

        public Task<List<SanPham>> GetSanPhams(string? name = null)
        {
            var repo = _httpClient.GetFromJsonAsync<List<SanPham>>($"api/SanPham/getall?name={name}");
            return repo;
        }

        public async Task<SanPham?> GetSanPhamById(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SanPham/getbyid?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SanPham>();
                }
                else
                {
                    Console.WriteLine($"Error retrieving product: {response.StatusCode}");
                    return null; // Trả về null nếu không tìm thấy
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }
        }

        public async Task Update(SanPham sanPham)
        {
            await _httpClient.PutAsJsonAsync("api/SanPham/Sua", sanPham);
        }

        public async Task<ThuongHieu?> GetThuongHieuBySanPhamIdAsync(Guid sanPhamId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SanPham/getthuonghieu?id={sanPhamId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ThuongHieu>();
                }
                else
                {
                    Console.WriteLine($"Error retrieving brand: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the brand: {ex.Message}");
                return null;
            }
        }
        public Task<List<SanPham>>GetSanPhamClient(string? name)
        {
            var response = _httpClient.GetFromJsonAsync<List<SanPham>>($"api/SanPham/getallspclient?name={name}");
            return response;
        }
        public async Task<List<SanPham>> GetSanPhamByCategory(Guid idDanhMuc)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/SanPham/getSanPhamDanhMuc?idDanhMuc={idDanhMuc}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<SanPham>>();
                }
                else
                {
                    Console.WriteLine($"Error retrieving product: {response.StatusCode}");
                    return null; // Trả về null nếu không tìm thấy
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while retrieving the product: {ex.Message}");
                return null; // Trả về null nếu có lỗi
            }
        }
    }
}
