using AppData.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppData.Configurations
{
    public class PromotionConfig : IEntityTypeConfiguration<Promotion>
	{
		public void Configure(EntityTypeBuilder<Promotion> builder)
		{
            builder.HasKey(p => p.IdPromotion);

            builder.HasMany(p => p.PromotionSanPhamChiTiets)
                   .WithOne(psct => psct.Promotion)
                   .HasForeignKey(psct => psct.IdPromotion);
        }
	}
}
