using AppData.Model;

namespace APPMVC.IService
{
    public interface IChatLieuService
    {
        Task<List<ChatLieu>> GetChatLieu(string? name);
        Task<ChatLieu> GetChatLieuById(Guid id);
        Task Create(ChatLieu chatLieu);
        Task Update(ChatLieu chatLieu);
        Task Delete(Guid id);
    }
}