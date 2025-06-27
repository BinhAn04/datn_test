using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
	public class GioHangChiTietConfig : IEntityTypeConfiguration<GioHangChiTiet>
	{
		public void Configure(EntityTypeBuilder<GioHangChiTiet> builder)
		{
			builder.HasKey(p => p.IdGioHangChiTiet);

			builder.HasOne(p => p.SanPhamChiTiet)
				   .WithMany(p => p.GioHangChiTiets)
				   .HasForeignKey(p => p.IdSanPhamChiTiet);

			builder.HasOne(p => p.GioHang)
				   .WithMany(p => p.GioHangChiTiets)
				   .HasForeignKey(p => p.IdGioHang);
		}
	}
}
