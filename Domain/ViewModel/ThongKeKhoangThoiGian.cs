using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class ThongKeKhoangThoiGian
    {
        public DateTime Ngay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TongDonHang { get; set; }
        public int DonHangHuy { get; set; }
        public int DonHangChoXacNhan { get; set; }
        public int DonHangThanhCong { get; set; }
        public double TongDoanhThu { get; set; }
    }
}
