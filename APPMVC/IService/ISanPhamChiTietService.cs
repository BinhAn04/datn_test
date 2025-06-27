using AppData.Model;

namespace APPMVC.IService
{
    public interface ISanPhamChiTietService
    {
        Task<List<SanPhamChiTiet>> GetSanPhamChiTiets();
        Task<SanPhamChiTiet> GetSanPhamChiTietById(Guid id);
        Task Create(SanPhamChiTiet sanPhamChiTiet);
        Task Update(SanPhamChiTiet sanPhamChiTiet);
        Task Delete(Guid id);

        Task<List<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId);
        Task<SanPhamChiTiet> GetIdSanPhamChiTietByFilter(Guid idSanPham, Guid idKichCo, Guid idMauSac);

        Task<SanPham> GetSanPhamByIdSanPhamChiTietAsync(Guid idSanPhamChiTiet);

        Task<SanPhamChiTiet> GetByIdHoaDonChiTietAsync(Guid id);
        Task<SanPhamChiTiet> GetByProductCodeAsync(string productCode);

    }

}


