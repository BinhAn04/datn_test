using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class HoaDon
	{
		[Required]
		public Guid IdHoaDon { get; set; }
		[Required (ErrorMessage ="Không Được Để Trống")]
		public string MaDon { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? NguoiNhan { get; set; }
		//[Required (ErrorMessage = "Không Được Để Trống")]
		public string? SoDienThoaiNguoiNhan { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? DiaChiGiaoHang { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? LoaiHoaDon { get; set; }
		public string? GhiChu { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Range(0,double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double? TienShip { get; set; }
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Range(0, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double? TienGiam { get; set; }
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[Range(0, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double TongTienDonHang { get; set; }
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[Range(0, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double TongTienHoaDon { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiTao { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public int KichHoat { get; set; }

		public string TrangThai {get; set; }

		public Guid? IdKhachHang { get; set; }
		public Guid? IdNhanVien { get; set; }

        public Guid? IdDiaChiHoaDon { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public virtual DiaChiHoaDon? DiaChiHoaDon { get; set; }
        public virtual NhanVien? NhanVien { get; set; }

        public ICollection<LichSuThanhToan>? lichSuThanhToans { get; set; }
        public virtual ICollection<LichSuHoaDon>? LichSuHoaDons { get; set; }
        public virtual ICollection<LichSuSuDungVoucher>? LichSuSuDungVouchers { get; set; }
        public virtual ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
	}
}
