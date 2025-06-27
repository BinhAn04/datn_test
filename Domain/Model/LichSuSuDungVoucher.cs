using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Model
{
    public class LichSuSuDungVoucher
    {
        public Guid IdLichSuVoucher {  get; set; }
        public Guid IdVoucher {  get; set; }
        public Guid IdKhachHang { get; set; }
        public Guid? IdHoaDon {  get; set; }
        public DateTime? NgaySuDungVoucher { get; set; }
        public int TrangThaiVoucher { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
    }
}
