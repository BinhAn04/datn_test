using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class DeGiayService : IDeGiayService
    {
        HttpClient _httpClient;

        public DeGiayService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(DeGiay deGiay)
        {
            await _httpClient.PostAsJsonAsync("api/DeGiay/them", deGiay);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/DeGiay/Xoa?id={id}");
        }

        public Task<List<DeGiay>> GetDeGiay(string? name)
        {
            var repo = _httpClient.GetFromJsonAsync<List<DeGiay>>($"api/DeGiay/getall?name={name}");
            return repo;
        }

        public Task<DeGiay> GetDeGiayById(Guid id)
        {
            var deGiay = _httpClient.GetFromJsonAsync<DeGiay>($"api/DeGiay/getbyid?id={id}");
            return deGiay;
        }

        public async Task Update(DeGiay deGiay)
        {
            await _httpClient.PutAsJsonAsync("api/DeGiay/Sua", deGiay);
        }
    }
}