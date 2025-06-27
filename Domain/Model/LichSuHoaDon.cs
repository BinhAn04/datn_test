using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class LichSuHoaDon
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdLichSuHoaDon { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string ThaoTac { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiThaoTac { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string TrangThai { get; set; }

		public string? GhiChu { get; set; }
		public Guid IdHoaDon { get; set; }

        public Guid? IdNhanVien { get; set; }
        public virtual HoaDon? HoaDon { get; set; }

        public virtual NhanVien? NhanVien { get; set; }
    }
}
