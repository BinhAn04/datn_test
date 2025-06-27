using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class KhachHang
	{
		public Guid? IdKhachHang { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? HoTen { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
		[StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
		public string? SoDienThoai { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Không Đúng Định Dạng")]
		public string? Email { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? AuthProvider { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[StringLength(20, ErrorMessage = "Phải Lớn Hơn 8 Ký tự và Nhỏ hơn 20", MinimumLength = 8)]
		//[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái thường, một chữ cái hoa, một chữ số và một ký tự đặc biệt")]
		public string? MatKhau { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime NgayTao { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
		public DateTime NgayCapNhat { get; set; }
		public string? NguoiTao { get; set; }
		public string? NguoiCapNhat { get; set; }
		public int KichHoat { get; set; }

		//public string ? LoaiKhach { get; set; }

		public virtual GioHang? GioHang { get; set; }
		public virtual ICollection<DiaChi>? DiaChis { get; set; }

		public virtual ICollection<HoaDon>? HoaDons { get; set; }

		public virtual ICollection<LichSuSuDungVoucher>? LichSuSuDungVouchers { get; set; }

	}
}
