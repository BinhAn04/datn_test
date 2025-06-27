using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppData.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData.ViewModel
{
    public class CombinationViewModel
    {
        public string? TenSanPham { get; set; } 
        public string? KichCo { get; set; }
        public string? MauSac { get; set; }
        public double Gia { get; set; }
        public double SoLuong { get; set; }
        public string? XuatXu { get; set; }
        public Guid? IdSanPhamChiTiet { get; set; }
        public List<HinhAnh>? hinhAnhs { get; set; }

        [NotMapped]
        public List<IFormFile>? Files { get; set; } = new List<IFormFile>();
        public string? KichCoId { get; set; }
        public string? MauSacId { get; set; }
    }

}
