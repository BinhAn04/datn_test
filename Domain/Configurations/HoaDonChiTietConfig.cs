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
	public class HoaDonChiTietConfig : IEntityTypeConfiguration<HoaDonChiTiet>
	{
		public void Configure(EntityTypeBuilder<HoaDonChiTiet> builder)
		{
			builder.HasKey(p => p.IdHoaDonChiTiet);

			builder.HasOne(p => p.HoaDon)
				   .WithMany(p => p.HoaDonChiTiets)
				   .HasForeignKey(p => p.IdHoaDon);

			builder.HasOne(p => p.SanPhamChiTiet)
				   .WithMany(p => p.HoaDonChiTiets)
				   .HasForeignKey(p => p.IdSanPhamChiTiet);
		}
	}
}
