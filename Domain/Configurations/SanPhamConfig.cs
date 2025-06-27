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
	public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
	{
		public void Configure(EntityTypeBuilder<SanPham> builder)
		{
			builder.HasKey(p => p.IdSanPham);

			builder.HasOne(p => p.DanhMuc)
				   .WithMany(p => p.SanPhams)
				   .HasForeignKey(p => p.IdDanhMuc);


            builder.HasOne(p => p.ThuongHieu)
				   .WithMany(p => p.SanPhams)
				   .HasForeignKey(p => p.IdThuongHieu);


            builder.HasOne(p => p.KieuDang)
				   .WithMany(p => p.SanPhams)
				   .HasForeignKey(p => p.IdKieuDang);


			builder.HasOne(p => p.ChatLieu)
				   .WithMany(p => p.SanPhams)
				   .HasForeignKey(p => p.IdChatLieu);

            builder.HasOne(p => p.DeGiay)
				   .WithMany(p => p.SanPhams)
				   .HasForeignKey(p => p.IdDeGiay);
        }
	}
}
