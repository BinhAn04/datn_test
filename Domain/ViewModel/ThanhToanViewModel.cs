using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppData.ViewModel
{
    public class ThanhToanViewModel
    {
        public string? NguoiNhan { get; set; }

        [RegularExpression(@"^(\+84|0)[3|5|7|8|9][0-9]{8}$", ErrorMessage = "Không Đúng Định Dạng")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phải Đủ 10 Số")]
        public string? SoDienThoai { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố không được để trống")]
        public string ProvinceName { get; set; }
        public int? ProvinceId { get; set; }

        [Required(ErrorMessage = "Quận/Huyện không được để trống")]
        public string DistrictName { get; set; }

        public int? DistrictId { get; set; }

        [Required(ErrorMessage = "Phường/Xã không được để trống")]
        public string WardName { get; set; }

        public string? WardId { get; set; }
        public string? MoTa { get; set; }

        public string? DiaChiCuThe { get; set; }

        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public List<BuyItemViewModel> BuyItem { get; set; } = new List<BuyItemViewModel>();

        public string PaymentMethod { get; set; }
        public double PhiVanChuyen { get; set; }
    }

    public class CartItemViewModel
    {
        public Guid IdSanPhamChiTiet { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }

        public double OriginalPrice { get; set; }

    }
    public class BuyItemViewModel
    {
        public Guid IdSanPhamChiTiet { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double OriginalPrice { get; set; }

    }
    public class SaveOrderResult
    {
        public string OrderNumber { get; set; }
        public double TotalAmount { get; set; }
    }
}