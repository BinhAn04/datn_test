using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class ThongKeViewModel
    {
        public int TongDonHang { get; set; }
        public double TongDoanhThu { get; set; }
        public List<ThongKeNgay> ThongKeNgay { get; set; }
        public List<ThongKeNgay> ThongKeTuan { get; set; }
        public List<ThongKeNgay> ThongKeThang { get; set; }
        public List<ThongKeThang> ThongKeNam { get; set; }
        public List<ThongKeKhoangThoiGian> ThongKeKhoangThoiGian {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
