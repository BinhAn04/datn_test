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
    public class SanPhamChiTietKichCoConig : IEntityTypeConfiguration<SanPhamChiTietKichCo>
    {
        public void Configure(EntityTypeBuilder<SanPhamChiTietKichCo> builder)
        {
            builder.HasKey(sc => new { sc.IdSanPhamChiTiet, sc.IdKichCo });

            builder.HasOne(sc => sc.SanPhamChiTiet)
                .WithMany(s => s.SanPhamChiTietKichCos)
                .HasForeignKey(sc => sc.IdSanPhamChiTiet);

            builder.HasOne(sc => sc.KichCo)
                .WithMany(k => k.SanPhamChiTietKichCos)
                .HasForeignKey(sc => sc.IdKichCo);
        }
    }
}
