using AppAPI.IRepository;
using AppAPI.IService;
using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Thêm namespace này để sử dụng Task

namespace AppAPI.Service
{
    public class SanPhamChiTietKichCoService : ISanPhamChiTietKichCoService
    {
        private readonly ISanPhamChiTietKichCoRepo _repository;

        public SanPhamChiTietKichCoService(ISanPhamChiTietKichCoRepo repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(SanPhamChiTietKichCo kichCo)
        {
            // Gọi phương thức của repository để thêm Kich Co mới
            return await _repository.CreateAsync(kichCo);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            // Gọi phương thức của repository để xóa Kich Co theo ID
            return await _repository.DeleteAsync(id);
        }

        public async Task<SanPhamChiTietKichCo> GetKichCoByIdAsync(Guid id)
        {
            // Gọi phương thức của repository để lấy Kich Co theo ID
            return await _repository.GetKichCoByIdAsync(id);
        }

        public async Task<List<KichCo>> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId)
        {
            return await _repository.GetKichCoIdsBySanPhamChiTietId(sanPhamChiTietId);
        }
        public async Task<List<Guid>> GetSanPhamChiTietIdsByKichCoIdAsync(Guid kichCoId)
        {
            return await _repository.GetSanPhamChiTietIdsByKichCoIdAsync(kichCoId);
        }


        public async Task<List<SanPhamChiTietKichCo>> GetSanPhamChiTietKichCoAsync()
        {
            // Gọi phương thức của repository để lấy danh sách Kich Co
            return await _repository.GetSanPhamChiTietKichCoAsync();
        }

        public async Task<bool> UpdateAsync(SanPhamChiTietKichCo kichCo)
        {
            // Gọi phương thức của repository để cập nhật Kich Co
            return await _repository.UpdateAsync(kichCo);
        }
    }
}