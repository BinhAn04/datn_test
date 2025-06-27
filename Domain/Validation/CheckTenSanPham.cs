using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenSanPham : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenSanPham()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdSanPham");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var sanPhamId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.sanPhams
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(sp => sp.TenSanPham == name && sp.IdSanPham != sanPhamId);

            if (existing)
            {
                return new ValidationResult("Tên sản phẩm đã tồn tại", new[] { "TenSanPham" });
            }
            return ValidationResult.Success;
        }
    }
}