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
	public class HoaDonConfig : IEntityTypeConfiguration<HoaDon>
	{
		public void Configure(EntityTypeBuilder<HoaDon> builder)
		{
			builder.HasKey(p => p.IdHoaDon);

			builder.HasOne(p => p.KhachHang)
				  .WithMany(p => p.HoaDons)
				  .HasForeignKey(p => p.IdKhachHang);

			builder.HasOne(p => p.NhanVien)
				  .WithMany(p => p.HoaDons)
				  .HasForeignKey(p => p.IdNhanVien);

            builder.HasOne(p => p.DiaChiHoaDon)
              .WithMany(p => p.HoaDons)
              .HasForeignKey(p => p.IdDiaChiHoaDon);
        }
    }
}
