using AppData.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModel
{
    public class SanPhamChiTietViewModel
    {
        public Guid IdSanPham { get; set; }

        public string? TenSanPham { get; set; }
        public Guid ThuongHieuId { get; set; } 
        public Guid ChatLieuId { get; set; } 
        public Guid DanhMucId { get; set; }
        public Guid DeGiayId { get; set; } 
        public Guid KieuDangId { get; set; }

        public string? ThuongHieu {  get; set; }
        public string? DanhMuc { get; set; }
        public string? ChatLieu { get; set; }
        public string? KieuDang { get; set; }
        public string? DeGiay { get; set; }   
        public int KichHoat { get; set; }
        public List<string>? SelectedKichCoIds { get; set; }
        public List<string>? SelectedMauSacIds { get; set; } 
        public List<SanPhamChiTietItemViewModel>? SanPhamChiTietList { get; set; } = new List<SanPhamChiTietItemViewModel>();
    }
    public class SanPhamChiTietItemViewModel
    {
        public Guid IdSanPhamChiTiet { get; set; }
        public string? MaSanPham { get; set; }
        public double? GiaDaGiam { get; set; }
        public List<HinhAnh>? HinhAnhs { get; set; }
        public List<string> MauSac { get; set; } = new List<string>();
        public List<string> KichCo { get; set; } = new List<string>();
        public double Gia { get; set; }
        public double SoLuong { get; set; }
        public string? XuatXu { get; set; }
        public int KichHoat { get; set; }
        public bool Chon { get; set; }
        public List<IFormFile>? Files { get; set; } = new List<IFormFile>();

    }

}
