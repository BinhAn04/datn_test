using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class SanPham
	{
    
        //[Required(ErrorMessage = "Không Được Để Trống")]
        public Guid IdSanPham { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[CheckTenSanPham]
		public string? TenSanPham { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayCapNhat { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? NguoiCapNhat { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? NguoiTao { get; set; }

		public int KichHoat { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		public string? MoTa { get; set; }
		//[Required(ErrorMessage = "Không Được Để Trống")]
		//[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		//[Range(1, double.MaxValue, ErrorMessage = "Không Được Nhập Số Âm")]
		public double Sale { get; set; }
		public Guid IdChatLieu { get; set; }
		public Guid IdKieuDang { get; set; }
		public Guid IdThuongHieu { get; set; }
		public Guid IdDanhMuc { get; set; }
        public Guid IdDeGiay { get; set; }

        public virtual DeGiay? DeGiay { get; set; }
        public virtual ChatLieu? ChatLieu { get; set; }
		public virtual KieuDang? KieuDang { get; set; }
		public virtual ThuongHieu? ThuongHieu { get; set; }
		public virtual DanhMuc? DanhMuc { get; set; }
		
		public virtual ICollection<SanPhamChiTiet>? SanPhamChiTiets { get; set; }


	}
}
