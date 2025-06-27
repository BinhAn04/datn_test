using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class ChatLieu
	{
		[Required]
		public Guid IdChatLieu { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[CheckTenChatLieu]
		public string TenChatLieu { get; set; }

		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayCapNhat { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiCapNhat { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiTao { get; set; }
		public int KichHoat { get; set; }

		public virtual ICollection<SanPham>? SanPhams { get; set; }
	}
}
