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
	public class NhanVienConfig : IEntityTypeConfiguration<NhanVien>
	{
		public void Configure(EntityTypeBuilder<NhanVien> builder)
		{
			builder.HasKey(p => p.IdNhanVien);

			builder.HasOne(p => p.chucVu)
					.WithMany(p => p.nhanViens)
					.HasForeignKey(p => p.IdchucVu);
		}
	}
}
