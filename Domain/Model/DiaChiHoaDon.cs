using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
    public class DiaChiHoaDon
    {
        public Guid IdDiaChiHoaDon { get; set; }
        [Required(ErrorMessage = "Không Được Để Trống")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Không Được Để Trống")]
        [RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
        public string SoDienThoai { get; set; }
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống")]
        public string ProvinceName { get; set; }
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Phường/Xã không được để trống")]
        public string DistrictName { get; set; }
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Quận/Huyện không được để trống")]
        public string WardName { get; set; }
        public string WardId { get; set; }
        public bool DiaChiMacDinh { get; set; }
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
    }
}
