using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class ChuyenKhoanViewModel
    {
        public Guid IdHoaDon { get; set; }
        public string TenNguoiNhan { get; set; }
        public double TongTienHang { get; set; }
    }
    public class ThanhToanCKViewModel
    {
        public List<AppData.ViewModel.HoaDonChiTietViewModel.SanPhamChiTietViewModel> SanPhamChiTiets { get; set; }
        public Guid IdHoaDon { get; set; }
        public string MaDon { get; set; }

        public Guid IdKhachHang { get; set; }
    }
}
