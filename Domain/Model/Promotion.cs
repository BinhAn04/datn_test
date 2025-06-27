using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class Promotion
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdPromotion { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string TenPromotion { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Không Đúng Ký Tự")]
		public int PhanTramGiam { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayTao { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayBatDau { get; set; }
		[DataType(DataType.DateTime, ErrorMessage = "Không Đúng Định Dạng")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Range(typeof(DateTime), "1/1/2020", "12/31/2025", ErrorMessage = "Không Trong Thời Gian Cho Phép")]
		public DateTime NgayKetThuc { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public int TrangThai { get; set; }

		public virtual ICollection<PromotionSanPhamChiTiet>? PromotionSanPhamChiTiets { get; set; }
        public string GetTrangThaiDisplay()
        {
            return TrangThai switch
            {
                0 => "Dừng Hoạt Động",
                1 => "Hoạt Động",
                2 => "Chờ Hoạt Động",
                3 => "Expired",
                _ => "Unknown"
            };
        }
    }

}
