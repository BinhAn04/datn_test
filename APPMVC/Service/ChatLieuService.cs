using AppData.Model;
using APPMVC.IService;

namespace APPMVC.Service
{
    public class ChatLieuService : IChatLieuService
    {
        HttpClient _httpClient;

        public ChatLieuService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7198");
        }

        public async Task Create(ChatLieu chatLieu)
        {
            await _httpClient.PostAsJsonAsync("api/ChatLieu/them", chatLieu);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/ChatLieu/Xoa?id={id}");
        }

        public Task<List<ChatLieu>> GetChatLieu(string? name)
        {
            var repo = _httpClient.GetFromJsonAsync<List<ChatLieu>>($"api/ChatLieu/getall?name={name}");
            return repo;
        }

        public Task<ChatLieu> GetChatLieuById(Guid id)
        {
            var chatLieu = _httpClient.GetFromJsonAsync<ChatLieu>($"api/ChatLieu/getbyid?id={id}");
            return chatLieu;
        }

        public async Task Update(ChatLieu chatLieu)
        {
            await _httpClient.PutAsJsonAsync("api/ChatLieu/Sua", chatLieu);
        }
    }
}