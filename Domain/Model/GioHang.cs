using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class GioHang
	{
		[Required(ErrorMessage = "Không Được Để Trống")]
		public Guid IdGioHang { get; set; }
		public Guid? IdKhachHang { get; set; }

		public virtual KhachHang? KhachHang { get; set; }

		public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
	}
}
