using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class LichSuHoaDonService : ILichSuHoaDonService
    {
        private readonly HttpClient _httpClient;

        public LichSuHoaDonService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7198") // Địa chỉ API
            };
        }

        // Thêm lịch sử hóa đơn
        public async Task AddAsync(LichSuHoaDon lichSuHoaDon)
        {
            var response = await _httpClient.PostAsJsonAsync("api/LichSuHoaDon/them", lichSuHoaDon);
            response.EnsureSuccessStatusCode();
        }

        // Xóa lịch sử hóa đơn theo ID
        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/LichSuHoaDon/xoa?id={id}");
            response.EnsureSuccessStatusCode();
        }

        // Lấy tất cả lịch sử hóa đơn
        public async Task<List<LichSuHoaDon>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/LichSuHoaDon/getall");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LichSuHoaDon>>();
        }

        // Lấy lịch sử hóa đơn theo ID
        public async Task<LichSuHoaDon> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/LichSuHoaDon/getbyid?id={id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LichSuHoaDon>();
        }

        public async Task<List<LichSuHoaDon>> GetByIdHoaDonAsync(Guid idHoaDon)
        {
            var response = await _httpClient.GetAsync($"api/LichSuHoaDon/getbyidhoadon?idHoaDon={idHoaDon}");

            // Kiểm tra mã trạng thái
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Nếu không tìm thấy, trả về danh sách rỗng
                return new List<LichSuHoaDon>();
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LichSuHoaDon>>();
        }
        // Cập nhật lịch sử hóa đơn
        public async Task UpdateAsync(LichSuHoaDon lichSuHoaDon)
        {
            var response = await _httpClient.PutAsJsonAsync("api/LichSuHoaDon/sua", lichSuHoaDon);
            response.EnsureSuccessStatusCode();
        }
    }
}
