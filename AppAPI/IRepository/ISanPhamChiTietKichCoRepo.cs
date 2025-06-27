using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.IRepository
{
    public interface ISanPhamChiTietKichCoRepo
    {
        // Lấy danh sách tất cả SanPhamChiTietKichCo
        Task<List<SanPhamChiTietKichCo>> GetSanPhamChiTietKichCoAsync();

        // Lấy Kich Co theo ID
        Task<SanPhamChiTietKichCo> GetKichCoByIdAsync(Guid id);

        // Tạo một Kich Co mới
        Task<bool> CreateAsync(SanPhamChiTietKichCo kichCo);

        // Cập nhật một Kich Co đã tồn tại
        Task<bool> UpdateAsync(SanPhamChiTietKichCo kichCo);

        // Xóa một Kich Co theo ID
        Task<bool> DeleteAsync(Guid id);

        Task<List<KichCo>> GetKichCoIdsBySanPhamChiTietId(Guid sanPhamChiTietId);

        Task<List<Guid>> GetSanPhamChiTietIdsByKichCoIdAsync(Guid kichCoId);
    }
}