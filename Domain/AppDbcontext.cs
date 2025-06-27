using AppData.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
	public class AppDbcontext : DbContext
	{
        public AppDbcontext()
        {
            
        }

		public AppDbcontext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ChatLieu> chatLieus { get; set; }

		public DbSet<ChucVu> chuVu { get; set; }

		public DbSet<DanhMuc> danhMuc { get; set; }

		//public DbSet<DayGiay> dayGiay { get; set; }

		public DbSet<DeGiay> deGiay { get; set; }

		public DbSet<DiaChi> diaChi { get; set; }


		public DbSet<GioHang> gioHang { get; set; }

		public DbSet<GioHangChiTiet> gioHangChiTiets { get; set; }

		public DbSet<HinhAnh> hinhAnh { get; set; }

		public DbSet<HoaDon> hoaDons { get; set; }

		public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }

		public DbSet<KichCo> kichCos { get; set; }

		public DbSet<KieuDang> kieuDangs { get; set; }

		public DbSet<KhachHang> khachHangs { get; set; }

		public DbSet<Promotion> promotions { get; set; }

		public DbSet<LichSuHoaDon> lichSuHoaDons { get; set; }

		public DbSet<MauSac> mauSacs { get; set; }

		public DbSet<NhanVien> nhanViens { get; set; }

		public DbSet<SanPham> sanPhams { get; set; }

		public DbSet<SanPhamChiTiet> sanPhamChiTiets{ get; set; }
		public DbSet<SanPhamChiTietKichCo> sanPhamChiTietKichCos { get; set; }
		public DbSet<SanPhamChiTietMauSac> sanPhamChiTietMausacs { get; set; }
		public DbSet<LichSuThanhToan> lichSuThanhToans { get; set; }
		public DbSet<ThuongHieu> thuongHieus { get; set; }
		public DbSet<Voucher> vouchers { get; set; }
		public DbSet<LichSuSuDungVoucher> LichSuSuDungVouchers { get; set; }
		public DbSet<Province> provinces { get; set; }
		public DbSet<District> districts { get; set; }
		public DbSet<Ward> wards { get; set; }

		public DbSet<DiaChiHoaDon> diaChiHoaDons { get; set; }

		public DbSet<PromotionSanPhamChiTiet> promotionSanPhamChiTiets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

			optionsBuilder.UseSqlServer("Server=ADMIN;Database=daantotnghiep;Trusted_Connection=True;TrustServerCertificate=True;");

			//Server Của Long
			//optionsBuilder.UseSqlServer("Server=DESKTOP-UC1K64J\\SQLEXPRESS04;Database=daantotnghiep;Trusted_Connection=True;TrustServerCertificate=True;");

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.Seed();
		}
	}
}
