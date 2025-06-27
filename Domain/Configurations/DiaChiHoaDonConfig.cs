using AppData.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
    public class DiaChiHoaDonConfig : IEntityTypeConfiguration<DiaChiHoaDon>
    {
        public void Configure(EntityTypeBuilder<DiaChiHoaDon> builder)
        {
            builder.HasKey(p => p.IdDiaChiHoaDon);      
        }
    }
}
