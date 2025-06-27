using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
	public class GioHangConfig : IEntityTypeConfiguration<GioHang>
	{
		public void Configure(EntityTypeBuilder<GioHang> builder)
		{
			builder.HasKey(p => p.IdGioHang);

			builder.HasOne(p => p.KhachHang).WithOne(p => p.GioHang).HasForeignKey<GioHang>(p => p.IdKhachHang);
		}
	}
}
