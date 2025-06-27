using AppData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class GioHangChiTietViewModel
    {
        public bool Chon { get; set; }
        public Guid IdGioHangChiTiet { get; set; }
        public List<HinhAnh>? HinhAnhs { get; set; }
        public string TenSanPham { get; set; }
        public double? DonGia { get; set; }
        public double SoLuong { get; set; }
        public double Quantity { get; set; }
        public double TongTien { get; set; }
        public double? GiaDaGiam { get; set; }
        public double? PhanTramGiam { get; set; }

        public int KichHoat {  get; set; }

        public int HoatKich { get; set; }

    }

    public class UpdateQuantityViewModel
    {
        public Guid IdGioHangChiTiet { get; set; }
        public double DonGia {  set; get; }
        public double Quantity { get; set; }
        public double TongTien { get; set; } // Đổi tên thuộc tính
    }
    public class RemoveRequest
    {
        public Guid IdGioHangChiTiet { get; set; }
    }
}
