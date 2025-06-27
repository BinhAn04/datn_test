using AppData.Model;
using AppData.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppData.Validation
{
    public class CheckVoucher : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if the object instance is of type VoucherDto or Voucher
            if (validationContext.ObjectInstance is VoucherDto voucherDto)
            {
                // Validate for VoucherDto
                return ValidateVoucherDto(voucherDto, value);
            }
            else if (validationContext.ObjectInstance is Voucher voucher)
            {
                // Validate for Voucher
                return ValidateVoucher(voucher, value);
            }

            return new ValidationResult("Invalid voucher type.");
        }

        private ValidationResult ValidateVoucherDto(VoucherDto voucherDto, object value)
        {
            // If LoaiGiamGia is percentage-based
            if (voucherDto.LoaiGiamGia == 1) // 1 represents percentage
            {
                if (value is int giaTriGiam && (giaTriGiam < 1 || giaTriGiam > 100))
                {
                    return new ValidationResult("Giá trị giảm phải nằm trong khoảng từ 1% đến 100%.");
                }
            }
            return ValidationResult.Success;
        }

        private ValidationResult ValidateVoucher(Voucher voucher, object value)
        {
            // Implement similar validation logic for Voucher if needed
            if (voucher.LoaiGiamGia == 1) // Assuming 1 is percentage
            {
                if (value is int giaTriGiam && (giaTriGiam < 1 || giaTriGiam > 100))
                {
                    return new ValidationResult("Giá trị giảm phải nằm trong khoảng từ 1% đến 100%.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
public class VoucherDto
{
    public Guid VoucherId { get; set; }

    [Required(ErrorMessage = "Mã voucher không được để trống")]
    public string MaVoucher { get; set; }

    public string? MoTaVoucher { get; set; }

    [Required(ErrorMessage = "Không được để trống")]
    [Range(1, 2)]
    public int LoaiGiamGia { get; set; } // 1 for percentage, 2 for fixed amount

    [Required(ErrorMessage = "Không được để trống")]
    [Range(1, int.MaxValue, ErrorMessage = "Giá trị giảm phải lớn hơn hoặc bằng 1")]
    [CheckVoucher] // Ensure this attribute is used
    public int GiaTriGiam { get; set; }

    public int? GiaTriDonHangToiThieu { get; set; }
    public int? SoTienToiDa { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
    public int TongSoLuongVoucher { get; set; }
    public int SoLuongVoucherConLai { get; set; }
    public int TrangThai { get; set; }
    public DateTime NgayTao { get; set; }
    public string NguoiTao { get; set; }
    public DateTime? NgayUpdate { get; set; }
    public string? NguoiUpdate { get; set; }
}