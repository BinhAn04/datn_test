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
	public class KhachHangConfig : IEntityTypeConfiguration<KhachHang>
	{
		public void Configure(EntityTypeBuilder<KhachHang> builder)
		{
			builder.HasKey(p => p.IdKhachHang);

            builder.Property(p => p.Email)
            .HasMaxLength(100);

            builder.Property(p => p.SoDienThoai)
                .HasMaxLength(10);

            builder.Property(p => p.MatKhau)
                .HasMaxLength(200);

            builder.HasMany(kh => kh.LichSuSuDungVouchers)
                .WithOne(lsv => lsv.KhachHang)
                .HasForeignKey(lsv => lsv.IdKhachHang);
        }
	}
}
