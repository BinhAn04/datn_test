using AppData.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppAPI.IRepository
{
    public interface IHinhAnhRepo
    {
        Task<List<HinhAnh>> GetHinhAnhsAsync();
        Task<HinhAnh> GetHinhAnhByIdAsync(Guid id);
        Task<bool> UploadAsync(HinhAnh hinhAnh);
        Task<bool> DeleteAsync(Guid id);

        Task<List<HinhAnh>> GetHinhAnhsBySanPhamChiTietId(Guid sanPhamChiTietId);
    }
}