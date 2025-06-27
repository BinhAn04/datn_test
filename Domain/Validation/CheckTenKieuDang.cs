using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenKieuDang : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenKieuDang()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdKieuDang");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var kieuDangId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.kieuDangs
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(kd => kd.TenKieuDang == name && kd.IdKieuDang != kieuDangId);

            if (existing)
            {
                return new ValidationResult("Tên kiểu dáng đã tồn tại", new[] { "TenKieuDang" });
            }
            return ValidationResult.Success;
        }
    }
}