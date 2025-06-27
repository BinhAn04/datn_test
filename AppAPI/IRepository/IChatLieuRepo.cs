using AppData.Model;

namespace AppAPI.IRepository
{
    public interface IChatLieuRepo
    {
        List<ChatLieu> GetChatLieu(string? name);
        ChatLieu GetChatLieuById(Guid id);
        bool Create(ChatLieu chatLieu);
        bool Update(ChatLieu chatLieu);
        bool Delete(Guid id);

    }
}
