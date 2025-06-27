using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class GioHangChiTiet
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdGioHangChiTiet { get; set; }

        [Required(ErrorMessage = "Không Được Để Trống")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
        [Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
        public double DonGia { get; set; }

        [Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double SoLuong { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double TongTien { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public int KichHoat { get; set; }
		public Guid IdGioHang { get; set; }
		public Guid IdSanPhamChiTiet { get; set; }
		public virtual GioHang? GioHang { get; set; }
		public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
	}
}
