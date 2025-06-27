using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppData.Configurations
{
    public class VoucherConfig : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(p => p.VoucherId);
            builder.HasMany(v => v.LichSuSuDungVouchers)
            .WithOne(lsv => lsv.Voucher)
            .HasForeignKey(lsv => lsv.IdVoucher);
        }
    }
}
