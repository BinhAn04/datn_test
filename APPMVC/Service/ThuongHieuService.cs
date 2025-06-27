using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class ThuongHieuService : IThuongHieuService
    {
        HttpClient _httpClient;

        public ThuongHieuService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(ThuongHieu thuongHieu)
        {
            await _httpClient.PostAsJsonAsync("api/ThuongHieu/them", thuongHieu);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/ThuongHieu/Xoa?id={id}");
        }

        public Task<List<ThuongHieu>> GetThuongHieu(string? name)
        {
            var repo = _httpClient.GetFromJsonAsync<List<ThuongHieu>>($"api/ThuongHieu/getall?name={name}");
            return repo;
        }

        public Task<ThuongHieu> GetThuongHieuById(Guid id)
        {
            var thuongHieu = _httpClient.GetFromJsonAsync<ThuongHieu>($"api/ThuongHieu/getbyid?id={id}");
            return thuongHieu;
        }

        public async Task Update(ThuongHieu thuongHieu)
        {
            await _httpClient.PutAsJsonAsync("api/ThuongHieu/Sua", thuongHieu);
        }
    }
}