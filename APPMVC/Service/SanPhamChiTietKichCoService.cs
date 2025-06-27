using AppData.Model;
using APPMVC.IService;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class SanPhamChiTietKichCoService : ISanPhamChiTietKichCoService
    {
        private readonly HttpClient _httpClient;

        public SanPhamChiTietKichCoService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198")
            };
        }

        public async Task<List<SanPhamChiTietKichCo>> GetAllKichCo()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<SanPhamChiTietKichCo>>("api/SanPhamChiTietKichCo/getall");
            }
            catch (HttpRequestException ex)
            {
                // Xử lý lỗi ở đây
                Console.WriteLine($"Error fetching KichCo: {ex.Message}");
                return new List<SanPhamChiTietKichCo>(); // Trả về danh sách rỗng hoặc xử lý theo cách khác
            }
        }
        public async Task Create(SanPhamChiTietKichCo sanPhamChiTietKichCo)
        {
            await _httpClient.PostAsJsonAsync("api/SanPhamChiTietKichCo/them", sanPhamChiTietKichCo);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/SanPhamChiTietKichCo/Xoa?id={id}");
        }

        public async Task<SanPhamChiTietKichCo> GetKichCoById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<SanPhamChiTietKichCo>($"api/SanPhamChiTietKichCo/getbyid?id={id}");
        }

        public async Task Update(SanPhamChiTietKichCo sanPhamChiTietKichCo)
        {
            await _httpClient.PutAsJsonAsync("api/SanPhamChiTietKichCo/Sua", sanPhamChiTietKichCo);
        }

        public async Task<List<KichCo>> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<KichCo>>($"api/SanPhamChiTietKichCo/kichCoIds?sanPhamChiTietId={sanPhamChiTietId}");
            }
            catch (HttpRequestException ex)
            {
                // Handle error here
                Console.WriteLine($"Error fetching Mau Sac IDs: {ex.Message}");
                return new List<KichCo>(); // Return an empty list or handle accordingly
            }
        }

        public async Task<List<Guid>> GetSanPhamChiTietIdsByKichCoId(Guid kichCoId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Guid>>($"api/SanPhamChiTietKichCo/sanphamchitietids?kichCoId={kichCoId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching SanPhamChiTiet IDs: {ex.Message}");
                return new List<Guid>(); // Return an empty list or handle accordingly
            }
        }
    }
}