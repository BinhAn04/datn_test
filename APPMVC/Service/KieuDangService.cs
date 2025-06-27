using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class KieuDangService : IKieuDangService
    {
        HttpClient _httpClient;

        public KieuDangService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(KieuDang kieuDang)
        {
            await _httpClient.PostAsJsonAsync("api/KieuDang/them", kieuDang);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/KieuDang/Xoa?id={id}");
        }

        public Task<List<KieuDang>> GetKieuDang(string? name)
        {
            var repo = _httpClient.GetFromJsonAsync<List<KieuDang>>($"api/KieuDang/getall?name={name}");
            return repo;
        }

        public Task<KieuDang> GetKieuDangById(Guid id)
        {
            var kieuDang = _httpClient.GetFromJsonAsync<KieuDang>($"api/KieuDang/getbyid?id={id}");
            return kieuDang;
        }

        public async Task Update(KieuDang kieuDang)
        {
            await _httpClient.PutAsJsonAsync("api/KieuDang/Sua", kieuDang);
        }
    }
}