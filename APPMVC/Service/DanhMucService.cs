using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class DanhMucService : IDanhMucService
    {
        HttpClient _httpClient;

        public DanhMucService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(DanhMuc danhMuc)
        {
            await _httpClient.PostAsJsonAsync("api/DanhMuc/them", danhMuc);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/DanhMuc/Xoa?id={id}");
        }

        public Task<List<DanhMuc>> GetDanhMuc(string? name)
        {
            var repo = _httpClient.GetFromJsonAsync<List<DanhMuc>>($"api/DanhMuc/getall?name={name}");
            return repo;
        }

        public Task<DanhMuc> GetDanhMucById(Guid id)
        {
            var danhMuc = _httpClient.GetFromJsonAsync<DanhMuc>($"api/DanhMuc/getbyid?id={id}");
            return danhMuc;
        }

        public async Task Update(DanhMuc danhMuc)
        {
            await _httpClient.PutAsJsonAsync("api/DanhMuc/Sua", danhMuc);
        }
    }
}