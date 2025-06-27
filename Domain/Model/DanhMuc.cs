using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class DanhMuc
	{
		public Guid IdDanhMuc { get; set; }
		[Required(ErrorMessage =" Không Được Để Trống Tên Danh Mục")]
        [CheckTenDanhMuc]
        public string TenDanhMuc { get; set; }

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
        //[Required(ErrorMessage = "Trạng thái kích hoạt không được để trống")] 
		public int KichHoat { get; set; }

		public virtual ICollection<SanPham>? SanPhams { get; set; }
	}
}
