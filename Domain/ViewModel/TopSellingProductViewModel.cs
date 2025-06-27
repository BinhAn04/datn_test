using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class TopSellingProductViewModel
    {
        public Guid SanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public double SoLuongBan { get; set; }
        public double TongDoanhThu { get; set; }
    }
}
