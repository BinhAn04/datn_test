using AppData.Model;
using AppData.ViewModel;

namespace APPMVC.ViewModel
{
    public class SanPhamChiTietClientViewModel
    {
        public Guid IdSanPham { get; set; }

        public List<SanPhamChiTietItemViewModel> SanPhamChiTietList { get; set; }
        public string? TenSanPham { get; set; }
        public double? DiscountedPrice { get; set; }
        public string? MoTa { get; set; }
        public double? Gia { get; set; }
        public double? SoLuong { get; set; }
        public List<HinhAnh>? HinhAnhs { get; set; }
        public List<string> MauSac { get; set; } = new List<string>();
        public List<string> KichCo { get; set; } = new List<string>();
        public List<MauSac> AvailableColors { get; set; } = new List<MauSac>(); // Danh sách màu sắc có sẵn
        public List<KichCo> AvailableSizes { get; set; } = new List<KichCo>(); // Danh sách kích cỡ có sẵn
        public Guid? SelectedColorId { get; set; }
        public Guid? SelectedSizeId { get; set; }
        public int KichHoat { get; set; }
    }
}