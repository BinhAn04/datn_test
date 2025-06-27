using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class HoaDonChiTiet
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdHoaDonChiTiet { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double DonGia { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double TienGiam { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double SoLuong { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double TongTien { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public double KichHoat { get; set; }
		public Guid IdHoaDon { get; set; }
		public Guid IdSanPhamChiTiet { get; set; }
		public virtual HoaDon? HoaDon { get; set; }
		public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
	}
}
