using AppData.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AppData.ViewModel
{
    public class SanPhamViewModel
    {
        public SanPham SanPham { get; set; } = new SanPham();
        public List<CombinationViewModel> Combinations { get; set; } = new List<CombinationViewModel>();
        public List<SelectListItem>? KichCoOptions { get; set; }
        public List<SelectListItem>? MauSacOptions { get; set; }
        public List<string> SelectedKichCoIds { get; set; } = new List<string>();
        public List<string> SelectedMauSacIds { get; set; } = new List<string>();

        public Guid IdChatLieu { get; set; }
        public Guid IdKieuDang { get; set; }
        public Guid IdThuongHieu { get; set; }
        public Guid IdDanhMuc { get; set; }
        public Guid IdDeGiay { get; set; }

        public List<SanPhamChiTiet> SanPhamChiTiet { get; set; } = new List<SanPhamChiTiet>();
        public double TotalQuantity { get; set; }

    }
}
