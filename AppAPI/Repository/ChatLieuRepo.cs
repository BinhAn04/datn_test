using AppAPI.IRepository;
using AppData;
using AppData.Model;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Repository
{
    public class ChatLieuRepo : IChatLieuRepo
    {
        private readonly AppDbcontext _context;

        public ChatLieuRepo(AppDbcontext context)
        {
            _context = context;
        }

        public bool Create(ChatLieu chatLieu)
        {
            try
            {
                _context.chatLieus.Add(chatLieu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var chatLieu = _context.chatLieus.Find(id);
                if (chatLieu != null)
                {
                    _context.chatLieus.Remove(chatLieu);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ChatLieu GetChatLieuById(Guid id)
        {
            var chatLieu = _context.chatLieus.Find(id);
            return chatLieu;
        }

        public List<ChatLieu> GetChatLieu(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.chatLieus.ToList();
            }
            else
            {
                return _context.chatLieus
                    .Where(c => c.TenChatLieu.Contains(name)).ToList();
            }
        }

        public bool Update(ChatLieu chatLieu)
        {
            var chatLieuUpdate = _context.chatLieus.Find(chatLieu.IdChatLieu);
            if (chatLieuUpdate != null)
            {
                chatLieuUpdate.TenChatLieu = chatLieu.TenChatLieu;
                chatLieuUpdate.NgayCapNhat = chatLieu.NgayCapNhat;
                chatLieuUpdate.NgayTao = chatLieu.NgayTao;
                chatLieuUpdate.NguoiCapNhat = chatLieu.NguoiCapNhat;
                chatLieuUpdate.NguoiTao = chatLieu.NguoiTao;
                chatLieuUpdate.KichHoat = chatLieu.KichHoat == 1 ? 1 : 0;
                _context.chatLieus.Update(chatLieuUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}