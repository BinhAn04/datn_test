using AppData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class KhachHangViewModel
    {
        public KhachHang? KhachHang { get; set; }
        public string? DiaChi { get; set; } // Address name
    }
    public class DiaChiViewModel
    {
        public Guid IdDiaChi { get; set; } // Assuming you need the ID
        public string HoTen { get; set; } // Name of the recipient
        public string SoDienThoai { get; set; } // Phone number
        public string Diachi { get; set; } // Address
        public bool DiaChiMacDinh { get; set; } // Default address flag
        public DateTime NgayTao { get; set; } // Creation date
        public DateTime NgayCapNhat { get; set; } // Update date
        public string CustomerName { get; set; } // Customer name
    }
    public class NhanVienViewModel
    {
        public NhanVien? NhanVien { get; set; }
        public Guid IdNhanVien { get; set; }
        public string? TenNhanVien { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }

        public List<ChucVu>? ChucVuList { get; set; }
        public string? TenChucVu { get; set; } // Add this property
        public string? AnhNhanVien { get; set; }
        public int? KichHoat { get; set; }
        public int? TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}
