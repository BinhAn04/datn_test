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
	public class DiaChiConfig : IEntityTypeConfiguration<DiaChi>
	{
		public void Configure(EntityTypeBuilder<DiaChi> builder)
		{
			builder.HasKey(p => p.IdDiaChi);
			builder.HasOne(p => p.khachHang)
				   .WithMany(p => p.DiaChis) 
				   .HasForeignKey(p => p.IdKhachHang);
		}
	}
}
