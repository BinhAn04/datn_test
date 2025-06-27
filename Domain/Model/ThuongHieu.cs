using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class ThuongHieu
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdThuongHieu { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[CheckTenThuongHieu]
		public string TenThuongHieu { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayCapNhat { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NgayTao { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiCapNhat { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string NguoiTao { get; set; }
		public int KichHoat { get; set; }

		public virtual ICollection<SanPham>? SanPhams { get; set; }
	}
}
