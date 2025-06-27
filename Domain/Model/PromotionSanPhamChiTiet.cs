namespace AppData.Model
{
    public class PromotionSanPhamChiTiet
    {
        public Guid IdPromotion { get; set; }
        public Guid IdSanPhamChiTiet { get; set; }
        public virtual Promotion? Promotion { get; set; }
        public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
    }
}