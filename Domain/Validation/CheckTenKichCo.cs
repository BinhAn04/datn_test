using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validation
{
    public class CheckTenKichCo : ValidationAttribute
    {
        private readonly AppDbcontext _context;

        public CheckTenKichCo()
        {
            _context = new AppDbcontext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
                return ValidationResult.Success;

            var objectType = validationContext.ObjectType;
            var idProperty = objectType.GetProperty("IdKichCo");
            if (idProperty == null)
            {
                // Handle the case where Id property is not found
                return ValidationResult.Success; // or return an error message if needed
            }

            var kichCoId = (Guid)idProperty.GetValue(validationContext.ObjectInstance);
            var existing = _context.kichCos
                .AsNoTracking() // Add this to avoid tracking issues
                .Any(kc => kc.TenKichCo == name && kc.IdKichCo != kichCoId);

            if (existing)
            {
                return new ValidationResult("Tên kích cỡ đã tồn tại", new[] { "TenKichCo" });
            }
            return ValidationResult.Success;
        }
    }
}