using AppData.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
    public class Voucher
    {
        [Required(ErrorMessage = "Không được để trống")]
        public Guid VoucherId { get; set; }
        [Required(ErrorMessage = "Mã voucher không được để trống")]
        public string MaVoucher { get; set; }
        public string? MoTaVoucher { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, 2)]
        public int LoaiGiamGia { get; set; } //Theo % gia tri don hang hoac so tien
        [Required(ErrorMessage ="Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage ="Giá trị giảm phải lớn hơn hoặc bằng 1")]
        [CheckVoucher]
        public int GiaTriGiam { get; set; }
        public int? GiaTriDonHangToiThieu { get; set; } 
        public int? SoTienToiDa {  get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }       
        public int TrangThai {  get; set; }
        public DateTime NgayTao {  get; set; }
        public string? NguoiTao { get; set; } 
        public DateTime? NgayUpdate {  get; set; }
        public string? NguoiUpdate { get; set; } //Tạm thời nhập tay, sau khi hoàn thành phân quyền đăng nhập chuyển sang IdUser


        public virtual ICollection<LichSuSuDungVoucher>? LichSuSuDungVouchers { get; set; }

        public string GetLoaiGiamGiaDisplay()
        {
            return LoaiGiamGia switch
            {
                1 => "Theo % sản phẩm",
                2 => "Theo số tiền"
            };
        }
        public string GetTrangThaiDisplay()
        {
            return TrangThai switch
            {
                0 => "Hết hạn",
                1 => "Chờ kích hoạt",
                2 => "Đang kích hoạt",
                3 => "Tạm dừng",
                4 => "Dừng",
                5 => "Hết lượt sử dụng"
            };
            
        }
    }
}
