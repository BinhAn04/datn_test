using AppData.Model;

namespace AppAPI.IService
{
    public interface IChatLieuService
    {
        List<ChatLieu> GetChatLieu(string? name);
        ChatLieu GetChatLieuById(Guid id);
        bool Create(ChatLieu chatLieu);
        bool Update(ChatLieu chatLieu);
        bool Delete(Guid id);
    }
}
