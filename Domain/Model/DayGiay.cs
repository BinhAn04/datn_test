using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	//public class DayGiay
	//{
	//	[Required(ErrorMessage = "Không Được Để Trống")]
	//	public Guid IdDayGiay { get; set; }
	//	[Required(ErrorMessage = "Không Được Để Trống")]
	//	[CheckTenDayGiay(ErrorMessage = "Tên đã tồn tại")]
	//	public string TenDayGiay { get; set; }
	//	[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
	//	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	//	//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
	//	public DateTime NgayCapNhat { get; set; }
	//	[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
	//	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	//	//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
	//	public DateTime NgayTao { get; set; }
	//	[Required(ErrorMessage = "Không Được Để Trống")]
	//	public string NguoiCapNhat { get; set; }
	//	[Required(ErrorMessage = "Không Được Để Trống")]
	//	public string NguoiTao { get; set; }
	//	[Required(ErrorMessage = "Không Được Để Trống")]
	//	public int KichHoat { get; set; }
	//	public ICollection<SanPhamChiTiet>? SanPhamChiTiets { get; set; }
	//}
}
