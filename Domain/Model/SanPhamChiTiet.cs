using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
    public class SanPhamChiTiet
    {
        public Guid IdSanPhamChiTiet { get; set; }
        [Required(ErrorMessage = "Không Được Để Trống")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
        [Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
        public double Gia { get; set; }
        public string? MaSp { get; set; }    

        [Required(ErrorMessage = "Không Được Để Trống")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
        [Range(0, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
        public double SoLuong { get; set; }
        public double? GiaGiam { get; set; }
        public string? CoHienThi { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
        public DateTime NgayCapNhat { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]

        //[Required(ErrorMessage = "Không Được Để Trống")]
        public string? GioiTinh { get; set; }

        [Required(ErrorMessage = "Không Được Để Trống")]
        public string? XuatXu { get; set; }
        public DateTime NgayTao { get; set; }
        //[Required(ErrorMessage = "Không Được Để Trống")]
        public string? NguoiCapNhat { get; set; }
        //[Required(ErrorMessage = "Không Được Để Trống")]
        public string? NguoiTao { get; set; }
        public int KichHoat { get; set; }
        public Guid IdSanPham { get; set; }
        public virtual SanPham? SanPham { get; set; }

        public virtual ICollection<HinhAnh>? HinhAnhs { get; set; }

        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }

        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public virtual ICollection<PromotionSanPhamChiTiet>? PromotionSanPhamChiTiets { get; set; }
        public virtual ICollection<SanPhamChiTietKichCo>? SanPhamChiTietKichCos { get; set; }

        public virtual ICollection<SanPhamChiTietMauSac>? SanPhamChiTietMauSacs { get; set; }
    }
}
