using AppData.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class HoaDonChiTietViewModel
    {
        public Guid IdHoaDonChiTiet { get; set; }
        public double DonGia { get; set; }
        public double SoLuong { get; set; }
        public double? TienKhachDua { get; set; }
        public double? GiamGia { get; set; } 

        public double? PhiVanChuyen { get; set; }
        public double TongTien { get; set; }

        public double KichHoat { get; set; }

        public Guid IdHoaDon { get; set; }
        public List<SanPhamChiTietViewModel> SanPhamChiTiets { get; set; } = new List<SanPhamChiTietViewModel>();
        public HoaDonViewModel? HoaDon { get; set; }
        public List<LichSuThanhToanViewModel> LichSuThanhToans { get; set; } = new List<LichSuThanhToanViewModel>();
        public List<LichSuHoaDonViewModel> LichSuHoaDons { get; set; } = new List<LichSuHoaDonViewModel>();

        public string? NguoiNhan { get; set; }

        [RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
        public string? SoDienThoai { get; set; }

        public string? ProvinceName { get; set; }
        public int? ProvinceId { get; set; }

        public string? DistrictName { get; set; }

        public int? DistrictId { get; set; }
        public string? WardName { get; set; }

        public string? WardId { get; set; }
        public string? MoTa { get; set; }

        public class LichSuHoaDonViewModel
        {
            public Guid IdLichSuHoaDon { get; set; }
            public string ThaoTac { get; set; }

            public DateTime NgayTao { get; set; }

            public string NguoiThaoTac { get; set; }
            public string TrangThai { get; set; }

            public string? GhiChu { get; set; }

            public Guid IdHoaDon { get; set; }
        }
        public class SanPhamChiTietViewModel
        {
            public Guid IdSanPhamChiTiet { get; set; }
            public string? MaSanPham { get; set; }
            public string? ProductName { get; set; }
            public double Quantity { get; set; }

            public double SoLuong { get; set; }
            public double Price { get; set; }
            public List<HinhAnh>? HinhAnhs { get; set; }
            public List<string> MauSac { get; set; } = new List<string>();
            public List<string> KichCo { get; set; } = new List<string>();
            public double? GiaDaGiam { get; set; }
            public double? PhanTramGiam { get; set; }
            public Guid IdHoaDonChiTiet { get; set; }

            public double OriginalPrice { get; set; }
            public double DiscountAmount { get; set; } 

            public int KichHoat { get; set; }

            public int HoatKich { get; set; }
        }
        public class HoaDonViewModel
        {
            public Guid IdHoaDon { get; set; }
            public string MaDon { get; set; }
            public string KhachHang { get; set; }
            public string TrangThai { get; set; }
            public string SoDienThoaiNguoiNhan { get; set; }
            public string LoaiHoaDon { get; set; }
            public string DiaChiGiaoHang { get; set; }

        }

        public class LichSuThanhToanViewModel
        {
            public Guid IdLichSuThanhToan { get; set; }

            public double TongTien { get; set; }
            public double SoTien { get; set; }

            public double? TienThua { get; set; }
            public DateTime NgayThanhToan { get; set; }
            public string? LoaiGiaoDich { get; set; }
            public string? NguoiThaoTac { get; set; }
            public string HinhThucThanhToan { get; set; }
            public string? TrangThai { get; set; }
            public Guid IdHoaDon { get; set; }
            public Guid IdNhanVien { get; set; }
        }
        public class KhachHangViewModel
        {
            public Guid IdKhachHang { get; set; }

            public string? HoTen { get; set; }
            //[Required(ErrorMessage = "Không Được Để Trống")]
            [RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
            [StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
            public string? SoDienThoai { get; set; }
            //[Required(ErrorMessage = "Không Được Để Trống")]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Không Đúng Định Dạng")]
            public string? Email { get; set; }
        }
    }
    public class HoaDonViewModell
    {
        public HoaDon HoaDon { get; set; }
        public double TotalQuantity { get; set; }
        public List<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
