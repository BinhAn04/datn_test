using AppData.Model;
using APPMVC.IService;
using System.Net.Http;

namespace APPMVC.Service
{
    public class ChucVuService : IChucVuService
    {
        HttpClient httpClient;
        public ChucVuService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7198");
        }
        public Task CreateCV(ChucVu cv)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCV(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChucVu>> GetAllChucVu()
        {
            var repo = await httpClient.GetFromJsonAsync<List<ChucVu>>("/api/ChucVu/GetAllChucVu");
            return repo;
        }

        public async Task<ChucVu> GetIdChucVu(Guid id)
        {
            var response = await httpClient.GetAsync($"/api/ChucVu/GetIdChucVu?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ChucVu>();
            }
            return null; // Hoặc xử lý lỗi theo cách bạn muốn
        }

        public Task UpdateCV(ChucVu cv)
        {
            throw new NotImplementedException();
        }
        public async Task<ChucVu> GetChucVuId(Guid? idChucVu)
        {
            var response = await httpClient.GetAsync($"/api/ChucVu/GetChucVuId?idChucVu={idChucVu}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ChucVu>();
            }
            return null;
        }
    }
}
