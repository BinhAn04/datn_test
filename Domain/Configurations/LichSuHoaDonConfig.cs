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
	public class LichSuHoaDonConfig : IEntityTypeConfiguration<LichSuHoaDon>
	{
		public void Configure(EntityTypeBuilder<LichSuHoaDon> builder)
		{
			builder.HasKey(p => p.IdLichSuHoaDon);

			builder.HasOne(p => p.HoaDon)
				   .WithMany(p => p.LichSuHoaDons)
				   .HasForeignKey(p => p.IdHoaDon);

            builder.HasOne(p => p.NhanVien)
				   .WithMany(p => p.LichSuHoaDons)
				   .HasForeignKey(p => p.IdNhanVien)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
	}
}
