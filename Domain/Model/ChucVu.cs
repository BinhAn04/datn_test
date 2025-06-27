using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
	public class ChucVu
	{
		[Required]
		public Guid IdChucVu { get; set; }

		[Required(ErrorMessage = "Không Được Để Trống")]

		public string Code { get; set; }
		[Required(ErrorMessage = "Không Được Để Trống")]
		public string TenChucVu { get; set; }

	    public ICollection<NhanVien>? nhanViens { get; set; }
	}
}
