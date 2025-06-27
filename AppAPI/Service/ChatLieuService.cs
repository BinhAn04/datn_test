using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class ChatLieuService : IChatLieuService
    {
        private readonly IChatLieuRepo _repository;

        public ChatLieuService(IChatLieuRepo repository)
        {
            _repository = repository;
        }

        public bool Create(ChatLieu chatLieu)
        {
            return _repository.Create(chatLieu);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public ChatLieu GetChatLieuById(Guid id)
        {
            return _repository.GetChatLieuById(id);
        }

        // Phương thức GetChatLieu (Bất đồng bộ)
        public List<ChatLieu> GetChatLieu(string? name)
        {
            return _repository.GetChatLieu(name);
        }

        // Phương thức Update (Bất đồng bộ)
        public bool Update(ChatLieu chatLieu)
        {
            return _repository.Update(chatLieu);
        }
    }
}