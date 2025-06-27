namespace APPMVC.ViewModel
{
    public class SanPhamClientViewModel
    {
        public Guid IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public double? GiaThapNhat { get; set; }
        public double? GiaCaoNhat { get; set; }
        public int SoLuongMau { get; set; }
        public byte[] RepresentativeImage { get; set; }
        public List<RepresentativeImageViewModel> ColorImages { get; set; }
        public double HighestDiscountPercentage { get; set; }
    }


}