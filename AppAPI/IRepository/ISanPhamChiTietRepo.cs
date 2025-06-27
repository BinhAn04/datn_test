using AppData.Model;

namespace AppAPI.IRepository
{
    public interface ISanPhamChiTietRepo
    {
        List<SanPhamChiTiet> GetSanPhamChiTiet();
        SanPhamChiTiet GetSanPhamChitietById(Guid id);
        bool Create(SanPhamChiTiet sanPhamChitiet);
        Task<bool> Update(SanPhamChiTiet sanPhamChiTiet);
        Task<bool> Delete(Guid id);

        Task<List<SanPhamChiTiet>> GetSanPhamChiTietBySanPhamId(Guid sanPhamId);

        Task<SanPhamChiTiet> GetIdSanPhamChiTietByFilter(Guid idSanPham, Guid idKichCo, Guid idMauSac);

        Task<SanPhamDto> GetSanPhamByIdSanPhamChiTietAsync(Guid idSanPhamChiTiet);
        Task<SanPhamChiTietDto> GetByIdHoaDonChiTietAsync(Guid id);

        Task<SanPhamChiTiet> GetByProductCodeAsync(string productCode);
    }
}
public class SanPhamDto
{
    public Guid IdSanPham { get; set; }
    public string TenSanPham { get; set; }

    public int KichHoat { get; set; }
    // Các thuộc tính cần thiết khác
}

public class SanPhamChiTietDto
{
    public Guid IdSanPhamChiTiet { get; set; }

    public double Gia { get; set; }

    public double SoLuong { get; set; }

    public string? CoHienThi { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public string? GioiTinh { get; set; }

    public string? XuatXu { get; set; }

    public DateTime NgayTao { get; set; }

    public string? NguoiCapNhat { get; set; }

    public string? NguoiTao { get; set; }

    public int KichHoat { get; set; }
}