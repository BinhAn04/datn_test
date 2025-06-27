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
    public class SanPhamChiTietConfig : IEntityTypeConfiguration<SanPhamChiTiet>
    {
        public void Configure(EntityTypeBuilder<SanPhamChiTiet> builder)
        {
            builder.HasKey(p => p.IdSanPhamChiTiet);

            // Cấu hình mối quan hệ với KichCo
            builder.HasMany(spct => spct.SanPhamChiTietKichCos)
               .WithOne(spctk => spctk.SanPhamChiTiet)
               .HasForeignKey(spctk => spctk.IdSanPhamChiTiet);

            // Cấu hình mối quan hệ với bảng trung gian SanPhamChiTietMauSac
            builder.HasMany(spct => spct.SanPhamChiTietMauSacs)
                .WithOne(spctm => spctm.SanPhamChiTiet)
                .HasForeignKey(spctm => spctm.IdSanPhamChiTiet);

            builder.HasOne(p => p.SanPham)
                   .WithMany(p => p.SanPhamChiTiets)
                   .HasForeignKey(p => p.IdSanPham);

            builder.HasMany(spct => spct.PromotionSanPhamChiTiets)
                .WithOne(psct => psct.SanPhamChiTiet)
                .HasForeignKey(psct => psct.IdSanPhamChiTiet);
        }
        //builder.HasOne(p => p.DayGiay)
        //	   .WithMany(p => p.SanPhamChiTiets)
        //	   .HasForeignKey(p => p.IdDayGiay);
    }
}

