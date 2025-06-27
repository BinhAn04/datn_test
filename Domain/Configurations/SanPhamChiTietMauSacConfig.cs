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
    internal class SanPhamChiTietMauSacConfig : IEntityTypeConfiguration<SanPhamChiTietMauSac>
    {
        public void Configure(EntityTypeBuilder<SanPhamChiTietMauSac> builder)
        {
            builder.HasKey(sm => new { sm.IdSanPhamChiTiet, sm.IdMauSac });

            builder.HasOne(sm => sm.SanPhamChiTiet)
                .WithMany(s => s.SanPhamChiTietMauSacs)
                .HasForeignKey(sm => sm.IdSanPhamChiTiet);

            builder.HasOne(sm => sm.MauSac)
                .WithMany(m => m.SanPhamChiTietMauSacs)
                .HasForeignKey(sm => sm.IdMauSac);
        }
    }
}
