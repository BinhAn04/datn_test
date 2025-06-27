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
    public class LichSuThanhToanConfig : IEntityTypeConfiguration<LichSuThanhToan>
    {
        public void Configure(EntityTypeBuilder<LichSuThanhToan> builder)
        {
            // Specify the primary key
            builder.HasKey(p => p.IdLichSuThanhToan);

            // Configure relationships
            builder.HasOne(p => p.HoaDon)
                   .WithMany(p => p.lichSuThanhToans)
                   .HasForeignKey(p => p.IdHoaDon)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(p => p.NhanVien)
                   .WithMany(p => p.lichSuThanhToans) 
                   .HasForeignKey(p => p.IdNhanVien)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
