using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class NhanVien
	{
		[Key]
		public Guid IdNhanVien { get; set; }
		public string? TenNhanVien { get; set; }
        public string? SoDienThoai { get; set; }
		public string? Email { get; set; }

		public string? AnhNhanVien { get; set; }

		public string? MatKhau { get; set; }

		public string? AuthProvider { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? DiaChi { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayCapNhat { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }

		public string? NguoiTao { get; set; }
		public string? NguoiCapNhat { get; set; }
		public int? KichHoat { get; set; }
		public int? TrangThai { get; set; }

		public Guid? IdchucVu { get; set; }
		public virtual ChucVu? chucVu { get; set; }

		public ICollection<HoaDon>? HoaDons { get; set; }

        public ICollection<LichSuThanhToan>? lichSuThanhToans { get; set; }
        public ICollection<LichSuHoaDon>? LichSuHoaDons { get; set; }
    }
}
