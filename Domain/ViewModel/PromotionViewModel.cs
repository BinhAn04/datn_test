using AppData.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class PromotionViewModel
    {
        public Promotion Promotion { get; set; } = new Promotion();
        public List<SanPhamChiTietViewModel> SanPhamChiTiets { get; set; } = new List<SanPhamChiTietViewModel>();
        public List<SanPhamViewModel> SanPhams { get; set; } = new List<SanPhamViewModel>();
        public List<Guid>? SelectedSanPhamChiTietIds { get; set; }
        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public DateTime NgayTao { get; set; }

        public class SanPhamChiTietViewModel
        {
            public Guid IdSanPhamChiTiet { get; set; }
            public string? MaSP {  get; set; }
            public string? ProductName { get; set; }
            public double Quantity { get; set; }
            public double Price { get; set; }
            public List<HinhAnh>? HinhAnhs { get; set; }
            public List<string> MauSac { get; set; } = new List<string>();
            public List<string> KichCo { get; set; } = new List<string>();
        }
        public class SanPhamViewModel
        {
            public Guid IdSanPham { get; set; }

            public string? TenSanPham { get; set; }
            public Guid ThuongHieuId { get; set; }
            public Guid ChatLieuId { get; set; }
            public Guid DanhMucId { get; set; }
            public Guid DeGiayId { get; set; }
            public Guid KieuDangId { get; set; }

            public string? ThuongHieu { get; set; }
            public string? DanhMuc { get; set; }
            public string? ChatLieu { get; set; }
            public string? KieuDang { get; set; }
            public string? DeGiay { get; set; }
        }
        }
    }
