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
	public class MauSacConfig : IEntityTypeConfiguration<MauSac>
	{
		public void Configure(EntityTypeBuilder<MauSac> builder)
		{
			builder.HasKey(p => p.IdMauSac);
           
        }
	}
}
