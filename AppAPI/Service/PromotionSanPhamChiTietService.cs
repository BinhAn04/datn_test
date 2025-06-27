using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.Service
{
    public class PromotionSanPhamChiTietService : IPromotionSanPhamChiTietService
    {
        private readonly IPromotionSanPhamChiTietRepo _repository;

        public PromotionSanPhamChiTietService(IPromotionSanPhamChiTietRepo repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(PromotionSanPhamChiTiet promotion)
        {
            return await _repository.CreateAsync(promotion);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<PromotionSanPhamChiTiet> GetPromotionByIdAsync(Guid id)
        {
            return await _repository.GetPromotionByIdAsync(id);
        }

        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietAsync()
        {
            return await _repository.GetPromotionSanPhamChiTietAsync();
        }

        public async Task<bool> UpdateAsync(PromotionSanPhamChiTiet promotion)
        {
            return await _repository.UpdateAsync(promotion);
        }

        public async Task<Guid?> GetPromotionsBySanPhamChiTietIdAsync(Guid sanPhamChiTietId)
        {
            return await _repository.GetPromotionsBySanPhamChiTietIdAsync(sanPhamChiTietId);
        }

        public async Task<List<PromotionSanPhamChiTiet>> GetPromotionSanPhamChiTietsByPromotionIdAsync(Guid promotionId)
        {
            return await _repository.GetPromotionSanPhamChiTietsByPromotionIdAsync(promotionId);
        }
    }
}