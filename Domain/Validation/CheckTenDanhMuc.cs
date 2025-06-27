using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenDanhMuc : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenDanhMuc()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdDanhMuc");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var danhMucId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.danhMuc
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(dm => dm.TenDanhMuc == name && dm.IdDanhMuc != danhMucId);

            if (existing)
            {
                return new ValidationResult("Tên danh mục đã tồn tại", new[] { "TenDanhMuc" });
            }
            return ValidationResult.Success;
        }
    }
}